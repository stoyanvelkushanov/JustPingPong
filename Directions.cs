using System;
using System.Collections.Generic;

namespace JustPingPong
{
    public class Directions
    {
        public Dictionary<string, Coords> direction;

        public Directions()
        {
            this.direction = new Dictionary<string, Coords>();

            //ball directions
            direction.Add("up-right", new Coords(-1, 1));
            direction.Add("up-left", new Coords(-1, -1));
            direction.Add("down-right", new Coords(1, 1));
            direction.Add("down-left", new Coords(1, -1));

            //racket directions
            direction.Add("up", new Coords(-1, 0));
            direction.Add("down", new Coords(1, 0));
            direction.Add("right", new Coords(0, 1));
            direction.Add("left", new Coords(0, -1));
        }

    }
}
