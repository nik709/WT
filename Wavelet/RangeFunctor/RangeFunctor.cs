namespace Wavelet.RangeFunctor {
    
    public abstract class RangeFunctor {
        private readonly string mTag;

        protected RangeFunctor(string tag) {
            mTag = tag;
        }

        public string Tag() {
            return mTag;
        }

        /* starting value in range */
        public abstract double Start();
        
        /* ending value in range */
        public abstract double End();
        
        /* total steps */
        public abstract int Steps();
        
        /* evaluates value for specified step */
        public abstract double Evaluate(int i);
    }
}