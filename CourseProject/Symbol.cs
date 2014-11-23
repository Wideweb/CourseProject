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
        private int _top = int.MaxValue;
        private int _buttom = 0;
        private int _left = int.MaxValue;
        private int _right = 0;

        public int CenterX { get { return _x; } }
        public int CenterY { get { return _y; } }

        public Bitmap Pattern { get; set; }

        public Symbol(){}

        private Bitmap LeadToThePattern(Bitmap image)
        {
            Bitmap tmpFile = new Bitmap(image, new Size(45, 45));
            return tmpFile;
        }

        public void CutSymbol(Bitmap image)
        {
            Console.WriteLine("left - " + _left + "top - " + _top);
            RectangleF cloneRect = new RectangleF(_left, _top, _right - _left + 2, _buttom - _top + 2);
            System.Drawing.Imaging.PixelFormat format =
                image.PixelFormat;
            Bitmap cloneBitmap = image.Clone(cloneRect, format);

            Pattern = LeadToThePattern(cloneBitmap);
            //Pattern = cloneBitmap;
        }

        public void AddBlackPoint(int x, int y)
        {
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
            _x = (_left + _right) / 2;
            _y = (_top + _buttom) / 2;
        }
    }
}
