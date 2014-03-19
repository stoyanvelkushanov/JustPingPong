using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public abstract class Racket : IUpdateble
    {
        public const char symbol = '|';

        public int RacketLength { get; protected set; }
        public Coords CurrentPosition { get; set; }
        public Directions directions;

        public Racket(Coords position,int width, char symbol)
        {
            this.RacketLength = width;
            symbol = Racket.symbol;
            this.CurrentPosition = position;
            this.directions = new Directions();
        }

        public virtual void Update()
        {
            
        }

        public override string ToString()
        {
            return Racket.symbol.ToString();
        }
    }
}
