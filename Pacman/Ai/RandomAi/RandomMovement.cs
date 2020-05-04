using System;

namespace Pacman.Ai.RandomAi
{
    public class RandomMovement
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        public void Start(Pacman p) {

            myTimer.Tick += (sender, e) => TimerEventProcessor(sender, e, p);
            myTimer.Interval = 1;
            myTimer.Start();

        }

        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs, Pacman p)
        {
            Random rnd = new Random();
            var rd = rnd.Next(4);

            switch(rd)
            {
                case 0: p.nextDirection = 1; p.MovePacman(1); break;
                case 1: p.nextDirection = 2;  p.MovePacman(2); break;
                case 2: p.nextDirection = 3;  p.MovePacman(3); break;
                case 3: p.nextDirection = 4; p.MovePacman(4); break;
            }

        }

    }
}
