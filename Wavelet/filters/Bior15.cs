namespace Wavelet.filters {
    
    public class Bior15 : BaseFilter {
        
        public override void LoadTHFilter() {
            mL = 10;
            mZ = 5;
            mFilter = new[] {
                0.01171875, -0.01171875,
                -0.0859375, 0.0859375,
                0.5, 0.5,
                0.0859375, -0.0859375,
                -0.01171875, 0.01171875
            };
        }

        public override void LoadTGFilter() {
            mL = 2;
            mZ = 0;
            mFilter = new[] {
                -0.5, 0.5
            };
        }

        public override void LoadHFilter() {
            mL = 2;
            mZ = 0;
            mFilter = new[] {
                0.5, 0.5
            };
        }

        public override void LoadGFilter() {
            mL = 6;
            mZ = 2;
            mFilter = new[] {
                -0.0625, -0.0625,
                0.5, -0.5,
                0.0625, 0.0625
            };
        }
    }
}