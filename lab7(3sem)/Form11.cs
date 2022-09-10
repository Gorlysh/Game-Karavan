using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_3sem_
{
    public partial class Form11 : Form
    {
        public Form11(string str )
        {
            InitializeComponent();
            label1.Text = str;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void Form11_Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
