using System;

namespace BlazorServerRealtimeDemo.Models
{
    public interface IMessageNotifier
    {
        event EventHandler<TrainPosition> TrainMoved;
    }
}