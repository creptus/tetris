using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            //Создаем папку в моих документах
            string md=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (Directory.Exists(md + @"\Tetris") == false)
            {
                Directory.CreateDirectory(md + @"\Tetris");
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Form1 childForm1 = new Form1();
            childForm1.MdiParent = this;
            Height = childForm1.Height+45;
            Width = childForm1.Width + 25;
            childForm1.Show();
        }
    }
}
