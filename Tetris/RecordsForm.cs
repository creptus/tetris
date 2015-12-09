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
    public partial class RecordsForm : Form
    {
        private Form1 form1;
        public RecordsForm(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        private void RecordsForm_Shown(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("№", "№");
            dataGridView1.Columns["№"].Width = 20;
            dataGridView1.Columns.Add("Information", "Имя");
            dataGridView1.Columns["Information"].Width = 100;
            dataGridView1.Columns.Add("Points", "Очки");
            dataGridView1.Columns["Points"].Width = 100;

            string[,] stat;
            GameRecords gr = new GameRecords(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Tetris\"+@"rec.txt");
            stat = gr.getList();
            for (int i = 0; i < stat.GetLength(1); i++)
            {
                dataGridView1.Rows.Add((i+1).ToString(), stat[0,i], stat[1,i]);
            }                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RecordsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Enabled = true;
        }
    }
}
