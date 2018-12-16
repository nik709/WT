using System;
using System.IO;

namespace Wavelet {
    
    public class FileReader {

        private static FileReader mInstance;
        private StreamReader mReader;
        
        private FileReader() {}

        public static FileReader GetInstance() {
            return mInstance ?? (mInstance = new FileReader());
        }

        public string ReadAllFile(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                return reader.ReadToEnd();
            }
        }

        public FileReader OpenStreamReader(string fileName) {
            mReader = new StreamReader(fileName);
            return this;
        }
        
        public string NextLine() {
            return mReader.ReadLine();
        }

        public void CloseStreamReader() {
            mReader.Close();
            mReader = null;
        }

        public void loadSignal(string fileName, Signal signal) {
            GetInstance().OpenStreamReader(fileName);
            string line;
            var reData = signal.reData();
            var i = 0;
            while ((line = GetInstance().NextLine()) != null && i < signal.length()) {
                reData[i] = double.Parse(line);
                i++;
            }
        }

    }
}