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
            string[,] table = DataRead();

            for (int i = 0; i < Q.Length; i++)
            {
                this.Q[i] = i + 1;
            }

            //this.Sigma = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '+', '.' };
            for (int i = 0; i < table.GetLength(1)-1; i++)
            {
                this.Sigma[i] = Char.Parse(table[0, i+1]);
            }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    this.T[i, j] = table[i, j];

                }
            }

            this.S = int.Parse(table[0, 1]);

            this.F = new int[] {1, 4, 5, 6, 8, 10, 12, 14, 15, 18, 19, 22, 23, 24, 27, 29, 30, 33, 34, 36, 37, 39, 42, 43};

        }
        public string [,] DataRead()
        {
            string[] lines = File.ReadAllLines("Automat.txt");
            string[,] states = new string[lines.Length, lines[0].Split(' ').Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                {
                    //if ((i != 0 && j != 0) || (i == 0 && j !=0) || (i!= 0 && j==0))
                    //{
                    states[i, j] = temp[j];
                    //}
                }
            }
            //for (int i = 0; i < states.GetLength(0); i++)
            //{

            //    for (int j = 0; j < states.GetLength(1); j++)
            //    {
            //        Console.Write(states[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            return states;

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

        //public void NumberSearch(string str)
        //{
        //    string number = String.Empty;
        //    List<KeyValuePair<bool, int>> result = new List<KeyValuePair<bool, int>>();
        //    int curState = this.S;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        int strIndex = GetIndex(Sigma, str[i]);
        //        if (this.T[curState + 1, strIndex] != "z")
        //        {

        //            curState = int.Parse(this.T[curState + 1, strIndex]);
        //            number = number + str[i].ToString();
        //            if (F.Contains(curState))
        //            {
        //                //Console.WriteLine("True: {0}", this.Sigma.GetValue(strIndex));

        //            }
        //            else
        //            {
        //                Console.WriteLine("False: {0}", this.Sigma.GetValue(strIndex));
        //            }
        //        }
        //    }
        //}

        public List<KeyValuePair<bool, int>> MaxString(string str, int l)
        {
            int m = 0;
            bool res = false;
            List<KeyValuePair<bool, int>> result = new List<KeyValuePair<bool, int>>();
            KeyValuePair<bool, int> kvp = new KeyValuePair<bool, int>(res, m);
            int curState = this.S;
            int i = l;
            string digit = String.Empty;
            List<string> digitList = new List<string>();
            List<string> digitListInF = new List<string>();
            while (i < str.Length)
            {
                int strIndex = GetIndex(Sigma, str[i]);
                int strIndexNext = strIndex;
                if (i != str.Length - 1)
                {
                    strIndexNext = GetIndex(Sigma, str[i + 1]);
                }
                if (this.T[curState + 1, strIndex + 1] != "z")
                {
                    curState = int.Parse(this.T[curState + 1, strIndex + 1]);
                }
                else if (this.T[curState + 1, strIndex + 1] == "z")
                {
                    if (digitListInF.Count == 0)
                    {
                       // digitList.Add(digit);
                    }
                    else
                    {
                        //digitList.Add(digitListInF.Last());
                    }
                    digit = String.Empty;
                    curState = this.S;
                    continue;

                }
                    if (F.Contains(curState))
                    {
                        
                        //m = i - l + 1;
                        res = true;
                        //kvp = new KeyValuePair<bool, int>(res, m);
                        digit = digit + str[i];
                        digitList.Add(digit);
                        if (this.T[curState + 1, strIndexNext + 1] == "z" && strIndex != strIndexNext)
                        {
                            Console.WriteLine("ЗАШОЛ 1 " + digit);
                            digitListInF.Add(digit);
                        }
                        else if (this.T[curState + 1, strIndexNext + 1] != "z" && strIndex == strIndexNext)
                        {
                            Console.WriteLine("ЗАШОЛ 2 " + digit);
                            digitListInF.Add(digit);
                        }
                        else if (strIndex == strIndexNext && this.T[curState + 1, strIndexNext + 1] != "z" && !F.Contains(int.Parse(this.T[curState + 1, strIndexNext + 1])))
                        {
                            digitListInF.Add(digit);
                        }
                        //if (i == str.Length - 1)
                        //{
                        //    digitList.Add(digit);
                        //}
                        //Console.WriteLine(str[i] + " " + T[curState, 0]);
                    }
                    else
                    {
                        digit = digit + str[i];
                    }
                
                i++;
            }
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item.Key + " " + item.Value);
            //}
            //return result;
            var validDigits = digitList.Intersect(digitListInF);
            foreach (var item in digitListInF)
            {
                Console.WriteLine(item);
            }

            //m = validDigits.Max(len => len.Length);
            kvp = new KeyValuePair<bool, int>(res, m);
            result.Add(kvp);
            //Console.WriteLine(result[0]);
            return result;
        }

        //public List<KeyValuePair<bool, int>> MaxString(string str, int l)
        //{
        //    int m = 0;
        //    bool res = false;
        //    List<KeyValuePair<bool, int>> result = new List<KeyValuePair<bool, int>>();
        //    KeyValuePair<bool, int> kvp = new KeyValuePair<bool, int>(res, m);
        //    int curState = this.S + 1;
        //    int i = l;

        //    while (i < str.Length)
        //    {
        //        int strIndex = GetIndex(Sigma, str[i]);
        //        if (this.T[curState + 1, strIndex + 1] != "z")
        //        {
        //            curState = int.Parse(this.T[curState + 1, strIndex + 1]);
        //        }
        //        else
        //        {

        //            curState = this.S + 1;
        //        }
        //        if (F.Contains(curState))
        //        {
        //            m = i - l + 1;
        //            res = true;
        //            kvp = new KeyValuePair<bool, int>(res, m);
                    
        //            Console.WriteLine(str[i] + " " + m);
        //        }
        //        i++;
        //    }
        //    //foreach (var item in result)
        //    //{
        //    //    Console.WriteLine(item.Key + " " + item.Value);
        //    //}
        //    //return result;

        //    result.Add(kvp);
        //    //Console.WriteLine(result[0]);
        //    return result;
        //}




    }
}
