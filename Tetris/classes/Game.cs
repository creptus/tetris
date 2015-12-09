using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tetris.classes.Figures;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Tetris.classes
{
    class Game
    {
        private Board Stakan = null;
        private IFigura curentFigura = null;//Текущая фигура
        private Matrix pole = null; //Поле игры

        public bool isPause = false; //Игра на паузе

        public int linesInLevel = 2;

        public bool gameIsOver = false;

        public string pathToSave = @"C:\Test\";

        public int gamePoints = 0;
        public int gameLines = 0;
        public int gameLevel = 1;

        public Game(Graphics g)
        {
            Stakan = new Board(g, 20, 10);
            pole = new Matrix(20, 10);
            pathToSave=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Tetris\";
        }

        public bool RestoreGame()
        {
            if (RestoreCurentFigura() && RestorePole() && RestoreStatistic())
            {
                isPause = true;
                Stakan.DrawStakan();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReDrawStakan()
        {
            Stakan.DrawStakan();
            Stakan.LoadData(pole.getPole());
        }

        private bool RestorePole()
        {
            if (File.Exists(pathToSave + @"Stakan.txt"))
            {
                try
                {
                    Stream FiguraFileStream = File.OpenRead(pathToSave + @"Stakan.txt");
                    BinaryFormatter deserializer = new BinaryFormatter();
                    pole = (Matrix)deserializer.Deserialize(FiguraFileStream);
                    FiguraFileStream.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        private bool RestoreCurentFigura()
        {
            if (File.Exists(pathToSave + @"Figura.txt"))
            {
                try
                {
                    Stream FiguraFileStream = File.OpenRead(pathToSave + @"\Figura.txt");
                    BinaryFormatter deserializer = new BinaryFormatter();
                    curentFigura = (IFigura)deserializer.Deserialize(FiguraFileStream);
                    FiguraFileStream.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        private bool RestoreStatistic()
        {
            if (File.Exists(pathToSave + @"Stakan.txt"))
            {
                try
                {
                    StreamReader sr = new StreamReader(pathToSave + @"Game.txt");
                    string result = sr.ReadToEnd();
                    sr.Close();
                    string[] split = result.Split(new Char[] { ' ' });
                    gamePoints = Convert.ToInt32(split[0]);
                    gameLines = Convert.ToInt32(split[1]);
                    gameLevel = Convert.ToInt32(split[2]);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public void StartGame()
        {
            pole.initPole();
            DeleteSaveGame();
        }

        private IFigura CreateNewFigura()
        {
            Random rndFig = new Random();
            switch (rndFig.Next() % 6)
            {
                case 0:
                    return new FiguraI(Stakan.StakanCell, Stakan.StakanRow);
                case 1:
                    return new FiguraO(Stakan.StakanCell, Stakan.StakanRow);
                case 2:
                    return new FiguraJ(Stakan.StakanCell, Stakan.StakanRow);
                case 3:
                    return new FiguraL(Stakan.StakanCell, Stakan.StakanRow);
                case 4:
                    return new FiguraT(Stakan.StakanCell, Stakan.StakanRow);
                case 5:
                    return new FiguraS(Stakan.StakanCell, Stakan.StakanRow);
                case 6:
                    return new FiguraZ(Stakan.StakanCell, Stakan.StakanRow);
                default:
                    return null;
            }
        }

        public void TimerTick()
        {
            //if (curentFigura == null) { Environment.Exit(0); }
            if (gameIsOver) return;
            if (curentFigura == null) { curentFigura = CreateNewFigura(); }

            if (pole.AddMoveFigure(curentFigura))
            {
                Stakan.LoadData(pole.getPole());
                curentFigura.MoveDown();
            }
            else
            {
                SaveCreateFigure();
            }


        }

        private void SaveCreateFigure() //Когда достигнуто дно - сохраняет фигуру в стакане и создает новую, начисляет очки
        {
            curentFigura.MoveUp();
            if (curentFigura.Y == 0)
            {
                curentFigura.MoveUp();
                if (curentFigura.Y == 0)
                {
                    gameIsOver = true;
                }
                else
                {
                    curentFigura.MoveDown();
                }
            }
            pole.LastAddMoveFigure(curentFigura);
            //начисление очков
            int lines = pole.RowsComplete();
            gameLines += lines;
            gameLevel = (gameLines / linesInLevel) + 1;
            switch (lines)
            {
                case 1:
                    gamePoints += 100;
                    break;
                case 2:
                    gamePoints += 300;
                    break;
                case 3:
                    gamePoints += 700;
                    break;
                case 4:
                    gamePoints += 1500;
                    break;
                default:
                    break;
            }
            //
            curentFigura = null;
            //curentFigura = CreateNewFigura();
            TimerTick();
        }

        public void keyArrowLeft()
        {
            curentFigura.MoveLeft();
            if (pole.AddMoveFigure(curentFigura))
            {
                Stakan.LoadData(pole.getPole());
            }
            else
            {
                curentFigura.MoveRight();
            }
        }


        public void keyArrowUp()
        {
            curentFigura.Rotate();
            if (pole.AddMoveFigure(curentFigura))
            {
                Stakan.LoadData(pole.getPole());
            }
            else
            {
                curentFigura.UnRotate();
            }
        }


        public void keyArrowRight()
        {
            curentFigura.MoveRight();
            if (pole.AddMoveFigure(curentFigura))
            {
                Stakan.LoadData(pole.getPole());
            }
            else
            {
                curentFigura.MoveLeft();
            }
        }

        public void keyArrowDown()
        {
            curentFigura.MoveDown();
            if (pole.AddMoveFigure(curentFigura))
            {
                Stakan.LoadData(pole.getPole());
            }
            else
            {
                SaveCreateFigure();
            }
        }

        public void SaveGame()
        {
            if (gameIsOver)
            {
                DeleteSaveGame();
            }
            else
            {
                //save serilaze objects
                SaveCurentFigura();
                SavePole();
                SaveStatistic();


            }
        }

        private void SaveStatistic()//сохраняет статистику игры
        {
            string statistic = gamePoints.ToString() + " " + gameLines.ToString() + " " + gameLevel.ToString();
            StreamWriter sw = new StreamWriter(pathToSave + @"Game.txt");
            sw.Write(statistic);
            sw.Close();
        }

        private void SaveCurentFigura()//сохраняет фтекущую фигуру
        {
            if (curentFigura != null)
            {

                Stream FileStream = File.Create(pathToSave + @"Figura.txt");
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, curentFigura);
                FileStream.Close();
            }

        }

        private void SavePole()//сохраняет стакан
        {
            if (pole != null)
            {
                Stream FileStream = File.Create(pathToSave + @"Stakan.txt");
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, pole);
                FileStream.Close();
            }

        }

        private void DeleteSaveGame()//удаляет файлы сохранненой игры
        {
            deleteFile(pathToSave + @"Stakan.txt");
            deleteFile(pathToSave + @"Figura.txt");
            deleteFile(pathToSave + @"Game.txt");
        }

        private void deleteFile(string fileName)//удаляет файл
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }


    }
}
