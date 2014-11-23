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

        public Bitmap Pattern { get; set; }

        public Symbol(){}

        public int[] GetVector()
        {
            return Mat.GetVector(Pattern);
        }

        private Bitmap LeadToThePattern2(Bitmap image)
        {
            if (image.Height < 5 && image.Width < 5)
                return new Bitmap(image, 45, 45);

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
            {
                newWidth = image.Width / (image.Height / 45);
                newHeight = 45;
            }
            else
            {
                newHeight = image.Height / (image.Width / 45);
                newWidth = 45;
            }
            Bitmap lal = new Bitmap(image, new Size(newWidth, newHeight));

            Bitmap tmpFile = new Bitmap(45, 45);
            Graphics gr = Graphics.FromImage(tmpFile);
            gr.DrawImage(lal, new Point((45 - newWidth) / 2, 0));
            //Bitmap tmpFile = new Bitmap(image, new Size(newWidth, newHeight));
            return tmpFile;
        }

        public void CutSymbol(Bitmap image)
        {
            RectangleF cloneRect = new RectangleF(_left, _top, _right - _left + 2, _buttom - _top + 2);
            System.Drawing.Imaging.PixelFormat format =
                image.PixelFormat;
            Bitmap cloneBitmap = image.Clone(cloneRect, format);

            Pattern = LeadToThePattern2(cloneBitmap);
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
        }

        private void CountCenter()
        {
            _x = (_left + _right) / 2;
            _y = (_top + _buttom) / 2;
        }
    }
}
