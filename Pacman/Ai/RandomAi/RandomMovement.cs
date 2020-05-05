using System;

namespace Pacman.Ai.RandomAi
{
    public class RandomMovement : ArtificialMovement
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs, Pacman p, Food f)
        {
            Random rnd = new Random();
            var rd = rnd.Next(4);

            switch (rd)
            {
                case 0: p.nextDirection = 1; p.MovePacman(1); break;
                case 1: p.nextDirection = 2; p.MovePacman(2); break;
                case 2: p.nextDirection = 3; p.MovePacman(3); break;
                case 3: p.nextDirection = 4; p.MovePacman(4); break;
            }
        }

        public override void Start(Pacman pacman, FormElements formElements, GameBoard gameBoard, Food food)
        {
            myTimer.Tick += (sender, e) => TimerEventProcessor(sender, e, pacman, food);
            myTimer.Interval = 1;
            myTimer.Start();
        }
    }
}
