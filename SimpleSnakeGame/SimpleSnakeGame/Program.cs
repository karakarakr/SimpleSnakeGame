using System;
using System.Collections.Generic;

namespace SimpleSnakeGame
{
    class Snake
    {
        public class Part
        {
            public int X, Y, oldX, oldY;
        }
        public int headX, headY;
        public List<Part> parts = new List<Part>();
    }
    class Program
    {
        enum stateM
        {
            STOP,
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        static int width = 40;
        static int height = 20;

        static Random rnd = new Random();

        static int appleX = rnd.Next(1, width - 1);
        static int appleY = rnd.Next(1, height - 1);

        static stateM state = stateM.STOP;

        static Snake snake = new Snake()
        {
            headX = width / 2,
            headY = height / 2,
            parts = new List<Snake.Part>()
            {
                new Snake.Part() { X = (width / 2) - 1, Y = height / 2, oldX = width / 2, oldY = height / 2 }
            }
        };

        static bool Game = true;
        static void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1) Console.Write("#");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(snake.headX, snake.headY);
            Console.Write("@");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < snake.parts.Count; i++)
            {
                Console.SetCursorPosition(snake.parts[i].X, snake.parts[i].Y);
                Console.Write("O");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(appleX, appleY);
            Console.Write("+");
        }
        static void Controler()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        state = stateM.UP;
                        break;
                    case ConsoleKey.DownArrow:
                        state = stateM.DOWN;
                        break;
                    case ConsoleKey.LeftArrow:
                        state = stateM.LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        state = stateM.RIGHT;
                        break;
                }
            }
        }
        static void Move(int X, int Y)
        {
            int oldX = snake.headX;
            int oldY = snake.headY;

            snake.headX = snake.headX + X;
            snake.headY = snake.headY + Y;

            for (int i = 0; i < snake.parts.Count; i++)
            {
                if (snake.headX == snake.parts[i].X && snake.headY == snake.parts[i].Y)
                {
                    Game = false;
                }
                if (i == 0)
                {
                    snake.parts[i].oldX = snake.parts[i].X;
                    snake.parts[i].oldY = snake.parts[i].Y;

                    snake.parts[i].X = oldX;
                    snake.parts[i].Y = oldY;
                }
                else
                {
                    snake.parts[i].oldX = snake.parts[i].X;
                    snake.parts[i].oldY = snake.parts[i].Y;

                    snake.parts[i].X = snake.parts[i - 1].oldX;
                    snake.parts[i].Y = snake.parts[i - 1].oldY;
                }
            }

            if (snake.headX == appleX && snake.headY == appleY)
            {
                appleX = rnd.Next(1, width - 1);
                appleY = rnd.Next(1, height - 1);

                snake.parts.Add(new Snake.Part()
                {
                    X = snake.parts.Last().X,
                    Y = snake.parts.Last().Y
                });
            }

            if (snake.headX == width - 1 || snake.headY == height - 1 || snake.headX == 0 || snake.headY == 0)
            {
                Game = false;
            }
        }
        static void Main(string[] args)
        {
            while (Game)
            {
                Draw();
                Controler();
                switch (state)
                {
                    case stateM.UP:
                        Move(0, -1);
                        break;
                    case stateM.DOWN:
                        Move(0, 1);
                        break;
                    case stateM.LEFT:
                        Move(-1, 0);
                        break;
                    case stateM.RIGHT:
                        Move(1, 0);
                        break;
                }
                Thread.Sleep(100);
            }
        }
    }
}
