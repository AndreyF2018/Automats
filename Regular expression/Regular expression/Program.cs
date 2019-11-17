using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Regular_expression
{
    class Program
    {
        public static char[] ReadAlphabet(string fileName)
        {
            string lines = File.ReadAllText(fileName);
            char[] result = lines.Split().Select(n => Convert.ToChar(n)).ToArray();
            return result;

        }
        static void Main(string[] args)
        {
            string[] str = new string[3];
            List<Token> tokens = new List<Token>();
            using (StreamReader sr = new StreamReader("RegExp.txt"))
            {
                while (sr.Peek() != -1)
                {
                    str = sr.ReadLine().Split(":");
                    string[] reg = str[2].Split(" ");
                    tokens.Add(new Token(str[0], int.Parse(str[1]), reg));
                }

            }
            char[] alphabet = ReadAlphabet("Alphabet.txt");
            Console.WriteLine(tokens[0].ToString());

            string[][] sepReg = tokens[0].SepRegExp();
            if (sepReg[0].Contains(@"\w"))
            {
                foreach (var item in alphabet)
                {
                    if (Char.IsLower(item))
                    {
                        Console.Write(item);
                    }
                }

            }
        }
    }
}
