using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris.classes
{
    [Serializable]
    class Matrix
    {
        //public bool gameOver = true;

        private MatrixElement[,] pole; //массив игрового поля для представления
        private MatrixElement[,] poleBefore; //массив последнего объединения
        
        public Matrix(int row, int cell)
        {
            pole = new MatrixElement[row, cell];
            poleBefore = new MatrixElement[row, cell];
        }

        public void initPole() //инит массив
        {
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                {
                    pole[i, j].value = 0; //all zero
                    pole[i, j].color = Color.Black;
                    poleBefore[i, j].value = 0; //all zero
                    poleBefore[i, j].color = Color.Black;
                }
            }
        }

        private void Copy(MatrixElement[,] ArraySource, ref MatrixElement[,] DestanationArray) //копирует массив
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DestanationArray[i, j].value = ArraySource[i, j].value;
                    DestanationArray[i, j].color = ArraySource[i, j].color;
                }
            }
        }

        public bool AddMoveFigure(IFigura figura) // временно добавляет фигуру в стакан
                                                 //true если  фигура может двигаться
        {
            int[,] poleFigur = figura.figuraMatrix;
            int X=figura.X;
            int X1 = X + poleFigur.GetLength(0)-1;
            int Y = figura.Y;
            int Y1 = Y + poleFigur.GetLength(1)-1;
            Color colorFigure = figura.colorFigura;

            Copy(poleBefore,ref pole);


            //объединение матриц
            int row = pole.GetLength(0);
            int cell = pole.GetLength(1);

            if (X1 >= cell) return false;
            if (Y == row || Y1>=row)
            {
                return false;
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cell; j++)
                { 
                    if(Y<=i && i<=Y1 && X<=j && j<=X1)
                    {
                        if (pole[i, j].value == 0 && poleFigur[j - X, i - Y] == 0) continue;
                        if (pole[i, j].value == 1 && poleFigur[j - X, i - Y] == 0) continue;
                        if (pole[i, j].value == 1 && poleFigur[j - X, i - Y] == 1)
                        {
                            Copy(poleBefore, ref pole);
                            return false; 
                        }
                        pole[i, j].value = 1;
                        pole[i, j].color = colorFigure;
                    }
                }
            }
            return true;
        }

        public void LastAddMoveFigure(IFigura figura) //ДОбавляет фигуру в стакан на всегда
        {
            int[,] poleFigur = figura.figuraMatrix;
            int X = figura.X;
            int X1 = X + poleFigur.GetLength(0) - 1;
            int Y = figura.Y;
            int Y1 = Y + poleFigur.GetLength(1) - 1;
            Color colorFigure = figura.colorFigura;
                     

            //объединение матриц
            int row = pole.GetLength(0);
            int cell = pole.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cell; j++)
                {
                    if (Y <= i && i <= Y1 && X <= j && j <= X1)
                    {
                        if (pole[i, j].value == 0 && poleFigur[j - X, i - Y] == 0) continue;
                        if (pole[i, j].value == 1 && poleFigur[j - X, i - Y] == 0) continue;
                        pole[i, j].value = 1;
                        pole[i, j].color = colorFigure;
                    }
                }
            }
            Copy(pole, ref poleBefore);
        }


        public int RowsComplete()//Удаляет полные строки и выдает их количество
        {
            int row = poleBefore.GetLength(0);
            int cell = poleBefore.GetLength(1);

            int rowsSum = 0;
            int k; //считает сколько в строке заполнено

            for (int i = 0; i < row; i++)
            {
                k = 0;
                for (int j = 0; j < cell; j++)
                {
                    if (poleBefore[i, j].value == 1) { k++; } else { break; }
                }
                //если нашли заполненую линию, то суммируем ее и удаляем
                if (k == cell) 
                {
                    for (int i2 = i; i2>0;i2--)
                    {
                        for (int j = 0; j < cell; j++)
                        {
                            poleBefore[i2, j].value = poleBefore[i2 - 1, j].value;
                            poleBefore[i2, j].color = poleBefore[i2 - 1, j].color;
                        }
                    }
                    rowsSum++;
                }
            }



            return rowsSum;
        }
              

        public MatrixElement[,] getPole()
        {
            return pole;
        }

        /*
        public override string ToString()
        {
            string poleToString="";
            string poleBeforeToString="";

            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                {
                    poleToString += pole[i, j].value.ToString() + " ";
                    poleToString += pole[i, j].color.ToArgb().ToString() + " ";
                    poleBeforeToString += poleBefore[i, j].value.ToString() + " ";
                    poleBeforeToString += poleBefore[i, j].color.ToArgb().ToString() + " ";
                     
                }
            }

            return poleToString+"{Next}"+poleBeforeToString;
        }

        public bool RestoreFromString(string s)
        {
            try
            {
                string[] split = s.Split(new string[] { "{Next}" }, StringSplitOptions.None);
                string[] splitPole = split[0].Split(new Char[] { ' ' });
                string[] splitPoleBefore = split[1].Split(new Char[] { ' ' });
                int k = 0;

                for (int i = 0; i < pole.GetLength(0); i++)
                {
                    for (int j = 0; j < pole.GetLength(1); j++)
                    {
                        pole[i, j].value = Convert.ToByte(splitPole[k]);
                        pole[i, j].color = Color.FromArgb( Convert.ToInt32( splitPole[k+1]));

                        poleBefore[i, j].value = Convert.ToByte(splitPoleBefore[k]);
                        poleBefore[i, j].color = Color.FromArgb(Convert.ToInt32(splitPoleBefore[k + 1]));

                        k += 2;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                initPole();
                return false;
            }

            
        }
         */
        
    }
}
