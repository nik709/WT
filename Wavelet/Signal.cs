using System;

namespace Wavelet {
    
    public class Signal {
        private double[] mRe; // real part
        private double[] mIm; // imaginary part
        private int mLength; // signal length (total samples count)
        private double mFs = 1.0; // sample frequency
        private string mName; // signal name

        public Signal(int length, double[] real, double[] imag, double fs, string name) {
            if (length < 0 || fs <= 0.0) {
                throw new AggregateException("Invalid argument Length or Fs!");
            }

            mFs = fs;
            mLength = length;
            mName = name;

            if (mLength == 0) {
                return;
            }

            mRe = new double[mLength];
            mIm = new double[mLength];
        }
        
        /* Signal length in samples */
        public int length() {
            return mLength;
        }

        /* Signal length in time */
        public double time() {
            return (mLength - 1) / mFs;
        }

        /* Get signal name */
        public string getName() {
            return mName;
        }
        
        /* Get signal sampling frequency */
        public double getFs() {
            return mFs;
        }
        
        /* Get sampling time step value (the same as sampling frequency) */
        public double getDt() {
            return 1.0 / mFs;
        }
        
        /* Get reference to internal real data */
        public double[] reData() {
            return mRe;
        }

        /* Get reference to internal imaginary data */
        public double[] imData() {
            return mIm;
        }

    }
}