using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustPingPong
{
    public class ConsoleRenderer : IRenderer
    {

        public int worldRows;
        public int worldCols;
        public char[,] world;

        public ConsoleRenderer(int worldRows, int worldCols)
        {
            this.worldRows = worldRows;
            this.worldCols = worldCols;
            this.world = new char[this.worldRows, this.worldCols];
        }


        public void EnqueForRendering(Ball ball, PlayerRacket player, ComputerRacket computer)
        {
            //add ball for rendering
            if (ball.Position.X >= 0 && ball.Position.X < worldRows  && ball.Position.Y > 1 && ball.Position.Y < worldCols - 2)
            {
                world[ball.Position.X, ball.Position.Y] = Ball.ballSymbol;
            }

            //add player racket for rendering
            if (player.CurrentPosition.X >= 0 && player.CurrentPosition.X + player.RacketLength < worldRows - 1)
            {
                for (int i = player.CurrentPosition.X; i < (player.RacketLength + player.CurrentPosition.X); i++)
                {
                    world[i, 0] = Racket.symbol;
                    world[i, 1] = Racket.symbol;
                }
            }

            //add computer racket for rendering
            if (computer.CurrentPosition.X >= 0 && computer.CurrentPosition.X + computer.RacketLength < worldRows - 1)
            {
                for (int i = computer.CurrentPosition.X; i < (computer.RacketLength + computer.CurrentPosition.X); i++)
                {
                    world[i, worldCols - 1] = Racket.symbol;
                    world[i, worldCols - 2] = Racket.symbol;
                }
            }

            string result = Engine.playerScore.CurrentScore + " " + "-" + " " + Engine.computerScore.CurrentScore;

            for (int i = (this.worldCols / 2), k=0; i < result.Length + (this.worldCols/2); i++,k++)
            {
                world[this.worldRows - 1, i] = result[k];
            }
        }

        public void RenderAll()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            StringBuilder scene = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int row = 0; row < worldRows; row++)
            {
                for (int col = 0; col < worldCols; col++)
                {
                    scene.Append(world[row, col]);
                }
                scene.Append(Environment.NewLine);
            }
            Console.WriteLine(scene.ToString());
            Console.ResetColor();
        }

        public void ClearAll()
        {
            for (int row = 0; row < worldRows; row++)
            {
                for (int col = 0; col < worldCols; col++)
                {
                    world[row, col] = ' ';
                }
                
            }
        }
    }
}
