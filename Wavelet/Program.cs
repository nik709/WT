using System;
using Wavelet.RangeFunctor;
using Wavelet.Wavelet;

namespace Wavelet {

    internal static class Program {

        public static void Main(string[] args) {
            
            var s = new Signal(1024, null, null, 1.0, "Test_Signal");
            FileReader.GetInstance().loadSignal("data.txt", s);
            var scales = new LinearRangeFunctor(1.0, 1.0, 128.0);
            var translations = new LinearRangeFunctor(0.0, s.getDt(), s.time());
            var transform = CWTalgorithm.cwt(s, scales, translations, new ComplexMorlet(), 4, "test");
            
            Console.WriteLine("CWT result dimension: " + transform.rows() + "x" + transform.cols());
            Console.WriteLine("Frequency: " + s.getFs() + " Hz");
        }
        
        
    }

}