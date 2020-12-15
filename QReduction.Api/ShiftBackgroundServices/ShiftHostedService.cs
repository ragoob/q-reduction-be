using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QReduction.Core.Domain;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QReduction.Api.ShiftBackgroundServices
{
    public class ShiftHostedService : BackgroundService
    {

        #region Fields

        private readonly IService<Shift> _shiftService;
        // private readonly ILogger<ShiftHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        #endregion

        public ShiftHostedService(
           IServiceScopeFactory scopeFactory

            )
        {
            _scopeFactory = scopeFactory;
            // _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*
            while (!stoppingToken.IsCancellationRequested)
            {
               // using (var scope = _scopeFactory.CreateScope())
               // {
                    // var _shiftService = scope.ServiceProvider.GetRequiredService<IService<Shift>>();
                    // var shifts = await _shiftService.FindAsync(c => c.EndAt.HasValue );
                    // foreach (var shift in shifts)
                    // {
                    //     var StartTime = (DateTime.Now.Hour == shift.StartAt.Hour &&
                    //                     DateTime.Now.Minute == shift.StartAt.Minute) ||

                    //                    (DateTime.Now.Hour == shift.StartAt.Hour &&
                    //                      shift.StartAt.Minute < DateTime.Now.Minute);

                    //     var EndTime = (DateTime.Now.Hour == shift.EndAt.Value.Hour &&
                    //                    DateTime.Now.Minute == shift.EndAt.Value.Minute) ||
                    //                    (DateTime.Now.Hour == shift.EndAt.Value.Hour &&
                    //                    DateTime.Now.Minute > shift.EndAt.Value.Minute);

                    //     if (StartTime)
                    //         shift.IsEnded = false;
                    //     if (EndTime)
                    //         shift.IsEnded = true;
                    // }

                    // await _shiftService.EditRangeAsync(shifts);
                //}
              //  await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

            }
            */
        }

        #region Helpers

        #endregion
    }
}
