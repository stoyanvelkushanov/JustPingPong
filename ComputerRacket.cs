using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public class ComputerRacket : Racket
    {
        public ComputerRacket(Coords position, int width, char symbol = Racket.symbol)
            : base(position, width, symbol)
        {
        }

        public override void Update()
        {
            
        }
    }
}
