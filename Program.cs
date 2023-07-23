using SFML.Graphics;
using SFML.Learning;
using SFML.Window;
using System;

namespace Reaction_Game
{
    internal class Program : Game
    {
        private static readonly string backGroundMusic = LoadMusic("bg_music.wav");
        private static readonly string actionSound = LoadSound("ActionSound.wav");
        private const int initialScore = 0;
        private const uint winWeight = 800;
        private const uint winHeihgt = 600;

        private static void Main()
        {
            InitWindow(winWeight, winHeihgt, "Window");
            SetFont("comic.ttf");
            PlayMusic(backGroundMusic, 20);
            Game();
        }

        private static unsafe void Game()
        {
            bool active;
            double innerCyrcleSize;
            int outerCyrcleSize;
            int score = initialScore;
            int highScore = initialScore;
            Color innerCyrcleColor = ColorSwitch(Color.Black);
            ResetParameters(&active, &innerCyrcleSize, &outerCyrcleSize, &score, &highScore);
            double speed;
            bool keyPressed = false;
            while (true)
            {
                DispatchEvents();
                if (innerCyrcleSize >= outerCyrcleSize)
                {
                    ResetParameters(&active, &innerCyrcleSize, &outerCyrcleSize, &score, &highScore);
                }
                if (!keyPressed)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !active)
                    {
                        active = true;
                        keyPressed = true;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    {
                        PlaySound(actionSound, 20);
                        outerCyrcleSize = (int)Math.Ceiling(innerCyrcleSize);
                        innerCyrcleSize = 0;
                        keyPressed = true;
                        innerCyrcleColor = ColorSwitch(innerCyrcleColor);
                    }
                }
                else if (!Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    keyPressed = false;
                }
                if (active)
                {
                    score++;
                    speed = 1 + score / 500 * 0.3; // 1 + int / int * double
                    innerCyrcleSize += speed;
                }
                ClearWindow();
                FillGame(active, innerCyrcleColor, score, highScore, innerCyrcleSize, outerCyrcleSize);
                DisplayWindow();
                Delay(1);
            }
        }

        private static unsafe void ResetParameters(bool* active, double* innerSize, int* outerSize, int* score, int* highScore)
        {
            if (*score > *highScore) *highScore = *score;
            *active = false;
            *innerSize = 0;
            *outerSize = 250;
            *score = initialScore;
        }

        private static void FillGame(bool active, Color innerColor, int score, int highScore, double innerSize, int outerSize)
        {
            SetFillColor(255, 255, 255);
            DrawText(10, 0, "Инструкция:", 32);
            DrawText(20, 40, "Не выходите за пределы круга!", 21);
            DrawText(10, 80, "Клавиши:", 32);
            if (!active)
            {
                DrawText(20, 120, "Начать игру: ПРОБЕЛ", 21);
            }
            else
            {
                DrawText(20, 120, "Остановить круг: ПРОБЕЛ", 21);
            }
            DrawText(10, 560, $"Лучший результат: {highScore}", 24);
            DrawText(10, 525, $"Очки: {score}", 24);
            FillCircle(500, 330, outerSize);
            SetFillColor(innerColor);
            FillCircle(500, 330, (int)innerSize);
        }

        private static Color ColorSwitch(Color color)
        {
            Random Rand = new Random();
            Color newColor = color;
            int newColorIndex;
            while (newColor == color)
            {
                newColorIndex = Rand.Next(6);
                if (newColorIndex == 0) newColor = Color.Blue;
                else if (newColorIndex == 1) newColor = Color.Green;
                else if (newColorIndex == 2) newColor = Color.Red;
                else if (newColorIndex == 3) newColor = Color.Cyan;
                else if (newColorIndex == 4) newColor = Color.Magenta;
                else newColor = Color.Yellow;
            }
            return newColor;
        }
    }
}
