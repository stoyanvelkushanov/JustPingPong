using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public class Ball : IUpdateble
    {
        public const char ballSymbol = '@';

        public Coords Speed { get; set; }
        public Coords Position { get; set; }

        public Ball(Coords position, Coords speed,char symbol = Ball.ballSymbol)
        {
            this.Speed = speed;
            this.Position = position;
        }

        public void Update()
        {
            this.Position += Speed;
        }
    }
}
