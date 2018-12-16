using System.IO;

namespace Wavelet {
    
    public class FileWriter {

        private static FileWriter mInstance;
        
        private FileWriter() {}

        public static FileWriter getInstance() {
            return mInstance ?? (mInstance = new FileWriter());
        }

        public void writeWaveletTransformToFile(string fileName, WTransform transform) {
            var writer =  new StreamWriter(fileName);

            for (var i = 0; i < transform.rows(); ++i) {
                for (var j = 0; j < transform.cols(); ++j) {
                    writer.Write(transform.mag(i, j));
                }
                writer.Write("\n");
            }
        }
    }
}