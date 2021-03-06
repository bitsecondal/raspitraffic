using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Almostengr.TrafficPi.LampControl.Workers
{
    public class FlashRedWorker : BaseWorker
    {
        private readonly GpioController _gpio;

        public FlashRedWorker(ILogger<FlashGreenWorker> logger, GpioController gpio) : 
            base(logger)
        {
            _gpio = gpio;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            InitializeGpio(_gpio);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            ShutdownGpio(_gpio);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ChangeSignal(LampOn, LampOff, LampOff, _gpio);
                await Task.Delay(TimeSpan.FromMilliseconds(FlasherDelay), stoppingToken);

                ChangeSignal(LampOff, LampOff, LampOff, _gpio);
                await Task.Delay(TimeSpan.FromMilliseconds(FlasherDelay), stoppingToken);
            }
        }
    }
}