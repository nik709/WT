using System;

namespace Wavelet.Wavelet {
    public class ComplexMorlet : Wavelet {
        private const string TAG = "ComplexMorlet";
        
        /* Default central frequency value*/
        private const double FC = 0.8;
        
        /* Default bandwidth parameter */
        private const double FB = 2.0;
        
        private double mFc;    // central frequency
        private double mFb;    // bandwidth parameter
        private double mC;     // L2 norm
        // effective support params
        private double mEffl;
        private double mEffr;

        public ComplexMorlet() : base(TAG) {
            mFc = FC;
            mFb = FB;
            // compute L2 norm ...
            mC = 1.0 / Math.Sqrt(Math.PI * mFb);
            // ... and effective support boundary values
            mEffl = -2.0 * mFb;
            mEffr = +2.0 * mFb;
        }

        public override double reT(double t) {
            return mC * Math.Exp(-(t*t) / mFb) * Math.Cos(2.0 * Math.PI * mFc * t);
        }

        public override double imT(double t) {
            return mC * Math.Exp(-(t*t) / mFb) * Math.Sin(2.0 * Math.PI * mFc * t);
        }

        public override double reF(double w) {
            var br = w - 2.0 * Math.PI * mFc;
            return Math.Exp(-mFb * br * br / 4.0);
        }

        public override double imF(double t) {
            return 0.0;
        }

        public override double cFreq() {
            return mFc;
        }

        public override double effL() {
            return mEffl;
        }

        public override double effR() {
            return mEffr;
        }
    }
}