using System;
using System.Drawing;
using static System.Math;
using System.Windows.Forms;

namespace LAB
{
    public partial class Form1 : Form
    {
        public double _V0, _V0x, _V0y, _Hmax, _Lmax, _Tmax, _h0, _alpha, _t, _n, _n0x, _n0y, _x, _y;
        public int _W, _H, _fullW, _fullH, _x0, _y0;
        public int _grad = 180;
        public double _G = 9.81;
        public int _dtab = 100;
        public double _dt = 0.01;
        public double _mashtab = 100;
        public double _test = 100;
        public Form1()
        {
            InitializeComponent();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DrawGraph();
            _t += _dt;
            _x = Round(_n0x * _t);
            _y = Round(_h0 + (_n0y * _t) - (_G * _t * _t) / 2);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            _fullW = Form1.ActiveForm.ClientSize.Width;
            _fullH = Form1.ActiveForm.ClientSize.Height;
            Graphics spot3 = Form1.ActiveForm.CreateGraphics();
            Brush Pen3 = new SolidBrush(Color.White);
            spot3.FillRectangle(Pen3, 0, 0, _fullW, _fullH);
            _x0 = 0;
            _y0 = 0;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            DrawTable();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DrawAxis();
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            _x = 0;
            _y = 0;
            _t = 0;
        }
        public void Input()
        {
            try
            {
                _V0 = Convert.ToDouble(textBox1.Text);
                _alpha = Convert.ToDouble(textBox2.Text);
                _h0 = Convert.ToDouble(textBox3.Text);
            }
            catch
            {

            }
            finally
            {
                button1.Enabled = true;
            }
            _n = _V0;
            while (_n > _mashtab)
            {
                _n *= _test;
                _n0x *= _test;
                _n0y *= _test;
            }
        }
        public void Calc()
        {
            _V0x = _V0 * Cos(_alpha * PI / _grad);
            _V0y = _V0 * Sin(_alpha * PI / _grad);
            _n0x = _n * Cos(_alpha * PI / _grad);
            _n0y = _n * Sin(_alpha * PI / _grad);
            _Hmax = _h0 + (_V0y * _V0y) / (2 * _G);
            _Tmax = (_V0y + Sqrt(_V0y * _V0y + 2 * _G * _h0)) / _G;
            _Lmax = _V0x * _Tmax;
        }
        public void Output()
        {
            textBox4.Text = Convert.ToString(_Hmax);
            textBox5.Text = Convert.ToString(_Lmax);
            textBox6.Text = Convert.ToString(_Tmax);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Input();
            Calc();
            Output();
        }

        public void DrawAxis()
        {
            Graphics g = Form1.ActiveForm.CreateGraphics();
            Pen Pen;
            Pen = new Pen(Color.Green);
            Pen.Width = 5;
            _fullW = Form1.ActiveForm.ClientSize.Width;
            _fullH = Form1.ActiveForm.ClientSize.Height;
            Point KX1, KX2;
            KX1 = new Point(0, _fullH);
            KX2 = new Point(_fullW, _fullH);
            g.DrawLine(Pen, KX1, KX2);
            Point KY1, KY2;
            KY1 = new Point(0, 0);
            KY2 = new Point(0, _fullH);
            g.DrawLine(Pen, KY1, KY2);
            g.Dispose();
        }
        public void DrawGraph()
        {
            Graphics spot = Form1.ActiveForm.CreateGraphics();
            Pen Pen2 = new Pen(Color.Red);
            Pen2.Width = 2;
            spot.DrawLine(Pen2, Convert.ToInt32(_x - 1), Convert.ToInt32(_fullH - _y - 1), Convert.ToInt32(_x + 1), Convert.ToInt32(_fullH - _y + 1));
        }
        public void DrawTable()
        {
            Graphics tab = Form1.ActiveForm.CreateGraphics();
            Pen Pent;
            Pent = new Pen(Color.Black);
            Pent.Width = 0.2F;
            _fullW = Form1.ActiveForm.ClientSize.Width;
            _fullH = Form1.ActiveForm.ClientSize.Height;
            while (_x0 < _fullW)
            {
                Point TX1, TX2;
                TX1 = new Point(_x0 + _dtab, _fullH);
                TX2 = new Point(_x0 + _dtab, 0);
                tab.DrawLine(Pent, TX1, TX2);
                _x0 += _dtab;
            }
            while (_y0 < _fullH)
            {
                Point TY1, TY2;
                TY1 = new Point(0, _fullH - _y0);
                TY2 = new Point(_fullW, _fullH - _y0);
                tab.DrawLine(Pent, TY1, TY2);
                _y0 += _dtab;
            }
            tab.Dispose();
        }
    }
}