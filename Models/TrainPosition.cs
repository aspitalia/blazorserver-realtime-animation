using System;

namespace BlazorServerRealtimeDemo.Models
{
    public class TrainPosition
    {
        const float maxValue = 100f;
        public TrainPosition(string name, float progress)
        {
            this.Name = name;
            this.Direction = (int) (progress / 100) % 2 == 0 ? Direction.SouthEast : Direction.NorthWest;
            this.Position = progress % maxValue;
            if (Direction == Direction.NorthWest)
            {
                this.Position = maxValue - this.Position; 
            }
        }
        public string Name { get; }
        public float Position { get; private set; }
        public Direction Direction { get; private set; }

        public void Advance(float amount)
        {
            if (Direction == Direction.SouthEast)
            {
                Position += amount;
                if (Position > 100.0f)
                {
                    Direction = Direction.NorthWest;
                    Position = 100.0f;
                }
            }
            else
            {
                Position -= amount;
                if (Position < 0f)
                {
                    Direction = Direction.SouthEast;
                    Position = 0f;
                }
            }
        }
    }
}