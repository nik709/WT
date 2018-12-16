using System;

namespace Wavelet.RangeFunctor {
    public class LinearRangeFunctor : RangeFunctor {
        private const string TAG = "LinearRange";

        private readonly double mStart;
        private readonly double mStep;
        private readonly double mEnd;
        private readonly int mSteps;
        
        public LinearRangeFunctor(double start, double step, double end) : base(TAG) {
            if (start > end || step <= 0.0) {
                throw new ArgumentException("Invalid range passed!");
            }
            mStart = start;
            mStep = step;
            mSteps = (int) (Math.Floor((end - start) / step) + 1);
            mEnd = mStart + (mSteps - 1) * mStep;
        }

        public override double Start() {
            return mStart;
        }

        public override double End() {
            return mEnd;
        }

        public override int Steps() {
            return mSteps;
        }

        public override double Evaluate(int i) {
            if(i < 0 || i >= mSteps)
                throw new IndexOutOfRangeException("Index out if bounds!");
            
            return mStart + i * mStep;
        }
    }
}