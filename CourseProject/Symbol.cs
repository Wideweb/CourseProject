using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
    class Symbol
    {
        private int _x;
        private int _y;
        private int _top = 0;
        private int _buttom = 0;
        private int _left = 0;
        private int _right = 0;
        private Canvas _canvas;

        public int CenterX { get; set; }
        public int CenterY { get; set; }

        public Bitmap Pattern { get; set; }

        public Symbol(int Width, int Height)
        {
            _canvas = new Canvas(Width, Height);
        }

        private Bitmap LeadToThePattern(Bitmap image)
        {
            Bitmap tmpFile = new Bitmap(image, new Size(45, 45));
            return tmpFile;
        }

        public void CutSymbol(Bitmap image)
        {
            RectangleF cloneRect = new RectangleF(_top, _left, _right - _left + 2, _buttom - _top + 2);
            System.Drawing.Imaging.PixelFormat format =
                image.PixelFormat;
            Bitmap cloneBitmap = image.Clone(cloneRect, format);

            Pattern = LeadToThePattern(cloneBitmap);
        }

        public void AddBlackPoint(int x, int y)
        {
            _canvas.DrawBlackPoint(x, y);
            if (y > _buttom)
                _buttom = y;
            if (y < _top)
                _top = y;
            if (x < _left)
                _left = x;
            if (x > _right)
                _right = x;
        }

        private void CountCenter()
        {
            CenterX = (_left + _right) / 2;
            CenterY = (_top + _buttom) / 2;
        }
    }
}
