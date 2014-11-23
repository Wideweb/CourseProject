using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
    static class Mat
    {

        static public int[][] GetMatrix(Bitmap image)
        {
            int[][] matrix = new int[image.Height][];
            for (var i = 0; i < image.Height; i++)
            {
                matrix[i] = new int[image.Width];
                for (var j = 0; j < image.Width; j++)
                    if (isBlack(image.GetPixel(j, i)))
                        matrix[i][j] = 1;
                    else
                        matrix[i][j] = 0;
            }

            return matrix;
        }

        static public int[] GetVector(Bitmap image)
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

        static public bool isBlack(Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return true;
            else
                return false;
        }
    }
}
