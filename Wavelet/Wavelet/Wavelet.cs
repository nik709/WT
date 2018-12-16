namespace Wavelet.Wavelet {
    
    public abstract class Wavelet {
        private readonly string mTag;

        protected Wavelet(string tag) {
            mTag = tag;
        }
        
        /* Real part of a wavelet in Time Domain */
        public abstract double reT(double t);
        
        /* Imaginary part of a wavelet in Time Domain */
        public abstract double imT(double t);
        
        /* Real part of a wavelet in Frequency Domain */
        public abstract double reF(double t);
        
        /* Imaginary part of a wavelet in Frequency Domain */
        public abstract double imF(double t);
        
        /* Central frequency */
        public abstract double cFreq();
        
        /* Left boundary of effective support */
        public abstract double effL();
        
        /* Right boundary of effective support */
        public abstract double effR();

        public string Tag() {
            return mTag;
        }
    }
}