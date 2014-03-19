using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public interface IRenderer
    {
        void EnqueForRendering(Ball ball, PlayerRacket player, ComputerRacket computer);

        void RenderAll();

        void ClearAll();
    }
}
