using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tetris.classes;


namespace Tetris
{
    class Board
    {
        private Graphics graph; 
        
        public int StakanRow; //размеры стакана
        public int StakanCell;

        public Board(Graphics g, int StakanRowSet, int StakanCellSet)
        {
            graph = g;
            StakanRow = StakanRowSet;
            StakanCell = StakanCellSet;
            DrawStakan();
        }

        public Board(Graphics g)
        {
            graph = g;
            StakanRow = 20;
            StakanCell = 10;
            DrawStakan();
        }


        ~Board() //Освобождаем ресурсы
        {
            graph.Dispose();
        }

        public void DrawStakan() //Рисует пустой стакан
        {
            graph.Clear(Color.Black);
            Pen myPen;
            myPen = new Pen(Color.Red);
            for (int i = 0; i < StakanRow+1;i++ )
            {
                graph.DrawLine(myPen, 0, i*20, 200, i*20);
            }
            for (int i = 0; i < StakanCell; i++)
            {
                graph.DrawLine(myPen, (i * 20), 0, (i * 20), 400);
            }
            myPen.Dispose();
        }

        public void LoadData(MatrixElement[,] pole)//Загружает и отрисовывает данные
        {
            for (int i = 0; i < StakanRow; i++)
            {
                for (int j = 0; j < StakanCell; j++)
                {
                    if (pole[i, j].value == 1)
                    {
                        SolidBrush myBrush = new SolidBrush(pole[i, j].color);
                        graph.FillRectangle(myBrush, 2 + (20 * j), 2 + (20 * i), 16, 16);
                        myBrush.Dispose();
                    }
                    else
                    {
                        graph.FillRectangle(Brushes.Black, 2 + (20 * j), 2 + (20 * i), 16, 16);
                    }
                    
                }
            }

        }

        
    }
}
