using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Regular_expression
{
    class CreateAutomat
    {
        //private List<Token> tokens;

        //public CreateAutomat(string fileName)
        //{
        //    this.tokens = TokensRead(fileName);
        //}
        public static char[] ReadAlphabet(string fileName)
        {
            string lines = File.ReadAllText(fileName);
            char[] result = lines.Split().Select(n => Convert.ToChar(n)).ToArray();
            return result;

        }
        public List<Token> TokensRead(string fileName)
        {
            string[] str = new string[3];
            List<Token> tokens = new List<Token>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() != -1)
                {
                    str = sr.ReadLine().Split(':');
                    string[] reg = str[2].Split(' ');
                    tokens.Add(new Token(str[0], int.Parse(str[1]), reg));
                }

            }
            char[] alphabet = ReadAlphabet("Alphabet.txt");
            Console.WriteLine(tokens[0].ToString());
            //tokens[0].ConformRegex(alphabet);
            tokens[0].Test(alphabet);
           
            string[][] sepReg = tokens[0].SeparateRegExp();

            return tokens;
        }
    }
}
