﻿using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic.Matrix
{
    public sealed class MatrixIdentity : ISymbolicMatrix
    {
        private readonly int _size;


        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;


        public MatrixIdentity(int size)
        {
            CasInterface = SymbolicUtils.Cas; 
            _size = size;
        }


        public int Rows => _size;

        public int Columns => _size;

        public bool IsZero()
        {
            return false;
        }

        public bool IsIdentity()
        {
            return true;
        }

        public bool IsDiagonal()
        {
            return true;
        }

        public bool IsSymmetric()
        {
            return true;
        }

        public bool IsOrthogonal()
        {
            return true;
        }

        public bool IsInvertable()
        {
            return true;
        }

        public bool IsFullMatrix()
        {
            return true;
        }

        public bool IsSparseMatrix()
        {
            return false;
        }

        public bool IsSquare()
        {
            return true;
        }

        public bool IsRowVector()
        {
            return (_size == 1);
        }

        public bool IsColumnVector()
        {
            return (_size == 1);
        }

        public MathematicaScalar this[int row, int column] => row == column 
            ? CasConstants.One 
            : CasConstants.Zero;

        public ISymbolicVector Times(ISymbolicVector v)
        {
            return v;
        }

        public ISymbolicMatrix Transpose()
        {
            return this;
        }

        public ISymbolicMatrix Inverse()
        {
            return this;
        }

        public ISymbolicMatrix InverseTranspose()
        {
            return this;
        }

        public MathematicaMatrix ToMathematicaMatrix()
        {
            return MathematicaMatrix.CreateIdentity(CasInterface, _size);
        }

        public MathematicaMatrix ToMathematicaFullMatrix()
        {
            return MathematicaMatrix.CreateIdentity(CasInterface, _size);
        }

        public MathematicaMatrix ToMathematicaSparseMatrix()
        {
            return MathematicaMatrix.CreateIdentity(CasInterface, _size, false);
        }


        public ISymbolicVector GetRow(int row)
        {
            return MathematicaVector.Create(CasInterface, "SparseArray[Rule[" + (row + 1) + ", 1], " + _size + "]");
        }

        public ISymbolicVector GetColumn(int column)
        {
            return MathematicaVector.Create(CasInterface, "SparseArray[Rule[" + (column + 1) + ", 1], " + _size + "]");
        }

        public ISymbolicVector GetDiagonal()
        {
            return MathematicaVector.Create(CasConstants.One, _size);
        }
    }
}
