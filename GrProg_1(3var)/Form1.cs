using System;
using System.Drawing;
using System.Windows.Forms;

namespace GrProg_1_3var_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Метод, устанавливающий пиксел на форме с заданными цветом и прозрачностью для метода Брезенхема
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            var bm = new Bitmap(10, 10);
            g.DrawImageUnscaled(bm, x, y);

            g.FillRectangle(new SolidBrush( Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        //Статический метод, реализующий отрисовку 4-связной линии
        public static void Bresenham4Line(Graphics g, Color clr, int x0, int y0,
                                                                 int x1, int y1)
        {
            //Изменения координат
            var dx = x1 > x0 ? x1 - x0 : x0 - x1;
            var dy = y1 > y0 ? y1 - y0 : y0 - y1;
            //Направление приращения
            var sx = x1 >= x0 ? 1 : -1;
            var sy = y1 >= y0 ? 1 : -1;

            if (dy < dx)
            {
                var d = (dy << 1) - dx;
                var d1 = dy << 1;
                var d2 = (dy - dx) << 1;
                PutPixel(g, clr, x0, y0, 100);
                var x = x0 + sx;
                var y = y0;
                for (var i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 100);
                    x += sx;
                }
            }
            else
            {
                var d = (dx << 1) - dy;
                var d1 = dx << 1;
                var d2 = (dx - dy) << 1;
                PutPixel(g, clr, x0, y0, 100);
                var x = x0;
                var y = y0 + sy;
                for (var i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 100);
                    y += sy;
                }
            }
        }

        //Метод, устанавливающий пиксел на форме с заданными цветом и прозрачностью для метода ЦДА
        private static void PutPixel1(Graphics g, int x, int y)
        {
            var bm = new Bitmap(20, 20);
            bm.SetPixel(0, 0, Color.Red);
            g.DrawImageUnscaled(bm, x, y);
        }

        private static void DdaCiz(int x1, int y1, int x2, int y2, Graphics grafik)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;

            var pikselSayisi = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            var xFark = dx / (float)pikselSayisi;
            var yFark = dy / (float)pikselSayisi;

            float x = x1;
            float y = y1;

            while (pikselSayisi != 0)
            {
                PutPixel1(grafik, (int)Math.Floor(x + 0.5F), (int)Math.Floor(y + 0.5f));
                x += xFark;
                y += yFark;
                pikselSayisi--;
            }
        }

        //кнопка рисует 3 линии
        private void button1_Click(object sender, EventArgs e)
        {   //создаем переменную для управления графикой
            var g = pictureBox1.CreateGraphics();
            // создаем линию косую по где после цвета (ред) идут координаты: х0, у0, х1, у1
            Bresenham4Line(g, Color.FromArgb(0, 255, 25), pictureBox1.ClientSize.Width, 0,
                0, pictureBox1.ClientSize.Height);
            // аналогично, но косая линия в другую сторону
            Bresenham4Line(g, Color.FromArgb(0, 26, 255), 0, 0,
                pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            // прямая линия - смысл тот же, только цвет уже мы указали в методе алгоритма ЦДА

            // верхнее основание
            DdaCiz(0, 0, pictureBox1.ClientSize.Width * 2, 0, g);

            //нижнее основание
            DdaCiz(0, pictureBox1.ClientSize.Height - 1, pictureBox1.ClientSize.Width * 2, pictureBox1.ClientSize.Height,
                g);

            //левая сторона
            DdaCiz(0, 0, 0, pictureBox1.ClientSize.Height,
                g);

            //правая сторона
            DdaCiz(pictureBox1.ClientSize.Width, -pictureBox1.ClientSize.Height, pictureBox1.ClientSize.Width - 1,
                pictureBox1.ClientSize.Height, g);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
