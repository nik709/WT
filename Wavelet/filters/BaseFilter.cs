namespace Wavelet.filters {
    public abstract class BaseFilter : Filter {

        protected double[] mFilter;
        protected int mL;
        protected int mZ;
        
        public abstract void LoadTHFilter();
        
        public abstract void LoadTGFilter();
        
        public abstract void LoadHFilter();
        
        public abstract void LoadGFilter();

        public double[] GetFilter() {
            return mFilter;
        }

        public int getL() {
            return mL;
        }

        public int getZ() {
            return mZ;
        }
    }
}