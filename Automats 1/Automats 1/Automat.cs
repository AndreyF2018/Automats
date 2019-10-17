using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automats_1
{
    public class Automat
    {
        
        private int[] Q = new int[43];
        public char[] Sigma = new char[14];
        string[,] T = new string[45, 15];
        private int S { get; set; }
        private int[] F = new int[24];

        public Automat()
        {
            string[,] table = DataReadStates();

                for (int i = 0; i < Q.Length; i++)
                {
                    this.Q[i] = i + 1;
                }

                for (int i = 0; i < table.GetLength(1) - 1; i++)
                {
                    this.Sigma[i] = Char.Parse(table[0, i + 1]);
                }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    this.T[i, j] = table[i, j];

                }
            }

            this.S = int.Parse(table[0, 1]);        
            this.F = DataReadF();

        }
        public string[,] DataReadStates()
        {
            string[] lines = File.ReadAllLines("Automat.txt");
            string[,] states = new string[lines.Length-1, lines[0].Split(' ').Length];
            for (int i = 0; i < lines.Length-1; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                {
                    states[i, j] = temp[j];

                }
            }
            return states;

        }
        public int [] DataReadF()
        {
            string lines = File.ReadLines("Automat.txt").Skip(45).First();
            int[] result = lines.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            return result;

        }
     

        public int GetIndex(char[] array, char value)
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
        public int GetIndex(string str, char value)
        {
            int resultIndex = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == value)
                {
                    resultIndex = i;
                    break;
                }
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

                    int strIndex = GetIndex(Sigma, str[i]);
                    int strIndexNext = strIndex;
                    if (i != str.Length - 1)
                    {
                        strIndexNext = GetIndex(Sigma, str[i + 1]);
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
