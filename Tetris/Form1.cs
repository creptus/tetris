using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tetris.classes;


namespace Tetris
{
    public partial class Form1 : Form
    {
        private Game tetrisGame;

        public Form1()
        {
            InitializeComponent();
        }



        private void StartButton_Click(object sender, EventArgs e)
        {
            tetrisGame = new Game(pictureBox1.CreateGraphics());
            tetrisGame.StartGame();
            PauseButton.Enabled = true;
            timer1.Enabled = true;
            tetrisGame.TimerTick();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tetrisGame.gameIsOver)
            {
                PauseButton.Enabled = false;
                timer1.Enabled = false;
                MessageBox.Show("Игра окончена!", "Информация",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                EnterForm childEnterForm = new EnterForm(this,tetrisGame.gamePoints);
                childEnterForm.MdiParent = this.MdiParent;
                childEnterForm.Show();
                this.Enabled = false;
            }
            else
            {
                UpdateStatistic();
                tetrisGame.TimerTick();
            }
        }

        private void UpdateStatistic()
        {
            label3.Text = tetrisGame.gamePoints.ToString();
            label5.Text = tetrisGame.gameLines.ToString();
            //Если уровень поменялся, то ускоряем таймер
            if (label7.Text != tetrisGame.gameLevel.ToString())
            {
                timer1.Stop();
                if (timer1.Interval - 50 * tetrisGame.gameLevel < 101)
                {
                    timer1.Interval = 100;
                }
                else
                {
                    timer1.Interval = timer1.Interval - 50 * tetrisGame.gameLevel;
                }
                timer1.Start();
            }

            label7.Text = tetrisGame.gameLevel.ToString();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled == true)
            {
                tetrisGame.isPause = false;
                PauseButton.Text = "Pause";
                tetrisGame.ReDrawStakan();
            }
            else
            {
                tetrisGame.isPause = true;
                PauseButton.Text = "Continue";
            }
            pictureBox1.Focus();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (tetrisGame.isPause || tetrisGame.gameIsOver) return;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    tetrisGame.keyArrowLeft();
                    break;
                case Keys.Right:
                    tetrisGame.keyArrowRight();
                    break;
                case Keys.Down:
                    tetrisGame.keyArrowDown();
                    timer1.Stop();
                    timer1.Start();
                    UpdateStatistic();
                    break;
                case Keys.Up:
                    tetrisGame.keyArrowUp();
                    break;
                default:
                    break;

            }
        }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StartButton_Click(sender, e);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            tetrisGame.SaveGame();
            Environment.Exit(0);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            tetrisGame = new Game(pictureBox1.CreateGraphics());
            if (tetrisGame.RestoreGame())
            {
                UpdateStatistic();
                PauseButton.Enabled = true;
                PauseButton.Text = "Continue";
            }

       }

        private void рекордыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordsForm childRecordsForm = new RecordsForm(this);
            childRecordsForm.MdiParent = this.MdiParent;
            childRecordsForm.Show();
        }
    

    }
}
