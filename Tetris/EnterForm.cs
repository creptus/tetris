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
    public partial class EnterForm : Form
    {
        private Form1 form1;
        private int points;

        public EnterForm(Form1 form,int GamePoints)
        {
            InitializeComponent();
            form1 = form;
            points = GamePoints;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameRecords gr = new GameRecords(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Tetris\"+@"rec.txt");

            string name = "NoName";
            if (textBox1.Text != "") name = textBox1.Text;
            gr.Add(name,points);
            gr.Save();
            Close();
        }

        private void EnterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RecordsForm childRecordsForm = new RecordsForm(form1);
            childRecordsForm.MdiParent = this.MdiParent;
            childRecordsForm.Left = -80;
            childRecordsForm.Top = -50;
            childRecordsForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
