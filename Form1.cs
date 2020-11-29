using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public double V0, V0x, V0y, Hmax, Lmax, Tmax, h0, alpha, t, n, n0x, n0y;

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Graph();
            t += 0.01;
            x = Math.Round(n0x * t);
            y = Math.Round(h0 + (n0y * t) - (9.81 * t * t) / 2);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Axis();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            x = 0; y = 0; t = 0;

        }

        public void Input()
        {
            try
            {

                V0 = Convert.ToDouble(textBox1.Text);
                alpha = Convert.ToDouble(textBox2.Text);
                h0 = Convert.ToDouble(textBox3.Text);
            }
            catch
            {

            }
            finally { button1.Enabled = true; }
            n = V0;
            while (n > 100)
            {
                n /= 2;
                n0x /= 2;
                n0y /= 2;
            }
        }
        public void Calc()
        {

            V0x = V0 * Math.Cos(alpha * Math.PI / 180);
            V0y = V0 * Math.Sin(alpha * Math.PI / 180);
            n0x = n * Math.Cos(alpha * Math.PI / 180);
            n0y = n * Math.Sin(alpha * Math.PI / 180);

            Hmax = h0 + (V0y * V0y) / (2 * 9.81);
            Tmax = (V0y + Math.Sqrt(V0y * V0y + 2 * 9.81 * h0)) / 9.81;
            Lmax = V0x * Tmax;
        }
        public void Output()
        {
            textBox4.Text = Convert.ToString(Hmax);
            textBox5.Text = Convert.ToString(Lmax);
            textBox6.Text = Convert.ToString(Tmax);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Input();
            Calc();
            Output();
        }


        
        
            public int W, H, fullW, fullH;
            public double x, y;

            public void Axis()
            {
            
               
                Graphics g = Form1.ActiveForm.CreateGraphics();
             
                Pen Pen;
                Pen = new Pen(Color.Violet);
                Pen.Width = 5;
               
                
                fullW = Form1.ActiveForm.ClientSize.Width; 
                fullH = Form1.ActiveForm.ClientSize.Height; 
          
            
            Point KX1, KX2;
                KX1 = new Point(0, fullH);
                KX2 = new Point(fullW, fullH);
                g.DrawLine(Pen, KX1, KX2);
          
                Point KY1, KY2;
                KY1 = new Point(0, 0);
                KY2 = new Point(0, fullH);
                g.DrawLine(Pen, KY1, KY2);
                g.Dispose();
            }
        public void Graph()
        {
            Graphics spot = Form1.ActiveForm.CreateGraphics();
            Pen Pen2; Pen2 = new Pen(Color.Black);
            Pen2.Width = 2;
            
            spot.DrawLine(Pen2, Convert.ToInt32(x - 1), Convert.ToInt32(fullH-y - 1), Convert.ToInt32(x + 1), Convert.ToInt32(fullH-y + 1));
        }
    }
}