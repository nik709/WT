using System;

namespace Wavelet {

    public class CWTalgorithm {
        private static CWTalgorithm mInstance;

        private CWTalgorithm() {
        }

        public static CWTalgorithm getInstance() {
            return mInstance ?? (mInstance = new CWTalgorithm());
        }

        /* Returns real part of complex multiplication */
        private static double cmplxMulRe(double r1, double i1, double r2, double i2) {
            return r1 * r2 - i1 * i2;
        }

        /* Compute fast Fourier transform */
        private void FastFourierTransform(double[] re, double[] im, int n, int off, int isign) {
            int i, j, oi, oj, k, l, le, le1, ip, n2;
            double wpr, wpi, wr, wi, wtr, wti;

            n2 = n >> 1;
            j = 1;
            for (i = 0; i < n - 1; i++) {
                oj = off + j;
                oi = off + i;
                if (i < j) {
                    wtr = re[oj - 1];
                    wti = im[oj - 1];
                    re[oj - 1] = re[oi];
                    im[oj - 1] = im[oi];
                    re[oi] = wtr;
                    im[oi] = wti;
                }

                k = n2;
                while (k < j) {
                    j -= k;
                    k >>= 1;
                }

                j += k;
            }

            l = 1;
            k = n;
            while ((k >>= 1) != 0) {
                le1 = (le = 1 << l++) >> 1;
                wtr = Math.PI / le1;
                wpr = Math.Cos(wtr);
                wpi = -isign * Math.Sin(wtr);
                wr = 1.0;
                wi = 0.0;
                for (j = 0; j < le1; j++) {
                    for (i = j; i < n; i += le) {
                        oi = off + i;
                        ip = oi + le1;
                        wtr = wr * re[ip] - wi * im[ip];
                        wti = wi * re[ip] + wr * im[ip];
                        re[ip] = re[oi] - wtr;
                        im[ip] = im[oi] - wti;
                        re[oi] = re[oi] + wtr;
                        im[oi] = im[oi] + wti;
                    }

                    wr = (wtr = wr) * wpr - wi * wpi;
                    wi = wi * wpr + wtr * wpi;
                }
            }
        }


        public static WTransform cwt(Signal s, RangeFunctor.RangeFunctor Scales,
            RangeFunctor.RangeFunctor Translations, Wavelet.Wavelet MotherWavelet, int ivalp, string Name) {
            // Result
            WTransform wt;
            // references to internal Signal/WTransform data
            double[] s_re, s_im;
            double[] wt_re, wt_im;
            // signal params
            int n = s.length();
            double fs = s.getFs();
            // WT params
            double a, b, T;
            double i, istep;
            // indexes and dimensions
            int dx, dy;
            int rows, cols;
            int row, row_dx;


            // check arguments
            if (Scales.Steps() <= 0 || Translations.Steps() <= 0 || n <= 0 ||
                fs <= 0.0 || ivalp <= 0)
                throw new ArgumentException();

            // create result object
            wt = new WTransform(Scales, Translations, MotherWavelet, Name);

            // obtain result dimensions and references to data
            rows = wt.rows();
            cols = wt.cols();
            wt_re = wt.reData();
            wt_im = wt.imData();
            s_re = s.reData();
            s_im = s.imData();

            // index step (used in convolution stage)
            istep = 1.0 / (double) ivalp;

            // Scales
            for (dy = 0; dy < rows; dy++) {
                // obtain current scale
                a = Scales.Evaluate(dy) * fs;
                if (Math.Abs(a) < 0.0) a = Double.MinValue;

                // set starting index of current row
                row = dy * cols;

                // Translations
                for (dx = 0; dx < cols; dx++) {
                    // obtain current translation
                    b = Translations.Evaluate(dx) * fs;

                    // index of convolution result
                    row_dx = row + dx;

                    // Perform convolution
                    wt_re[row_dx] = 0.0;
                    wt_im[row_dx] = 0.0;
                    for (i = 0.0; i < n; i += istep) {
                        T = (i - b) / a;
                        wt_re[row_dx] += cmplxMulRe(s_re[(int) i], s_im[(int) i],
                            MotherWavelet.reT(T), -MotherWavelet.imT(T));
                        wt_im[row_dx] += cmplxMulRe(s_re[(int) i], s_im[(int) i],
                            MotherWavelet.reT(T), -MotherWavelet.imT(T));
                        // NOTE: "-" before Wavelet imaginary part indicates complex
                        // conjunction.
                    }

                    wt_re[row_dx] *= 1.0 / (Math.Sqrt(a) * ivalp);
                    wt_im[row_dx] *= 1.0 / (Math.Sqrt(a) * ivalp);
                }
            }

            return wt;
        }

    }
}