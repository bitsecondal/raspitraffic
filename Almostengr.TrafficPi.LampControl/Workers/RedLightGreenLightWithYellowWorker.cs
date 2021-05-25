using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Almostengr.TrafficPi.LampControl.Workers
{
    public class RedLightGreenLightWithYellowWorker : BaseWorker
    {
        private readonly ILogger<RedLightGreenLightWithYellowWorker> _logger;
        private readonly GpioController _gpio;

        public RedLightGreenLightWithYellowWorker(ILogger<RedLightGreenLightWithYellowWorker> logger, 
            GpioController gpioController, AppSettings appSettings) : 
            base(logger, appSettings)
        {
            _logger = logger;
            _gpio = gpioController;
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
            int wait;

            while (!stoppingToken.IsCancellationRequested)
            {
                ChangeSignal(LampOn, LampOff, LampOff, _gpio);
                wait = random.Next(1, 5);
                await Task.Delay(TimeSpan.FromSeconds(wait), stoppingToken);

                ChangeSignal(LampOff, LampOff, LampOn, _gpio);
                wait = random.Next(1, 4);
                await Task.Delay(TimeSpan.FromSeconds(wait), stoppingToken);

                ChangeSignal(LampOff, LampOn, LampOff, _gpio);
                wait = random.Next(1, 3);
                await Task.Delay(TimeSpan.FromSeconds(wait), stoppingToken);
            }
        }
    }
}