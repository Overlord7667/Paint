using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        SolidBrush brush;
        SolidBrush brush2;
        int x;
        int y;
        float secondAngle;
        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            brush = new SolidBrush(Color.Black);
            brush2 = new SolidBrush(Color.White);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (e.Button == MouseButtons.Left)
                    graphics.FillEllipse(brush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            if(e.Button == MouseButtons.Right)
            graphics.FillEllipse(brush2, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                brush.Color = colorDialog1.Color;
            button1.BackColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                brush2.Color = colorDialog1.Color;
            button2.BackColor = colorDialog1.Color;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked)
            { 
            Pen pen = new Pen(brush);
            if (e.Button == MouseButtons.Left)
                pen = new Pen(brush, trackBar1.Value);
            if (e.Button == MouseButtons.Right)
                pen = new Pen(brush2, trackBar1.Value);
            graphics.DrawLine(pen, x, y, e.X, e.Y);
            }
        }

        void DrawLine(float x, float y, float angle, int length)
        {
            if (length < 10)
                return;
            float x1 = (float)Math.Cos(angle * (Math.PI / 180)) * length + x;
            float y1 = (float)Math.Sin(angle * (Math.PI / 180)) * length + y;
            Pen pen = new Pen(brush, trackBar1.Value-3);
            if (length < 40)
                pen.Color = brush2.Color;
            graphics.DrawLine(pen, x, y, x1, y1);

            DrawLine(x1, y1, angle - secondAngle, length - 8);
            DrawLine(x1, y1, angle + secondAngle, length - 8);
        }
        void DrawGear(int count, int length)
        {
            float angle = -90;
            float second = 360 / count;
            int x = pictureBox1.Width / 2;
            int y = pictureBox1.Width / 2;
            for (int i = 0; i < count; i++)
            {
                float y1 = (float)Math.Cos(angle * (Math.PI / 180)) * length + y;
                float x1 = (float)Math.Sin(angle * (Math.PI / 180)) * length + x;
                Pen pen = new Pen(brush, trackBar1.Value);
                graphics.DrawLine(pen, x, y, x1, y1);
                angle += second;
                
            }

            graphics.FillEllipse(brush, x - 80, y - 80, 160, 160);
            graphics.FillEllipse(new SolidBrush(Color.White), x - 20, y -20, 40, 40);
        }

        void Spruce()
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            secondAngle = (float)Convert.ToDouble(textBox1.Text);
            secondAngle *= -1;
            DrawLine(pictureBox1.Width/2,pictureBox1.Height-40, -90, 100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            int count = Convert.ToInt32(textBox2.Text);
            DrawGear(count, 100);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
