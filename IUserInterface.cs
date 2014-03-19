using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public interface IUserInterface
    {
        event EventHandler onUpPressed;

        event EventHandler onDownPressed;

        void ProccessInput();
    }
}
