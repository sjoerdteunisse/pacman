using Pacman.Ai;
using Pacman.Ai.AStar;
using Pacman.Ai.RandomAi;
using Pacman.Ai.Reinforced;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Form1 : Form
    {
        public static GameBoard gameboard = new GameBoard();
        public static Food food = new Food();
        public static Pacman pacman = new Pacman();
        public static Ghost ghost = new Ghost();
        public static Player player = new Player();
        public static HighScore highscore = new HighScore();
        private static FormElements formelements = new FormElements();


        public Form1()
        {
            InitializeComponent();
            SetupGame();

            GeneticLearning glr = new GeneticLearning(food, pacman, SetupGame);
            glr.Start(pacman, formelements, gameboard, food);
        }

        public void SetupGame()
        {
            food.RemoveFoodImages(this);
            player = new Player();

            pacman.SetFormElement(formelements);

            // Create Game Board
            gameboard.CreateBoardImage(this, 1);

            // Create Board Matrix
            Tuple<int, int> PacmanStartCoordinates = gameboard.InitialiseBoardMatrix(1);

            player.RemoveLives(this);

            // Create Player
            player.CreatePlayerDetails(this);
            player.CreateLives(this);

            // Create Form Elements
            formelements.CreateFormElements(this);

            // Create High Score
            highscore.CreateHighScore(this);

            // Create Food
            food.CreateFoodImages(this);

            // Create Ghosts
            ghost.CreateGhostImage(this);

            // Create Pacman`
            pacman.RemovePacmanImage(this, PacmanStartCoordinates.Item1, PacmanStartCoordinates.Item2);

            pacman.CreatePacmanImage(this, PacmanStartCoordinates.Item1, PacmanStartCoordinates.Item2);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Up: pacman.nextDirection = 1; pacman.MovePacman(1); break;
                case Keys.Right: pacman.nextDirection = 2; pacman.MovePacman(2); break;
                case Keys.Down: pacman.nextDirection = 3; pacman.MovePacman(3); break;
                case Keys.Left: pacman.nextDirection = 4; pacman.MovePacman(4); break;
            }
        }

    }
}
