using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
namespace BlazorServerRealtimeDemo.Models
{
    public class Worker : BackgroundService, IMessageNotifier
    {
        private readonly IEnumerable<TrainPosition> positions = GenerateSamplePositions().ToList();

        private static IEnumerable<TrainPosition> GenerateSamplePositions()
        {
            //Simulate 3 trains at different positions
            string[] trainNames = new [] { "BC1528", "FE8820", "RG9472" };
            float currentPosition = 0f;
            foreach(var trainName in trainNames)
            {
                yield return new TrainPosition(trainName, currentPosition);
                currentPosition += 200.0f / trainNames.Length;
            };
        }

        public event EventHandler<TrainPosition> TrainMoved;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Solleviamo l'evento per notificare il messaggio ai sottoscrittori
                foreach (var position in positions)
                {
                    position.Advance(0.25f);
                    TrainMoved?.Invoke(this, position);
                }

                //E infine attende un secondo prima di generare il successivo messaggio
                await Task.Delay(50, stoppingToken);
            }
        }
    }
}