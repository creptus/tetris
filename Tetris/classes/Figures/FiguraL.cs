using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris.classes.Figures
{
    [Serializable]
    class FiguraL : IFigura
    {
        public int[,] figuraMatrix { get; set; }
        public Color colorFigura { get; set; }

        private int fasaNext;// Фаза поворота 1..4


        public int X { get; set; } //Кординаты фигуры (верхний левый угол)

        public int Y { get; set; }

        public int maxX; //размеры игрового поля
        public int maxY;

        private int maxSizeAray = 3;

        public FiguraL(int mX, int mY)
        {
            maxX = mX;
            maxY = mY;
            X = 3;
            Y = 0;
            fasaNext = 2;
            figuraMatrix = new int[maxSizeAray, 2];
            for (int j = 0; j < maxSizeAray; j++)
            {
                figuraMatrix[j, 0] = 0;
                figuraMatrix[j, 1] = 1;
            }
            figuraMatrix[maxSizeAray - 1, 0] = 1;
            colorFigura = Color.Green;

        }


        public void MoveLeft()
        {
            if (X > 0)
            {
                X -= 1;
            }
        }

        public void MoveRight()
        {
            if (X < maxX)
            {
                X += 1;
            }
        }
        public void Rotate()
        {
            if (fasaNext == 0) fasaNext = 4;
            if (fasaNext <0) fasaNext = 3;
            switch (fasaNext)
            {
                case 1:
                    figuraMatrix = new int[maxSizeAray, 2];
                    for (int j = 0; j < maxSizeAray; j++)
                    {
                        figuraMatrix[j, 0] = 0;
                        figuraMatrix[j, 1] = 1;
                    }
                    figuraMatrix[maxSizeAray - 1, 0] = 1;
                    break;
                case 2:
                    figuraMatrix = new int[2, maxSizeAray];
                    for (int j = 0; j < maxSizeAray; j++)
                    {
                        figuraMatrix[1, j] = 0;
                        figuraMatrix[0, j] = 1;
                    }
                    figuraMatrix[1, maxSizeAray - 1] = 1;
                    break;
                case 3:
                    figuraMatrix = new int[maxSizeAray, 2];
                    for (int j = 0; j < maxSizeAray; j++)
                    {
                        figuraMatrix[j, 0] = 1;
                        figuraMatrix[j, 1] = 0;
                    }
                    figuraMatrix[0, 1] = 1;
                    break;
                case 4:
                default:
                    figuraMatrix = new int[2, maxSizeAray];
                    for (int j = 0; j < maxSizeAray; j++)
                    {
                        figuraMatrix[1, j] = 1;
                        figuraMatrix[0, j] = 0;
                    }
                    figuraMatrix[0, 0] = 1;
                    break;
            }
            fasaNext++;
            if (fasaNext > 4) fasaNext = 1;

        }

        public void UnRotate()
        {
            fasaNext -= 2;
            Rotate();
        }

        public void MoveDown()
        {
            if (Y < maxY) Y += 1;
        }

        public void MoveUp()
        {
            if (Y > 0) Y -= 1;
        }

    }
}
