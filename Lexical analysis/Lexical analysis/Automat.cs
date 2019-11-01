using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lexical_analysis
{
    public class Automat
    {
        private int priority {get; set;}
        private string lexeme {get; set;}
        private int[] Q = new int[1];
        public char[] Sigma = new char[1];
        string[,] T = new string[1,1];
        private int S { get; set; }
        private int[] F = new int[1];

        public Automat(string fileName)
        {
            priority = int.Parse(File.ReadLines(fileName).Skip(0).First());
            lexeme = File.ReadLines(fileName).Skip(1).First();
            string[,] table = DataReadStates(fileName);

            //for (int i = 0; i < table.GetLength(0); i++)
            //{
            //    for (int j = 0; j < table.GetLength(1); j++)
            //    {
            //        Console.Write(table[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            Q = new int[table.GetLength(0)-1];
            for (int i = 0; i < Q.Length; i++)
            {
                Q[i] = int.Parse(table[i + 1, 0]);
                //Console.WriteLine(Q[i]);
            }
            Sigma = DataReadSigma(fileName);
     
            T = new string[table.GetLength(0), table.GetLength(1)];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    T[i, j] = table[i, j];

                }
            }
            this.S = int.Parse(table[1, 0]);
            this.F = DataReadF(fileName);

        }
        public string[,] DataReadStates(string fileName)
        {
            int countCol = int.Parse(File.ReadAllLines(fileName).Skip(4).First());
            int countStr = int.Parse(File.ReadAllLines(fileName).Skip(5).First());
            int skipIndex = 6;
            //string lines = File.ReadAllLines("Automat.txt").Skip(skipIndex).First();
            string[,] states = new string[countCol, countStr];
            for (int i = 0; i < countCol; i++)
            {
                string lines = File.ReadAllLines(fileName).Skip(skipIndex).First();
                skipIndex++;
                string[] temp = lines.Split(' ');
                for (int j = 0; j < temp.Length; j++)
                {
                    states[i, j] = temp[j];
                }

            }
            return states;

        }
        public int[] DataReadF(string fileName)
        {
            string lines = File.ReadLines(fileName).Skip(3).First();
            int[] result = lines.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            return result;

        }
        public char[] DataReadSigma(string fileName)
        {
            string lines = File.ReadLines(fileName).Skip(2).First();
            //char[] result = lines.Split(' ').Select(n => Convert.ToChar(n)).ToArray();
            char[] result = lines.Split().Select(n => Convert.ToChar(n)).ToArray();
            return result;

        }
        
        public int GetPriority()
        {

            return priority;
        }

        public string GetLexeme()
        {
            return lexeme;
        }


        public int GetIndexElse(char[] array, char value)
        {
            int resultIndex = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    resultIndex = i;
                    break;
                }
            }
            return resultIndex;
        }

        public int GetIndexForComments(char value)
        {
            int resultIndex = -1;
            if (value == '{')
            {
                resultIndex = 0;
            }
            else if (value == '}')
            {
                resultIndex = 2;
            }
            else
            {
                resultIndex = 1;
            }
            return resultIndex;
        }

        public int GetIndexForID (char value)
        {
             int resultIndex = -1;
            if (Char.IsLetter(value))
            {
                resultIndex = 0;
            }
            else if (Char.IsDigit(value))
            {
                resultIndex = 1;
            }
            else 
            {
                resultIndex  = 2;
            }
            return resultIndex;
        }

        public int GetIndexForStr(char value)
        {
            int resultIndex = -1;
            if (value == '"')
            {
                resultIndex = 0;
            }
            else
            {
                resultIndex = 1;
            }
            return resultIndex;
        }

        public int GetIndex(string lexeme, char value)
        {
            int resultIndex = -1;
            if (lexeme == "Comments")
            {
                resultIndex = GetIndexForComments(value);
            }
            else if (lexeme == "ID")
            {
                resultIndex = GetIndexForID(value);
            }
            else if(lexeme == "Str")
            {
                resultIndex = GetIndexForStr(value);
            }
            else
            {
                resultIndex = GetIndexElse(Sigma, value);
            }

            return resultIndex;
        }



        // public List<KeyValuePair<bool, int>> MaxString(string str, int l)
        public KeyValuePair<bool, int> MaxString(string str, int l)
        {
            int m = 0;
            bool res = false;
            //List<KeyValuePair<bool, int>> result = new List<KeyValuePair<bool, int>>();
            KeyValuePair<bool, int> kvp = new KeyValuePair<bool, int>(res, m);
            int curState = this.S;
            int i = l;
            if (F.Contains(curState))
            {
                res = true;

            }
            while (i < str.Length)
            {
                if (!Sigma.Contains(str[i]))
                {
                    kvp = new KeyValuePair<bool, int>(res, m);
                    //result.Add(kvp);
                    return kvp;
                }
                else
                {

                    //int strIndex = GetIndexElse(Sigma, str[i]);
                    int strIndex = GetIndex(lexeme, str[i]);
                    int strIndexNext = strIndex;
                    if (i != str.Length - 1)
                    {
                        strIndexNext = GetIndexElse(Sigma, str[i + 1]);
                    }
                    if (this.T[curState + 1, strIndex + 1] != "z")
                    {
                        //if (strIndexNext != strIndex)
                        //{
                        //    m = i - l + 1;
                        //}
                        curState = int.Parse(this.T[curState + 1, strIndex + 1]);
                        if (F.Contains(curState))
                        {
                            m = i - l + 1;
                            res = true;
                            if (strIndex == strIndexNext)
                            {
                                //m++;
                                //m = i - l + 1;
                                kvp = new KeyValuePair<bool, int>(res, m);
                                //result.Add(kvp);
                                return kvp;
                            }
                        }
                    }
                    else
                    {
                        kvp = new KeyValuePair<bool, int>(res, m);
                        //result.Add(kvp);
                        return kvp;
                    }
                    i++;
                }


            }
            kvp = new KeyValuePair<bool, int>(res, m);
            //result.Add(kvp);
            return kvp;
        }

    }
}
