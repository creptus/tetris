using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris.classes
{
    [Serializable]
    class FiguraI:IFigura
    {
        public int[,] figuraMatrix{get; set;} 
        public Color colorFigura{get; set;}


        public int X{ get; set; } //Кординаты фигуры (верхний левый угол)

        public int Y{ get; set; }

        public int maxX; //размеры игрового поля
        public int maxY;

        private int maxSizeAray=4;

        public FiguraI(int mX, int mY)
        {
            maxX = mX;
            maxY = mY;
            X = 3;
            Y=0;
            figuraMatrix = new int[maxSizeAray, 1];
            //I
            for (int j = 0; j < maxSizeAray; j++)
            {
                figuraMatrix[j,0] = 1; 
            }
            colorFigura = Color.Aqua;

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
            if (X<maxX)
            {
                X += 1;
            }
        }
        public void Rotate() 
        {
            if (figuraMatrix.GetLength(1) == maxSizeAray)
            {
                figuraMatrix = new int[maxSizeAray, 1];
                //I
                for (int j = 0; j < maxSizeAray; j++)
                {
                    figuraMatrix[j, 0] = 1;
                }
            }
            else
            {
                figuraMatrix = new int[1, maxSizeAray];
                //I
                for (int j = 0; j < maxSizeAray; j++)
                {
                    figuraMatrix[0, j] = 1;
                }
            }

        }

        public void UnRotate()
        {
            Rotate();
        }

        public void MoveDown() 
        {
            if(Y<maxY) Y += 1;
        }

        public void MoveUp()
        {
            if (Y >0) Y -= 1;
        }

    }
}
