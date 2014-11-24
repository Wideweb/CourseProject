﻿using System;
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
            SortSymbols();
        }

        public List<int[]> GetVectors()
        {
            List<int[]> vectors = new List<int[]>();
            for (int i = 0; i < _symbols.Count; i++)
                vectors.Add(_symbols[i].GetVector());

            FindSpaces(vectors);

            return vectors;
        }

        public void FindSpaces(List<int[]> vectors)
        {
            var spaceVector = Mat.GetSpaceVector();
            var enterVector = Mat.GetEnterVector();

            var spaces = new List<int>();
            var avWidth = 0;
            var deltaY = 0;
            var deltaX = 0;
            var deltaI = 0;

            for (int i = 1; i < _symbols.Count; i++ )
            {
                deltaY = (_symbols[i - 1].Buttom - _symbols[i].Top);
                if (deltaY < 0)
                {
                    vectors.Insert(i + deltaI, enterVector);
                    deltaI++;
                }
            }
            /*
            for (int i = 1; i < _symbols.Count; i++)
            {
                deltaY = (_symbols[i - 1].Buttom - _symbols[i].Top);
                avWidth = (_symbols[i - 1].Width + _symbols[i].Width) / 2;
                deltaX = (_symbols[i].Left - _symbols[i - 1].Rigt);
                if (deltaX > avWidth / 3 && deltaY > 0)
                {
                    vectors.Insert(vectors.IndexOf(_symbols[i].GetVector()), spaceVector);
                    deltaI++;
                }
            }*/
        }

        private void SortSymbols()
        {
            _symbols.Sort(delegate(Symbol x, Symbol y)
            {
                if (x.CenterY - y.CenterY > 10) return 1;
                if (x.CenterY - y.CenterY < -10) return -1;
                if (x.CenterX >= y.CenterX) return 1;
                if (x.CenterX < y.CenterX) return -1;
                return 0;
            }
            );
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
