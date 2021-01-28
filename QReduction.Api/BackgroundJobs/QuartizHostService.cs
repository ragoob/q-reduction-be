﻿using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QReduction.Api.BackgroundJobs
{
    public class QuartizHostService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartizHostService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder.Create(jobType).WithIdentity(jobType.FullName).WithDescription(jobType.Name).Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            if (schedule.StartImediatly)
            {
                return TriggerBuilder.Create().StartNow().WithIdentity($"{schedule.JobType.FullName}.trigger").WithSimpleSchedule(a => a.WithIntervalInMinutes(schedule.Intervals).RepeatForever()).WithDescription("Repeate every 15").Build();
            }
            else
            {
                DateTimeOffset startDate = DateBuilder.NextGivenMinuteDate(DateTime.Now, schedule.StartInterval);
                if ((startDate - DateTime.Now).TotalMinutes < schedule.StartInterval)
                {
                    startDate = DateBuilder.NextGivenMinuteDate(startDate, schedule.StartInterval);
                }
                return TriggerBuilder.Create().StartAt(startDate).WithIdentity($"{schedule.JobType.FullName}.trigger").WithSimpleSchedule(a => a.WithIntervalInMinutes(schedule.Intervals).RepeatForever()).WithDescription("Repeate every 15").Build();
            }
        }
    }
}
