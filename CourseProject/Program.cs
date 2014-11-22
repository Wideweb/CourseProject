using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\A.bmp"));
            var vector = GetVector(bmp);
            KohonenNetwork kn = new KohonenNetwork();
            kn.Study(vector, 0);

            bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\B.bmp"));
            vector = GetVector(bmp);
            kn.Study(vector, 1);

            bmp = new Bitmap(Bitmap.FromFile(@"C:\Users\Alexander\Desktop\A-.bmp"));
            vector = GetVector(bmp);

            Console.WriteLine(kn.Parse(vector));
            
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
    }
}
