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
    public partial class Form3 : Form2
    {
        public Form3(Form4 f)
        {
            InitializeComponent();
            f.Hide();
            DelBut1();
            guard = f.guard;
            textBox4.AppendText("Стоимость охраны " + Convert.ToString(guard.GetCost().ToString("f0"))+ " риала." +"\r\n");
            textBox4.AppendText("Расход пищи зависит от кол-ва охранников, а также от кол-ва товара!" + "\r\n" + "\r\n");
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }


        private void Button5_Click(object sender, EventArgs e)//выбрасывание груза
        {

            try
            {
                cargo.SetValue(0);
                meal.SetValue(0);
                textBox4.AppendText("Весь груз выброшен!" + "\r\n" + "\r\n");

            }
            catch { MessageBox.Show("Что-то пошло не так!"); }
            textBox2.Clear(); textBox3.Clear();

        }




        private void Button4_Click(object sender, EventArgs e)//Ввод кол-ва товара и пищи
        {
            try
            {
                cargo.ct = cargo.City(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked, radioButton4.Checked);
                double dist = cargo.Distance(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked, radioButton4.Checked);
                cargo.SetValue(Convert.ToInt32(textBox2.Text));
                meal.SetValue(Convert.ToInt32(textBox3.Text));
                if (Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) > 100)
                {
                    MessageBox.Show("Сумма товара и пищи не должна превышать 100");
                }
                if (Convert.ToInt32(textBox2.Text)<= guard.GetCost()) {
                    MessageBox.Show("Стоимость наёма охраны " + Convert.ToString(guard.GetCost().ToString("f0")) + " риала, "+"вам не хватит денег расплатиться с охраной, возьмите больше товара!");
                }
                else
                {
                    textBox4.AppendText("Взято товара: " + Convert.ToString(cargo.GetValue()) + ". ");
                    textBox4.AppendText("Взято пищи: " + Convert.ToString(meal.GetValue()) + "." + "\r\n" + "\r\n");

                }

            }
            catch { MessageBox.Show("Что-то пошло не так!"); textBox2.Clear(); textBox3.Clear();}
            
        }

        private void Button6_Click(object sender, EventArgs e)//Вывод информации о грузе
        {
            
                try
                {
                    cargo.ct = cargo.City(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked, radioButton4.Checked);
                    int dist = cargo.Distance(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked, radioButton4.Checked);

                    if (cargo.Time(cargo.GetValue(), dist,0) > meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()))
                    {
                        MessageBox.Show("Вам не хватит пищи дойти до конца пути!");
                    }
                    else
                    {
                        textBox4.AppendText("Караван отправляется из города Пальмира в город" + cargo.ct + "Вы везёте товар стоимостью " + Convert.ToString(cargo.GetValue()) + " риала, " + "вас сопровождают " +
                                        Convert.ToString(guard.GetValue()) + " воинов. Дорога займёт " + Convert.ToString(cargo.Time(cargo.GetValue(), dist ,0).ToString("f0")) + " дней" + ", пищи хватиn на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                        " дней." + "\r\n" + "\r\n");
                    }
             
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так!");  textBox2.Clear(); textBox3.Clear();; 

                }
        }

        private void Button2_Click(object sender, EventArgs e)// возврат к наёму охраны
        {
                Form4 newForm = new Form4(this);
                newForm.Show();        
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Closed(object sender, FormClosedEventArgs e)//закрытие программы
        {
            Application.Exit();
        }

        private void Button3_Click_1(object sender, EventArgs e)//отправление в путь
        {
            int dist = cargo.Distance(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked, radioButton4.Checked);
            if (cargo.GetValue() == 0) { MessageBox.Show("Вы не снарядили караван!"); }
            else
            {
                if (cargo.Time(cargo.GetValue(), dist, 0) > meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()))
                {
                    MessageBox.Show("Вам не хватит пищи дойти до конца пути!");
                }
                else
                {

                    Form5 newForm = new Form5(this);
                    newForm.Show();
                }
            }
        }
    }
}
