﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Lexical_analysis
{
    class Program
    {
        public static List<Automat> CreateAutomats()
        {
            List<Automat> A = new List<Automat>();
            A.Add(new Automat("CommentsAutomat.txt"));
            A.Add(new Automat("IDAutomat.txt"));
            A.Add(new Automat("KeyWordAutomat.txt"));
            A.Add(new Automat("NumAutomat.txt"));
            A.Add(new Automat("OpEqAutomat.txt"));
            A.Add(new Automat("OperationsAutomat.txt"));
            A.Add(new Automat("ParentheresCloseAutomat.txt"));
            A.Add(new Automat("ParentheresOpenAutomat.txt"));
            A.Add(new Automat("SemicolonAutomat.txt"));
            A.Add(new Automat("StrAutomat.txt"));
            A.Add(new Automat("WhiteSpaceAutomat.txt"));

            return A;


        }
        static void Main(string[] args)
        {
            string str;
            string origStr;
            using (FileStream fstream = File.OpenRead("input.txt"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                str = System.Text.Encoding.Default.GetString(array).ToLower();
                origStr = str;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(str);
            for (int i = 0; i < sb.Length; i++)
            {
                if (Char.IsWhiteSpace(sb[i]))
                {
                    sb.Replace(sb[i], '@');
                }
            }

            str = sb.ToString();
            //Automat a = new Automat("NumAutomat.txt");

            // List<KeyValuePair<bool, int>> resultMaxString = new List<KeyValuePair<bool, int>>();
            List<Automat> A = CreateAutomats();
            KeyValuePair<bool, int> resultMaxString = new KeyValuePair<bool, int>();
            int k = 0;
            StreamWriter sw = new StreamWriter("output.txt");
            while (k < str.Length)
            {
                    string curLex = String.Empty;
                    int curPr = 0;
                    int m = 0;
                    foreach (var M in A)
                    {
                        resultMaxString = M.MaxString(str, k);
                        if (resultMaxString.Key)
                        {
                            if (m < resultMaxString.Value)
                            {
                                curLex = M.GetLexeme();
                                curPr = M.GetPriority();
                                m = resultMaxString.Value;
                            }
                            else if (m == resultMaxString.Value && curPr < M.GetPriority())
                            {
                                curLex = M.GetLexeme();
                                curPr = M.GetPriority();
                                m = resultMaxString.Value;
                            }
                        }
                    }
                    if (m > 0)
                    {
                        sw.WriteLine("<" + curLex + ", " + origStr.Substring(k, m) + ">");
                        Console.WriteLine("<" + curLex + ", " + origStr.Substring(k, m) + ">");
                    k += m;
                    }
                    else if (m == 0)
                    {
                    sw.WriteLine("<Error, " + origStr[k] + ">");
                    Console.WriteLine("<Error, " + origStr[k] + ">");
                    k++;
                    }
            }
            sw.Close();
        }
    }
}

