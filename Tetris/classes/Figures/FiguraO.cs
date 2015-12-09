using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris.classes.Figures
{
    [Serializable]
    class FiguraO : IFigura
    {
        public int[,] figuraMatrix { get; set; }
        public Color colorFigura { get; set; }


        public int X { get; set; } //Кординаты фигуры (верхний левый угол)

        public int Y { get; set; }

        public int maxX; //размеры игрового поля
        public int maxY;

        private int maxSizeAray = 2;

        public FiguraO(int mX, int mY)
        {
            maxX = mX;
            maxY = mY;
            X = 3;
            Y = 0;
            figuraMatrix = new int[maxSizeAray, 2];
            
            for (int j = 0; j < maxSizeAray; j++)
            {
                for (int i = 0; i < maxSizeAray; i++)
                {
                    figuraMatrix[i, j] = 1;
                }
            }
            colorFigura = Color.Blue;

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
            //nothing

        }

        public void UnRotate()
        {
            //nothing
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
