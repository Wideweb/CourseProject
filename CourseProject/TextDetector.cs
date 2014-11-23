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

        public List<Symbol> Symbols { get { return _symbols; } }

        public TextDetector() { }

        public void Detect(Bitmap image)
        {
            _image = image;
            _symbols = new List<Symbol>();
            _matrix = Mat.GetMatrix(_image);
            FindSymbols();
        }

        public List<int[]> GetVectors()
        {
            List<int[]> vectors = new List<int[]>();
            foreach (var symbol in _symbols)
                vectors.Add(symbol.GetVector());

            return vectors;
        }

        private void FindSymbols()
        {
            Symbol newSymbol;
            for (int i = 0; i < _image.Height; i++)
                for (int j = 0; j < _image.Width; j++)
                    if (_matrix[i][j] == 1)
                    {
                        newSymbol  = new Symbol();
                        AbsorbSymbol(j, i, newSymbol);
                        if (!newSymbol.isEmpty)
                        _symbols.Add(newSymbol);
                    }
            CutSymblos();
        }

        private void AbsorbSymbol(int x, int y, Symbol symbol)
        {
            if (x < 0 || x >= _image.Width || y < 0 || y > _image.Height)
                return;
            if (_matrix[y][x] == 0)
                return;

            symbol.AddBlackPoint(x, y);
            _matrix[y][x] = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    AbsorbSymbol(x + dx[i], y + dy[j], symbol);
        }

        private void CutSymblos()
        {
            foreach (var symbol in _symbols)
                symbol.CutSymbol(_image);
        }
    }
}
