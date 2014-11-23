using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace CourseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            /*for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\Arial\" + symbol + ".bmp"));
                vector = GetVector(bmp);
                Console.WriteLine(symbol + " - " + (char)('A' + kn.Parse(vector)));
            }*/

            /*for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                CreateBitmapImage(symbol.ToString());
            }*/

            var bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\lol.bmp"));

            var kn = new KohonenNetwork();
            var td = new TextDetector();
            var recognizer = new Recognizer(kn, td);
            var Message = recognizer.Parse(bmp);

            Console.WriteLine(Message);

            Console.ReadKey();
        }

        static Bitmap CreateBitmapImage(string imageText)
        {
            Bitmap bitmap = new Bitmap(1, 1);

            int width = 0;
            int height = 0;

            // Создаем объект Font для "рисования" им текста.
            Font font = new Font("Adobe Ming Std L", 58, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

            // Создаем объект Graphics для вычисления высоты и ширины текста.
            Graphics graphics = Graphics.FromImage(bitmap);

            // Определение размеров изображения.
            int StrWidth = (int)graphics.MeasureString(imageText, font).Width;
            int StrHeight = (int)graphics.MeasureString(imageText, font).Height;
            width = 45;
            height = 45;

            // Пересоздаем объект Bitmap с откорректированными размерами под текст и шрифт.
            bitmap = new Bitmap(bitmap, new Size(width, height));

            // Пересоздаем объект Graphics
            graphics = Graphics.FromImage(bitmap);

            // Задаем цвет фона.
            graphics.Clear(Color.White);
            // Задаем параметры анти-алиасинга
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            // Пишем (рисуем) текст
            graphics.DrawString(imageText, font, new SolidBrush(Color.FromArgb(0, 0, 0)), (45 - StrWidth) / 2, (45 - StrHeight)/2 + 3);
            graphics.Flush();

            bitmap.Save(@"C:\Users\Alexander\Desktop\Adobe Ming Std L\" + imageText + ".bmp");

            return (bitmap);
        }
    }
}
