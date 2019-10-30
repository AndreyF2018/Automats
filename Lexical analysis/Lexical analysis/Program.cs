using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Lexical_analysis
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            using (FileStream fstream = File.OpenRead("input.txt"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                str = System.Text.Encoding.Default.GetString(array).ToLower();
            }

            Automat a = new Automat("StrAutomat.txt");

            // List<KeyValuePair<bool, int>> resultMaxString = new List<KeyValuePair<bool, int>>();
            KeyValuePair<bool, int> resultMaxString = new KeyValuePair<bool, int>();
            int k = 0;
            StreamWriter sw = new StreamWriter("output.txt");
            while (k < str.Length)
            {
                resultMaxString = a.MaxString(str, k);
                if (resultMaxString.Key)
                {
                    sw.WriteLine(str.Substring(k, resultMaxString.Value));
                    Console.WriteLine("Result: " + str.Substring(k, resultMaxString.Value));
                    if (resultMaxString.Value == 0)
                    {
                        k++;
                    }
                    else
                    {
                        k += resultMaxString.Value;
                    }
                }
                else
                {
                    k++;
                }
            }
            sw.Close();


        }
    }
}
