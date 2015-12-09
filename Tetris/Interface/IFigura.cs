using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    interface IFigura
    {
        int[,] figuraMatrix{get; set;} //Матрица где храниться фигура

       // public int maxX; //размеры игрового поля
       // public int maxY;

        Color colorFigura{get; set;} //Цвет фигуры

        int X { get; set; } //Кординаты фигуры (верхний левый угол)
        int Y { get; set; }

        //public IFigura(int mX, int mY){}

        void MoveUp();
        void MoveDown();

        void MoveLeft();
        void MoveRight();

        void Rotate();
        void UnRotate();
    }
}
