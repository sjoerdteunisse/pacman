using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Food
    {
        public PictureBox[,] FoodImage = new PictureBox[30, 27];
        public List<Point> foodLocations = new List<Point>();

        public int Amount = 0;
        public readonly int TotalAmount = 244;

        private const int FoodScore = 10;
        private const int SuperFoodScore = 50;

        public void RemoveFoodImages(Form formInstnace)
        {
            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < 27; x++)
                {
                    formInstnace.Controls.Remove(FoodImage[y, x]);
                    foodLocations.Remove(new Point(x, y));
                }
            }
            Amount = 0;
        }

        public void CreateFoodImages(Form formInstance)
        {
            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < 27; x++)
                {
                    if (Form1.gameboard.Matrix[y, x] == 1 || Form1.gameboard.Matrix[y, x] == 2)
                    {
                        foodLocations.Add(new Point(x, y));
                        FoodImage[y, x] = new PictureBox();
                        FoodImage[y, x].Name = "FoodImage" + Amount.ToString();
                        FoodImage[y, x].SizeMode = PictureBoxSizeMode.AutoSize;
                        FoodImage[y, x].Location = new Point(x * 16 - 1, y * 16 + 47);
                        if (Form1.gameboard.Matrix[y, x] == 1)
                        {
                            FoodImage[y, x].Image = Properties.Resources.Block_1;
                            Amount++;
                        }
                        else
                        {
                            FoodImage[y, x].Image = Properties.Resources.Block_2;
                        }
                        formInstance.Controls.Add(FoodImage[y, x]);
                        FoodImage[y, x].BringToFront();

                    }
                }
            }
        }

        public void EatFood(int x, int y)
        {
            // Eat food
            foodLocations.Remove(new Point(x, y));

            FoodImage[x, y].Visible = false;
            Form1.gameboard.Matrix[x, y] = 0;
            Form1.player.UpdateScore(FoodScore);
            Amount--;
            if (Amount < 1) { Form1.player.LevelComplete(); }
            //Form1.audio.Play(1);
        }

        public void EatSuperFood(int x, int y)
        {
            // Eat food
            FoodImage[x, y].Visible = false;
            Form1.gameboard.Matrix[x, y] = 0;
            Form1.player.UpdateScore(SuperFoodScore);
            Form1.ghost.ChangeGhostState();
        }
    }
}
