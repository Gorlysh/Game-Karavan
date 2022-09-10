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
    public partial class Form4 : Form2
    {
        public Form4(Form2 f)
        {
            InitializeComponent();
            DelBut1();
            f.Hide();
            guard = f.guard;
            textBox1.AppendText("На стоимость охраны влияют кол-во охранников и их сила. Максимальное кол-во охранников 10." + "\r\n" + "\r\n");
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)//Роспуск охраны
        {
            button2.Enabled = false;
                try
                {

                    guard.SetValue(0);
                    guard.SetPwr(0);
                    textBox1.AppendText("Вся охрана распущена!" + "\r\n" + "\r\n");
                    guard.SetCost(guard.GetValue(), guard.GetPwr());

            }
                catch { MessageBox.Show("Что-то пошло не так!"); }
                textBox2.Clear(); textBox3.Clear();
            
        }

        private void Button3_Click(object sender, EventArgs e)//Наём охраны
        {
           
                try
                {
                textBox1.AppendText("Вы наняли " + Convert.ToString(guard.GetValue()) + " охранников, их стоимость " + Convert.ToString(guard.GetCost().ToString("f0")) + " риала." +   "\r\n" + "\r\n");
                }
                catch { MessageBox.Show("Что-то пошло не так!");  textBox2.Clear(); textBox3.Clear(); }
              
            
        }

        private void Button4_Click_1(object sender, EventArgs e)//Вывод информации об охране
        {
            button2.Enabled = true;
            try
            {
                guard.SetValue(Convert.ToInt32(textBox2.Text));
                guard.SetPwr(Convert.ToDouble(textBox3.Text));
                if (Convert.ToDouble(textBox3.Text) > 10) { MessageBox.Show("Сила не может быть больше 10!"); }
                else
                {
                    guard.SetCost(guard.GetValue(), guard.GetPwr());
                    textBox1.AppendText("Нанято охранников: " + Convert.ToString(guard.GetValue()) + ". ");
                    textBox1.AppendText("Сила установленна " + Convert.ToString(guard.GetPwr().ToString("f2")) + "." + "\r\n" + "\r\n");
                }
            }
            catch { MessageBox.Show("Что-то пошло не так!"); textBox2.Clear(); textBox3.Clear();}
            
        }

        private void Button5_Click(object sender, EventArgs e)//переход к настройке каравана
        {
            if (guard.GetValue() == 0) { MessageBox.Show("Вы не наняли охрану!"); }
            else
            {
                Form3 newForm = new Form3(this);
                newForm.Show();

            }
        }

        private void Form4_Closed(object sender, FormClosedEventArgs e)//закрытие приложения
        {
            Application.Exit();
        }

       
    }
}
