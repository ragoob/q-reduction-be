using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QReduction.Api.ShiftBackgroundServices
{
    public class ShiftHostedService : BackgroundService
    {
        public ShiftHostedService()
        {

        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
