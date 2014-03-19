using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public class Coords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coords(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Coords operator +(Coords a, Coords b)
        {
            return new Coords((a.X + b.X), (a.Y + b.Y));
        }

        public static Coords operator -(Coords a, Coords b)
        {
            return new Coords((a.X - b.X), (a.Y - b.Y));
        }

        public static bool operator ==(Coords a, Coords b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }

        public static bool operator !=(Coords a, Coords b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() & this.Y;
        }
    }
}
