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
    public partial class Form5 : Form2
    {
        int kol1 = 0;
        int kol2=0;
        int dday = 0;
        int day1 =0;
        int day2 = 0;
        public Form5(Form3 f)
        {
            InitializeComponent();
            f.Hide();
            DelBut1();
            guard = f.guard;
            cargo = f.cargo;
            meal = f.meal;
            bnd = f.bnd;
            wth = f.wth;
            incdnt = f.incdnt;

            textBox1.AppendText("Караван вышел из Пальмиры.");
            textBox1.AppendText("Стоимость охраны " + Convert.ToString(guard.GetCost().ToString("f0")) + " риала." + "\r\n");
            textBox1.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist,0).ToString("f0")) + " дней" +
                                ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                " дней." + "\r\n" + "\r\n");
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)//Продолжить путь
        {
            textBox2.Clear();
            if (cargo.GetValue() > 10) { button3.Enabled = true; }
            if (guard.GetValue() != 0) { button4.Enabled = true; }


                int vrn = incdnt.Variant(kol1, kol2);

            if (cargo.dist <= 0)//Завершение пути
            {
                string str = "Караван дошёл до города " + cargo.ct;
                Hide();
                Form11 newForm = new Form11(str);
                newForm.Show();
            }
            else
            {
                if (meal.Time(guard.GetValue(),meal.GetValue(), cargo.GetValue()) <0.5)//Гибель от голода
                {
                    Form10 newForm = new Form10();
                    newForm.Show();
                }
                else
                {
                    if (vrn == 1)//Происшествий нет
                    {
                        day1 = Convert.ToInt32(cargo.Time(cargo.GetValue(), cargo.dist, 0));                     
                        cargo.dist = cargo.dist - 100;
                        day2= Convert.ToInt32(cargo.Time(cargo.GetValue(), cargo.dist, 0));
                        dday =day1 - day2; 
                        meal.SetValue(Convert.ToInt32(meal.Mealdown(guard.GetValue(), Convert.ToDouble(meal.GetValue()), cargo.GetValue(), dday)));
                        textBox1.AppendText("Караван движется без происшествий." + "\r\n");

                        textBox1.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                            "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                            "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                            "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                            ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                            " дней." + "\r\n" + "\r\n");
                    }
                    else if (vrn == 2)//Нападение разбойников
                    {
                        button2.Enabled = false;
                        button3.Enabled = false;
                        if (guard.GetValue() != 0) { button4.Enabled = false; }
                        kol1++;
                        day1 = Convert.ToInt32(cargo.Time(cargo.GetValue(), cargo.dist, 0));
                        cargo.dist = cargo.dist - 100;
                        day2 = Convert.ToInt32(cargo.Time(cargo.GetValue(), cargo.dist, 0));
                        dday = day1 - day2;
                        meal.SetValue(Convert.ToInt32(meal.Mealdown(guard.GetValue(), Convert.ToDouble(meal.GetValue()), cargo.GetValue(), dday)));
                        textBox1.AppendText("На вас напали разбойники!" + "\r\n" + "\r\n");

                        textBox2.AppendText("На вас напали разбойники! Вы можете либо защищаться, либо заплатить им 3 риала." + "\r\n");

                        textBox2.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                            "-----------Сила: " + Convert.ToString(guard.GetPwr().ToString("f2")) + "\r\n" +
                                            "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n");

                        textBox2.AppendText("Стоимость охраны " + Convert.ToString(guard.GetCost().ToString("f0")) + " риала." + "\r\n" + "\r\n");

                        button6.Text = "Вступить в бой";
                        button5.Text = "Откупиться";
                        Form12 newForm = new Form12();
                        newForm.Show();
                    }
                    else if (vrn == 3)//Песчанная буря
                    {
                        button2.Enabled = false;
                        button3.Enabled = false;
                        if (guard.GetValue() != 0) { button4.Enabled = false; }
                        kol2++;
                        day1 = Convert.ToInt32(cargo.Time(cargo.GetValue(), cargo.dist, 0));
                        if (wth.Generate() == 1)
                        {
                            cargo.dist = cargo.dist - 50;
                        }
                        day2 = Convert.ToInt32(cargo.Time(cargo.GetValue(), cargo.dist-100, 0));
                        dday = day1 - day2;
                        meal.SetValue(Convert.ToInt32(meal.Mealdown(guard.GetValue(), Convert.ToDouble(meal.GetValue()), cargo.GetValue(),dday)));
                        button6.Text = "Распустить охрану";
                        button5.Text = "Ничего не предпринимать";
                        textBox1.AppendText("Начинается пещанная буря!" + "\r\n" + "\r\n");

                        textBox2.AppendText("Началась пещанная буря, вы не можете идти дальше, время в пути увеличивается. Можно распустить охрану, чтобы  пищи хватило на большее время, либо ничего не предпринимать." + "\r\n");

                        textBox2.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                            "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                            "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n");

                        textBox2.AppendText("До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                            ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                            " дней." + "\r\n" + "\r\n");

                        Form9 newForm = new Form9();
                        newForm.Show();
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)//Выбрасывание 3 единиц груза
        {
            button3.Enabled = true;
            if (!(cargo.GetValue() < 10)) {
                cargo.SetValue(cargo.Reduce(cargo.GetValue()));
                if (cargo.GetValue() < guard.GetCost())
                {
                    textBox1.AppendText("Вы сбросили слишком много груза, теперь вам нечем платить охране и она бросила вас, забрав с собой часть пищи." + "\r\n");
                    meal.SetValue(Convert.ToInt32(meal.Reducemeal(guard.GetValue(), Convert.ToDouble(meal.GetValue()))));
                    guard.SetValue(0);

                }
                else
                {
                    textBox1.AppendText("Вы сбросили часть груза." + "\r\n");
                }
                textBox1.AppendText("Стоимость охраны " + Convert.ToString(guard.GetCost().ToString("f0")) + " риала." + "\r\n");
                textBox1.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                    "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                    "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                    "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                    ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                    " дней." + "\r\n" + "\r\n");
            }
            else button3.Enabled = false;
        }

        private void Button4_Click(object sender, EventArgs e)//Роспуск охраны
        {
            button4.Enabled= false;
            guard.SetValue(0);
            textBox1.AppendText("Вы распустили охрану." + "\r\n");
            textBox1.AppendText("-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(),meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                " дней." + "\r\n" + "\r\n");

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void Form5_Closed(object sender, FormClosedEventArgs e)//Закрытие приложения
        {
            Application.Exit();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)//Кнопка действия1 (вступить в бой \ роспуск охраны)
        {
            button2.Enabled = true;
            if (cargo.GetValue() > 10) { button3.Enabled = true; }
            if (guard.GetValue() != 0) { button4.Enabled = true; }

            if (button6.Text== "Вступить в бой")//Вступить в бой
            {
                if (!(guard.GetValue() == 0))
                {

                    int kolbnd = bnd.Generate(guard.GetValue());
                    bool fl = guard.Fight(guard.GetValue(), kolbnd, bnd.Getbpw());
                    if (fl == true)
                    {
                        guard.SetValue(guard.Reduce(guard.GetValue()));
                        Form8 newForm = new Form8();
                        newForm.Show();
                        button5.Text = "Действие 2";
                        button6.Text = "Действие 1";
                        textBox2.Clear();
                        textBox1.AppendText("Вы отбили нападение." + "\r\n");

                        textBox1.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                            "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                            "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                            "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                            ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                            " дней." + "\r\n" + "\r\n");
                    }
                    else
                    {
                        Hide();
                        Form7 newForm = new Form7();
                        newForm.Show();
                    }
                }
                else button6.Enabled = false;
            }else if(button6.Text == "Распустить охрану")//Роспуск охраны
            {
                guard.SetValue(0);
                textBox2.Clear();
                textBox1.AppendText("Буря кончилась, можно идти дальше, но уже без охраны." + "\r\n");
                textBox1.AppendText("-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                    "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                    "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                    ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                    " дней." + "\r\n" + "\r\n");
                button5.Text = "Действие 2";
                button6.Text = "Действие 1";
            }
        
        }

        private void Button5_Click(object sender, EventArgs e)//Действие2 (Откупится \ Ничего не предпринимать)
        {
            button2.Enabled = true;
            if (cargo.GetValue() > 10) { button3.Enabled = true; }
            if (guard.GetValue() != 0) { button4.Enabled = true; }

            if (button5.Text == "Откупиться")//Откупиться
            {
                if (!(cargo.GetValue() < 10))
                {
                    cargo.SetValue(cargo.Reduce(cargo.GetValue()));
                    Form6 newForm = new Form6();
                    newForm.Show();
                    button5.Text = "Действие 2";
                    button6.Text = "Действие 1";
                    textBox2.Clear();
                    if (cargo.GetValue() < guard.GetCost())
                    {
                        textBox1.AppendText("Вы откупились, но теперь вам нечем платить охране, поэтому все воины покинули ваш караван, забрав с собой часть пищи." + "\r\n");
                        meal.SetValue(Convert.ToInt32(meal.Reducemeal(guard.GetValue(), Convert.ToDouble(meal.GetValue()))));
                        guard.SetValue(0);

                    }
                    else
                    {
                        textBox1.AppendText("Вы откупились." + "\r\n");
                    }
                    textBox1.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                        "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                        "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                        "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                        ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(), meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                        " дней." + "\r\n" + "\r\n");
                }
                else button5.Enabled = false;
            }else if (button5.Text == "Ничего не предпринимать")//Ничего не предпринимать
            {
                textBox2.Clear();
                textBox1.AppendText("Буря кончилась, теперь можно идти дальше." + "\r\n");

                textBox1.AppendText("-----------Кол-во охранников: " + Convert.ToString(guard.GetValue()) + "\r\n" +
                                    "-----------Кол-во товара: " + Convert.ToString(cargo.GetValue()) + "\r\n" +
                                    "-----------Кол-во пищи: " + Convert.ToString(meal.GetValue()) + "\r\n" +
                                    "До конца пути осталось " + Convert.ToString(cargo.Time(cargo.GetValue(), cargo.dist, 0).ToString("f0")) + " дней" +
                                    ". Пищи хватит на " + Convert.ToString(meal.Time(guard.GetValue(),meal.GetValue(), cargo.GetValue()).ToString("f0")) +
                                    " дней." + "\r\n" + "\r\n");
                button5.Text = "Действие 2";
                button6.Text = "Действие 1";
            }

        }
    }
    
}
