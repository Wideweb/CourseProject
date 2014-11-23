using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
    class TextDetector
    {
        private Bitmap _image;
        private int[][] _matrix;
        private List<Symbol> _symbols;

        private int[] dx = {0, 1, 1, 0, -1, -1, 1, -1};
        private int[] dy = {1, 0, 1, -1, 0, 1, -1, -1};

        public TextDetector(Bitmap image)
        {
            _image = image;
            _symbols = new List<Symbol>();
            _matrix = Mat.GetMatrix(_image);
        }

        private void FindBlackPixel()
        {
            Symbol newSymbol;
            for (int i = 0; i < _image.Height; i++)
                for (int j = 0; j < _image.Width; j++)
                    if (_matrix[i][j] == 1)
                    {
                        newSymbol  = new Symbol(_image.Width, _image.Height);
                        AbsorbSymbol(j, i, newSymbol);
                        _symbols.Add(newSymbol);
                    }
        }

        private void AbsorbSymbol(int x, int y, Symbol symbol)
        {
            if (x < 0 || x >= _image.Width || y < 0 || y > _image.Height)
                return;
            if (Mat.isBlack(_image.GetPixel(x, y)))


            symbol.AddBlackPoint(x, y);
            _matrix[y][x] = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    AbsorbSymbol(x + dx[i], y + dy[j], symbol);
        }
    }
}
