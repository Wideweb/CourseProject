using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class Canvas
    {
        public int Width { set;  get; }
        public int Height { set; get; }
        private int[,] _matrix;

        public Canvas(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            _matrix = new int[Height, Width];
        }

        public void DrawBlackPoint(int x, int y)
        {
            _matrix[y, x] = 1;
        }

        public void SetWhiteCanvas()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    this._matrix[i, j] = 0;
        }
    }
}
