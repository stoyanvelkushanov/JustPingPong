using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace JustPingPong
{
    public class Engine
    {
        private int sleepTime;

        ConsoleRenderer renderer;
       public IUserInterface userinterface;
        PlayerRacket playerRacket;
        ComputerRacket computerRacket;
        Ball ball;
        Directions directions;
        List<object> gameObjects;
        public static Score playerScore;
        public static Score computerScore;
        SoundPlayer hitTheBallSound;
        SoundPlayer scoreSound;
        int computerIQ;

        public Engine(ConsoleRenderer renderer, IUserInterface userinterface, int sleepTime, int computerIQ)
        {
            string pathToDesctop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string hitSoundFileName = "\\misc124.wav";
            string outSoundFileName = "\\whistle.wav";
            string pathPlusFileNameHitSound = pathToDesctop + hitSoundFileName;
            string pathPlusFileNameScoreSound = pathToDesctop + outSoundFileName;

            this.renderer = renderer;
            this.userinterface = userinterface;
            this.gameObjects = new List<object>();
            this.directions = new Directions();
            this.sleepTime = sleepTime;
            playerScore = new Score();
            computerScore = new Score();
            this.hitTheBallSound = new SoundPlayer(pathPlusFileNameHitSound);
            this.scoreSound = new SoundPlayer(pathPlusFileNameScoreSound);
            this.computerIQ = computerIQ;
        }

        public Engine()
        {
        }

        public void AddObject(Object obj)
        {
            this.gameObjects.Add(obj);
        }

        public void MovePlayerRacketUp()
        {
            if (this.playerRacket.CurrentPosition.X > 0)
            {
                this.playerRacket.CurrentPosition += directions.direction["up"];
            }
        }

        public void MovePlayerRacketDown()
        {
            if (this.playerRacket.CurrentPosition.X + this.playerRacket.RacketLength < this.renderer.worldRows - 2)
            {
                this.playerRacket.CurrentPosition += directions.direction["down"];
            }
        }

        public void Run()
        {
            while (true)
            {
                renderer.RenderAll();

                this.renderer.ClearAll();

                this.ball = (this.gameObjects[0] as Ball);
                this.playerRacket = (this.gameObjects[1] as PlayerRacket);
                this.computerRacket = (this.gameObjects[2] as ComputerRacket);

                userinterface.ProccessInput();

                renderer.EnqueForRendering(this.ball, this.playerRacket, this.computerRacket);

                System.Threading.Thread.Sleep(this.sleepTime);

                GiveBallDirection(ball);

                CheckForResult(ball);

                ComputerRackerAI(ball.Speed);

                ball.Update();
            }
            

        }

        private void CheckForResult(Ball ball)
        {
            int PlayerLastPoints = playerScore.CurrentScore;
            int ComputerLastPoints = computerScore.CurrentScore;

            if (ball.Speed.Y == -1 && ball.Position.Y <= 0 && ball.Position.X < this.playerRacket.CurrentPosition.X) 
            {
                computerScore.Update();
            }
            else if (ball.Speed.Y == -1 && ball.Position.Y <= 0 && ball.Position.X >
                     (this.playerRacket.CurrentPosition.X + this.playerRacket.RacketLength))
            {
                computerScore.Update();
            }
            else if (ball.Speed.Y == 1 && ball.Position.Y >= this.renderer.worldCols
                        && ball.Position.X < this.computerRacket.CurrentPosition.X)             
            {
                playerScore.Update();
            }
            else if (ball.Speed.Y == 1 && ball.Position.Y >= this.renderer.worldCols
                        && ball.Position.X > (this.computerRacket.CurrentPosition.X + this.computerRacket.RacketLength))
            {
                playerScore.Update();
            }

            if (playerScore.CurrentScore != PlayerLastPoints || computerScore.CurrentScore != ComputerLastPoints)
            {
                ball.Position = new Coords(this.renderer.worldRows / 2, this.renderer.worldCols / 2);
                //scoreSound.Play();
            }
        }

        private void ComputerRackerAI(Coords BallForceDirection)
        {
            Random random = new Random();

            int number = random.Next(1, 100);

            if (number <= computerIQ)
            {
                if (BallForceDirection.X == -1 && this.computerRacket.CurrentPosition.X > 0)
                {
                    computerRacket.CurrentPosition += directions.direction["up"];
                }

                else if (BallForceDirection.X == 1 && (this.computerRacket.CurrentPosition.X
                        + this.computerRacket.RacketLength) < this.renderer.worldRows - 2)
                {
                    computerRacket.CurrentPosition += directions.direction["down"];
                }
            }
        }

        private void GiveBallDirection(Ball ball)
        {
            int playerRacketStartXcoord = this.playerRacket.CurrentPosition.X;
            int playerRacketEndXcoord = this.playerRacket.CurrentPosition.X + this.playerRacket.RacketLength;

            int computerRacketStartXCoord = this.computerRacket.CurrentPosition.X;
            int computerRacketEndXCoord = this.computerRacket.CurrentPosition.X + this.computerRacket.RacketLength;

            int playerRacketMiddlePoint = this.playerRacket.CurrentPosition.X + (this.playerRacket.RacketLength / 2);
            int computerRacketMiddlePoint = this.computerRacket.CurrentPosition.X + (this.computerRacket.RacketLength / 2);

            //collision with top and bottom

            if (ball.Speed.Y == 1 && ball.Position.X == 0)
            {
                ball.Speed = directions.direction["down-right"];
            }
            else if (ball.Speed.Y == 1 && ball.Position.X == this.renderer.worldRows - 1)
            {
                ball.Speed = directions.direction["up-right"];
            }
            else if (ball.Speed.Y == -1 && ball.Position.X == 0)
            {
                ball.Speed = directions.direction["down-left"];
            }
            else if (ball.Speed.Y == -1 && ball.Position.X == this.renderer.worldRows - 1)
            {
                ball.Speed = directions.direction["up-left"];
            }

            //collision with player

            else if (ball.Speed.Y == -1 && (ball.Speed.X == 1 || ball.Speed.X == -1) && ball.Position.X >= playerRacketStartXcoord - 1
                && ball.Position.X <= playerRacketMiddlePoint && ball.Position.Y == 2)
                        
            {
                ball.Speed = directions.direction["up-right"];
                //this.hitTheBallSound.Play();
            }

            else if (ball.Speed.Y == -1 && (ball.Speed.X == -1 || ball.Speed.X == 1) && ball.Position.X >= playerRacketMiddlePoint
                && ball.Position.X <= playerRacketEndXcoord + 1 && ball.Position.Y == 2)
                        
            {
                ball.Speed = directions.direction["down-right"];
                //this.hitTheBallSound.Play();
            }

            //collision with computer

            else if (ball.Speed.Y == 1 && (ball.Speed.X == 1 || ball.Speed.X == -1) && ball.Position.X >= computerRacketStartXCoord - 1
                && ball.Position.X <= computerRacketMiddlePoint && ball.Position.Y == renderer.worldCols - 3)
            {
                ball.Speed = directions.direction["up-left"];
               // this.hitTheBallSound.Play();
            }

            else if (ball.Speed.Y == 1 && (ball.Speed.X == 1 || ball.Speed.X == -1) && ball.Position.X >= computerRacketMiddlePoint
                && ball.Position.X <= computerRacketEndXCoord + 1 && ball.Position.Y == renderer.worldCols - 3)
            {
                ball.Speed = directions.direction["down-left"];
               // this.hitTheBallSound.Play();
            }
        }
    }
}
