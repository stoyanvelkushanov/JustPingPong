using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public class UserKeyBoard:IUserInterface
    {
        public void ProccessInput()
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey();
                while (Console.KeyAvailable) Console.ReadKey();

                if (keyInfo.Key.Equals(ConsoleKey.DownArrow))
                {
                    if (onDownPressed != null)
                    {
                        this.onDownPressed(this, new EventArgs());
                    }
                }
                if (keyInfo.Key.Equals(ConsoleKey.UpArrow))
                {
                    if (onUpPressed != null)
                    {
                        this.onUpPressed(this, new EventArgs());
                    }
                }
            }
        }

        public event EventHandler onUpPressed;

        public event EventHandler onDownPressed;
    }
}
