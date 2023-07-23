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
        static uint winH = 600;
        static uint winW = 800;

        static string BGM = LoadMusic("bg_music.wav");

        static void Main()
        {
            InitWindow(winW, winH, "Window");
            SetFont("comic.ttf");
            PlayMusic(BGM, 20);
            Game();
        }

        static unsafe void Game()
        {
            bool active;
            int innerSize;
            int outerSize;
            int score = 0;
            int highScore = 0;
            ResetParameters(&active, &innerSize, &outerSize, &score, &highScore);
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
                    } else if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    {
                        outerSize = innerSize;
                        innerSize = 0;
                        keyPressed = true;
                    }
                } else if (!Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    keyPressed = false;
                }
                if (innerSize >= outerSize)
                {
                    ResetParameters(&active, &innerSize, &outerSize, &score, &highScore);
                } else if (active)
                {
                    score++;
                    innerSize++;
                }
                ClearWindow();
                FillGame(active, Color.Red, score, highScore, innerSize, outerSize);
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
            } else
            {
                DrawText(20, 120, "Остановить круг: ПРОБЕЛ", 21);
            }
            DrawText(10, 560, $"Лучший результат: {highScore}", 24);
            DrawText(10, 525, $"Очки: {score}", 24);
            FillCircle(500, 330, outerSize);
            SetFillColor(innerColor);
            FillCircle(500, 330, innerSize);
        }
    }
}
