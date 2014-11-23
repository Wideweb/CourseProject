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
            KohonenNetwork kn = new KohonenNetwork();
            Bitmap bmp;
            int[] vector;
            char symbol;

            for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\Arial\" + symbol + ".bmp"));
                vector = GetVector(bmp);
                kn.Study(vector, i);
            }
            
            for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\TimesNewRoman\" + symbol + ".bmp"));
                vector = GetVector(bmp);
                kn.Study(vector, i);
            }
            
            for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\Adobe Ming Std L\" + symbol + ".bmp"));
                vector = GetVector(bmp);
                kn.Study(vector, i);
            }
            
            for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\Arial\" + symbol + ".bmp"));
                vector = GetVector(bmp);
                Console.WriteLine(symbol + " - " + (char)('A' + kn.Parse(vector)));
            }

            /*for (var i = 0; i < 26; i++)
            {
                symbol = (char)('A' + i);
                CreateBitmapImage(symbol.ToString());
            }*/
            var img = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\text.bmp"));
            var textDetector = new TextDetector(img);
            int a = 123;
            foreach(var _symbol in textDetector.Symbols)
            {
                _symbol.Pattern.Save(@"C:\Users\Alexander\Desktop\text\" + a++ + ".bmp");
            }
            
            Console.ReadKey();
        }

        static int[] GetVector(Bitmap image)
        {
            var vector = new int[45 * 45];
            for (var i = 0; i < 45; i++)
                for (var j = 0; j < 45; j++)
                    if (isBlack(image.GetPixel(j, i)))
                        vector[i * 45 + j] = 1;
                    else
                        vector[i * 45 + j] = 0;
            
            return vector;
        }

        static bool isBlack(Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return true;
            else
                return false;
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
