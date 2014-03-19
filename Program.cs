using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustPingPong
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserInterface keyboard;
            Engine engine;

            InitializeGame(out keyboard, out engine);

            
            keyboard.onDownPressed += (sender, eventInfo) =>
                {
                    engine.MovePlayerRacketDown();
                };

            keyboard.onUpPressed += (sender, eventInfo) =>
                {
                    engine.MovePlayerRacketUp();
                };

            engine.Run();
        }

        private static void InitializeGame(out IUserInterface keyboard, out Engine engine)
        {
            Console.WriteLine("Set game speed: ");
            int speed = int.Parse(Console.ReadLine());
            Console.WriteLine("Set computer IQ [10-100] ");
            int computerIQ = int.Parse(Console.ReadLine());
            Console.WriteLine("Set player's racket Length: ");
            int PlayerRacketLength = int.Parse(Console.ReadLine());
            Console.WriteLine("Set computer's racket Length: ");
            int ComputerRacketLength = int.Parse(Console.ReadLine());

            ConsoleRenderer renderer = new ConsoleRenderer(50, 79);
            PlayerRacket player = new PlayerRacket(new Coords(7, 0), PlayerRacketLength);
            ComputerRacket computer = new ComputerRacket(new Coords(15, renderer.worldCols - 1), ComputerRacketLength);
            Ball ball = new Ball(new Coords(19, renderer.worldCols - 3), new Coords(1, 1));
            keyboard = new UserKeyBoard();

            engine = new Engine(renderer, keyboard, speed, computerIQ);
            engine.AddObject(ball);
            engine.AddObject(player);
            engine.AddObject(computer);

            Console.BufferHeight = renderer.worldRows + 2;
            Console.BufferWidth = renderer.worldCols + 1;
            Console.WindowHeight = renderer.worldRows + 2;
            Console.WindowWidth = renderer.worldCols;
        }
    }
}
