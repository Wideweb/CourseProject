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
        public bool isEmpty = true;

        public int CenterX { get { return _x; } }
        public int CenterY { get { return _y; } }
        public int Width { get { return _right - _left; } }
        public int Height { get { return _buttom - _top; } }
        public int Top { get { return _top; } }
        public int Buttom { get { return _buttom; } }
        public int Left { get { return _left; } }
        public int Rigt { get { return _right; } }

        public Bitmap Pattern { get; set; }

        public Symbol(){}

        public int[] GetVector()
        {
            return Mat.GetVector(Pattern);
        }

        private Bitmap LeadToThePattern2(Bitmap image)
        {
            if (image.Height < 5 && image.Width < 5)
                return new Bitmap(45, 45);

            Bitmap tmpFile = new Bitmap(image, new Size(45, 45));
            return tmpFile;
        }

        private Bitmap LeadToThePattern(Bitmap image)
        {
            if (image.Height < 5 && image.Width < 5)
                return new Bitmap(45, 45);
            
            int newWidth = 45;
            int newHeight = 45;

            if (image.Height > image.Width)
                newWidth = (int)(image.Width / ((float)image.Height / 45));
            
            if (image.Height < image.Width)
                newHeight = (int)(image.Height / ((float)image.Width / 45));

            Bitmap newImage = new Bitmap(image, new Size(newWidth, newHeight));

            Bitmap tmpFile = new Bitmap(45, 45);
            Graphics gr = Graphics.FromImage(tmpFile);
            gr.DrawImage(newImage, new Point((45 - newWidth) / 2, 0));

            return tmpFile;
        }

        public void CutSymbol(Bitmap image)
        {
            RectangleF cloneRect = new RectangleF(_left, _top, _right - _left + 2, _buttom - _top + 2);
            System.Drawing.Imaging.PixelFormat format =
                image.PixelFormat;
            Bitmap cloneBitmap = image.Clone(cloneRect, format);

            Pattern = LeadToThePattern(cloneBitmap);
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
            isEmpty = false;
            CountCenter();
        }

        private void CountCenter()
        {
            _x = (_left + _right) / 2;
            _y = (_top + _buttom) / 2;
        }
    }
}
