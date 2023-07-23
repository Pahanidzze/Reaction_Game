using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Learning;
using SFML.Graphics;
using SFML.Window;

namespace Reaction_Game
{
    class Program : Game
    {
        static string backGroundMusic = LoadMusic("bg_music.wav");

        static void Main()
        {
            uint winWeight = 800;
            uint winHeihgt = 600;
            InitWindow(winWeight, winHeihgt, "Window");
            SetFont("comic.ttf");
            PlayMusic(backGroundMusic, 20);
            Game();
        }

        static unsafe void Game()
        {
            bool active;
            int innerCyrcleSize;
            int outerCyrcleSize;
            int score = 0;
            int highScore = 0;
            Color innerCyrcleColor = ColorSwitch(Color.Black);
            ResetParameters(&active, &innerCyrcleSize, &outerCyrcleSize, &score, &highScore);
            bool keyPressed = false;
            while(true)
            {
                DispatchEvents();
                if (!keyPressed)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !active)
                    {
                        active = true;
                        keyPressed = true;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    {
                        outerCyrcleSize = innerCyrcleSize;
                        innerCyrcleSize = 0;
                        keyPressed = true;
                        innerCyrcleColor = ColorSwitch(innerCyrcleColor);
                    }
                }
                else if (!Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    keyPressed = false;
                }
                if (innerCyrcleSize >= outerCyrcleSize)
                {
                    ResetParameters(&active, &innerCyrcleSize, &outerCyrcleSize, &score, &highScore);
                }
                else if (active)
                {
                    score++;
                    innerCyrcleSize++;
                }
                ClearWindow();
                FillGame(active, innerCyrcleColor, score, highScore, innerCyrcleSize, outerCyrcleSize);
                DisplayWindow();
                Delay(1);
            }
        }

        static unsafe void ResetParameters(bool* active, int* innerSize, int* outerSize, int* score, int* highScore)
        {
            if (*score > *highScore) *highScore = *score;
            *active = false;
            *innerSize = 0;
            *outerSize = 250;
            *score = 0;
        }

        static void FillGame(bool active, Color innerColor, int score, int highScore, int innerSize, int outerSize)
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
            FillCircle(500, 330, innerSize);
        }

        static Color ColorSwitch(Color color)
        {
            var Rand = new Random();
            Color newColor = color;
            int newColorIndex;
            while(newColor == color)
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
