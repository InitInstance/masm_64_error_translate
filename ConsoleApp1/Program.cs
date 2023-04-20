using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string inputFile = args.Length > 0 ? args[0] : "";

            List<string> listLanguage = new List<string>();
            List<string> listError = new List<string>();

            try
            {
                listLanguage = GetEngRus("ConsoleApp1.error-translate.csv");
            }
            catch
            {
                MessageBox.Show($"error file \"error-translate.csv\" not found");
            }

            try
            {
                listError = GetFileError(inputFile);
            }
            catch
            {
                MessageBox.Show($"input file \"{inputFile}\" not found");
            }

            Translate(listLanguage, listError);

            try
            {
                File.WriteAllLines(inputFile, listError);
            }
            catch
            {

            }
        }

        public static void Translate(List<string> listLanguage, List<string> listError)
        {
            for (int i = 0; i < listError.Count; i++)
            {
                Program.GetTranslate(listLanguage, i, listError);
            }
        }

        public static void GetTranslate(List<string> listLanguage, int x, List<string> listError)
        {
            List<string> list = new List<string>();

            for (int y = 0; y < listLanguage.Count; y++)
            {
                string[] engrus = listLanguage[y].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int index = listError[x].IndexOf(engrus[0]);
                if (index != -1)
                {
                    listError[x] = listError[x].Replace(engrus[0], engrus[1]);
                }
            }
        }

        public static List<string> GetFileError(string file)
        {
            List<string> listError = new List<string>();

            using (StreamReader streamReader = new StreamReader(file))
            {
                while (!streamReader.EndOfStream)
                {
                    listError.Add(streamReader.ReadLine());
                }
            }

            return listError;
        }

        public static List<string> GetEngRus(string file)
        {
            List<string> listLanguage = new List<string>();

            var assemble = System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream stream = assemble.GetManifestResourceStream(file))
            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    listLanguage.Add(streamReader.ReadLine());
                }
            }

            return listLanguage;
        }
    }
}
