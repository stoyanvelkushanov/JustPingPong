using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public struct Score:IUpdateble
    {
        public int CurrentScore { get; set; }

        public void Update()
        {
            this.CurrentScore += 1;
        }
    }
}
