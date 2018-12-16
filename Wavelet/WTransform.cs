using System;

namespace Wavelet {
    public class WTransform {
        private double[] _re; // real part of transform
        private double[] _im; // imaginary part of transform
        private int _cols; // columns number
        private int _rows; // rows number
        private string _name; // result name
        private Wavelet.Wavelet _wavelet; // mother wavelet used for transform
        private RangeFunctor.RangeFunctor _scales; // scales functor used
        private RangeFunctor.RangeFunctor _translations; // translations functor used

        public WTransform(RangeFunctor.RangeFunctor Scales, RangeFunctor.RangeFunctor Translations,
            Wavelet.Wavelet MotherWavelet, string Name) {
            _name = Name;
            _rows = Scales.Steps();
            _cols = Translations.Steps();

            // transform result cannot be empty
            if (_rows <= 0 || _cols <= 0)
                throw new ArgumentException("Invalid dimensions provided!");

            // copy necessary objects
            _scales = Scales;
            _translations = Translations;
            _wavelet = MotherWavelet;

            // allocate storage
            _re = new double[_rows * _cols];
            _im = new double[_rows * _cols];
        }

        public double re(int row, int col) {
            if (row < 0 || row >= _rows || col < 0 || col >= _cols)
                throw new ArgumentOutOfRangeException();

            return _re[row * _cols + col];
        }

        public double im(int row, int col) {
            if (row < 0 || row >= _rows || col < 0 || col >= _cols)
                throw new ArgumentOutOfRangeException();

            return _im[row * _cols + col];
        }

        /* magnitude */
        public double mag(int row, int col) {
            if (row < 0 || row >= _rows || col < 0 || col >= _cols)
                throw new ArgumentOutOfRangeException();

            int idx = row * _cols + col;
            return Math.Sqrt(_re[idx] * _re[idx] + _im[idx] * _im[idx]);
        }

        /* angle */
        public double ang(int row, int col) {
            if (row < 0 || row >= _rows || col < 0 || col >= _cols)
                throw new ArgumentOutOfRangeException();

            int idx = row * _cols + col;
            return Math.Atan2(_im[idx], _re[idx]);
        }

        public int rows() {
            return _rows;
        }

        public int cols() {
            return _cols;
        }

        public Wavelet.Wavelet motherWavelet() {
            return _wavelet;
        }

        public RangeFunctor.RangeFunctor scales() {
            return _scales;
        }

        public RangeFunctor.RangeFunctor translations() {
            return _translations;
        }

        public string getName() {
            return _name;
        }

        public double[] reData() {
            return _re;
        }

        public double[] imData() {
            return _im;
        }
    }
}