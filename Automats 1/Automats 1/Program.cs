using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Automats_1
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
                str = System.Text.Encoding.Default.GetString(array).ToUpper();
            }


            Automat a = new Automat();
            foreach (var item in str)
            {
                if (!a.Sigma.Contains(item))
                {
                    str = str.Replace(item.ToString(), "");
                }
            }
            //List<KeyValuePair<bool, int>> slovar = new List<KeyValuePair<bool,int>>();
            //KeyValuePair<bool, int> kvp = new KeyValuePair<bool,int>(true, 5);
            
            //slovar.Add(new KeyValuePair<bool, int>(true, 5));
            Console.WriteLine(a.MaxString(str, 0)[0]); 
            List<KeyValuePair<bool, int>> resultMaxString = new List<KeyValuePair<bool, int>>();
            int k = 0;
            string resultStr = String.Empty;
            //while (k < str.Length)
            //{
            //    resultMaxString = a.MaxString(str, k);
            //    if (resultMaxString[0].Key == true && resultMaxString[0].Value == 6)
            //    {
            //        str = str.Substring(k, resultMaxString[0].Value);

            //        if (resultMaxString[0].Value > 0)
            //        {
            //            k = k + resultMaxString[0].Value;
            //        }
            //        else
            //        {
            //            k++;
            //        }
            //        //Console.WriteLine(str.Substring(k, resultMaxString[0].Value));
            //    }
            //    else
            //    {
            //        k++;
            //    }
            //}
            //Console.WriteLine(str);



            
            //a.getQ();

        }
    }
}
