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

    public partial class Form2 : Form
    {
        public CGuard guard = new CGuard(0, 0);//объект охрана
        public CKargo cargo = new CKargo();// объект груз
        public CMeal meal = new CMeal();// объект пища
        public CBanda bnd = new CBanda();// объект разбойники
        public CWheather wth = new CWheather();// объект погода
        public CIncident incdnt = new CIncident(0);// объет событие
        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {

        }

        public void Button1_Click(object sender, EventArgs e) //переход к наёму охраны
        {

            Form4 newForm = new Form4(this);
            newForm.Show();


        }
        public void CloseForm2()//закрытие программы
        {
            Application.Exit();
        }

        public void DelBut1() //скрытие клавиши 
        {
            button1.Hide();
        }
    }
    //Класс Караван
    abstract public class CKaravan : Object
    {
        public int value;

        public CKaravan(int avalue)
        {
            value = avalue;
        }
        public void SetValue(int avalue) //установить значение
        {
            value = avalue;
        }
        public int GetValue() //вывести значение
        {
            return value;
        }

        virtual public int Reduce(int value) //уменьшить значение на 3
        {
            value = value - 3;
            return value;
        }
    }

    interface Warriors
    {
        void SetPwr(double pwr);//установить силу

        double GetPwr();//вывести силу

    }
    interface Cost
    {
        void SetCost(int kol, double pwr);//установить цену наёма

        double GetCost();//вывести цену наёма
    }

    //Класс Охрана
    public class CGuard : CKaravan, Warriors, Cost
    {
        protected double pwr;
        protected double cst;

        public CGuard(int avalue, double ap) : base(0)
        {
            pwr = ap;
        }

        public void SetPwr(double ap) //установить силу
        {
            pwr = ap;
        }

        public double GetPwr()//вывести силу
        {
            return pwr;
        }

        public void SetCost(int kol, double pwr)//установить цену наёма
        {
            cst = (1.8 + (0.2 * pwr)) * kol;
        }
        public double GetCost()//вывести цену наёма
        {
            return cst;
        }

        public bool Fight(int kol, int kolbnd, double pwrbnd)//Бой с разбойниками
        {
            bool fl = false;
            double a = kol + (pwr * 0.5);
            double b = kolbnd + (pwrbnd * 0.5);
            if (a >= b) { fl = true; } else { fl = false; }
            return fl;
        }
        override public int Reduce(int value)//уменьшение кол-ва охранников после боя
        {
            value = value-2;
            if (value<0) { value = 0; }
            return value;
        }
    }

    //Класс груз
    public class CKargo : CKaravan
    {
        protected double day;
        public string ct;
        public int dist;

        public CKargo() : base(0)
        {
            day = 0;
            ct = null;
            dist = 0;
        }

        public int Distance(bool fl1, bool fl2, bool fl3, bool fl4)//нахождение расстояния между городами
        {
            if (fl1 == true) { dist = 400; } else 
                if (fl2 == true) { dist = 300; } else 
                if (fl3 == true) { dist = 900; } else 
                if (fl4 == true) { dist = 800; }
            return dist;
        }

        public string City(bool fl1, bool fl2, bool fl3, bool fl4)//вывод названия города
        {
            if (fl1 == true) { ct= " Дамаск. "; }
            else
                if (fl2 == true) { ct = " Кадеш. "; }
            else
                if (fl3 == true) { ct = " Нисибин. "; }
            else
                if (fl4 == true) { ct = " Алеппо. "; }
            return ct;
        }

        virtual public double Time(int kol, int dist, int value)//расчёт времени в пути
        {  
            day = dist / (50 - kol * 0.3);
            return day;
        }
    }

    //Класс пища
    public class CMeal : CKargo
    {
        public CMeal()
        {

        }

        public double Reducemeal(int guard, double meal)//уменьшение пищи на число необходимое охране
        {
            meal = meal - (0.2 * guard);
            return meal;
        }
        override public double Time(int guard, int meal, int value )//расчёт кол-ва дней на которое хвати еды
        {
            day = meal/ ((0.2*guard + 0.05*value) );
            return day;
        }

        public double Mealdown(int guard, double meal, int value, int dday)//уменьшение еды через dday(кол-во дней)
        {
           meal = meal -( (0.2 * guard + 0.05 * value) *(dday) );
            return meal;
        }
    }

    //Класс событие
    public class CIncident : Object
    {
        public Random c= new Random();
        public int s;

        public CIncident(int s)
        {

        }
        public int Variant (int kol1, int kol2)//Генерация случайного события
        {
          if ((kol1==0)&&(kol2==0))
            {
                s = c.Next(-4, 4);
            } else if ((kol1 == kol2) && !((kol1 == 0) && (kol2 == 0)))
            {
                s = c.Next(-2, 2);
            } else if ( (kol1 > kol2) && (kol1+kol2<4) )
            {
                s = c.Next(-1, 4);
            }else if ((kol1 < kol2) && (kol1 + kol2 < 4))
            {
                s = c.Next(-4, 1);
            } else { s = c.Next(-1, 1); }

          if (s==0) { s = 1; }else 
                if (s<0) { s = 2; } else 
                if (s>0) { s = 3; }
            return s;
        }
    }

    //Класс Разбойники
    public class CBanda : CIncident
    { 
        public int bnd;
        public int bpw;

        public CBanda () : base(0)
        {
            bpw = c.Next(1, 10);//генерация силы разбойников
        }

        public int Generate(int guard)//Генерация банды разбойников
        {
            bnd = c.Next(1, guard + 5);
            return bnd;
        }
        public int Getbpw()//установка силы разбойников
        {
            return bpw;
        }
    }

    //Класс Погода
    public class CWheather : CIncident
    {
        public int wth;
        public CWheather() : base(0) { }

        public int Generate()//Генерация коэффициента плохой погоды
        {
            wth = c.Next(1,10);
            if (wth < 5) { s = 1; } else { s = 2; }
            return s;
        }
    }
}
