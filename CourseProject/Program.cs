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
            var bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\hey.bmp"));

            var kn = new KohonenNetwork();
            var td = new TextDetector();
            var recognizer = new Recognizer(kn, td);
            var Message = recognizer.Parse(bmp);

            Console.WriteLine(Message);

            Console.ReadKey();
        }
    }
}
