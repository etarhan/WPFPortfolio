using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFPortfolio.TextEditor
{
    class OpenCloseFile
    {
        public string OpenFile(string fileName)
        {
            string textFileContents;
            using (StreamReader output = new StreamReader(fileName))
            {
                textFileContents = output.ReadToEnd();
            }
            return textFileContents;
        }

        public void SaveFile(string fileName, string whatToWrite)
        {

            using (StreamWriter output = new StreamWriter(fileName))
            {
                output.Write(whatToWrite);
            }
        }
    }
}
