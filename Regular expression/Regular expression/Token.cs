using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Regular_expression
{
    public class Token
    {
        private string tokenName;
        private int tokenPriority;
        public string[] tokenRegExp;

        public Token(string name, int priority, string[] regExp)
        {
            tokenName = name;
            tokenPriority = priority;
            tokenRegExp = regExp;
        }

        /// <summary>
        /// Разделяет регулярное выражение по скобкам, если они имеются, возвращая ступенчатый массив 
        /// </summary>
        /// <returns></returns>
        public string[][] SeparateRegExp()
        {
            if (tokenRegExp.Contains("("))
            {
                return ParenthesizedRegExp();
            }
            else
            {
                int lineCount = 1;
                string[][] sepReg = new string[lineCount][];
                sepReg[0] = new string[tokenRegExp.Length];
                for (int i = 0; i < tokenRegExp.Length; i++)
                {
                    sepReg[0][i] = tokenRegExp[i];
                }

                for (int i = 0; i < sepReg.Length; i++)
                {
                    for (int j = 0; j < sepReg[i].Length; j++)
                    {
                        Console.Write(sepReg[i][j] + " ");
                    }
                    Console.WriteLine();
                }

                return sepReg;
            }
        }

        private string[][] ParenthesizedRegExp()
        {
            int lineCount = 0;
            int rowCount = 0;
            List<int> rowCountList = new List<int>();
            string[][] sepReg = new string[lineCount][];
            for (int i = 0; i < tokenRegExp.Length; i++)
            {
                if (tokenRegExp[i] == "(")
                {
                    lineCount++;
                    while (tokenRegExp[i] != ")")
                    {
                        i++;
                        if (tokenRegExp[i + 1] == "*")
                        {
                            rowCount++;
                        }
                        if (tokenRegExp[i] != ")")
                        {
                            rowCount++;
                        }

                    }
                    rowCountList.Add(rowCount);
                    rowCount = 0;
                }
            }
            sepReg = new string[lineCount][];
            for (int i = 0; i < lineCount; i++)
            {
                sepReg[i] = new string[rowCountList[i]];
            }
            int line = -1;
            int row = -1;
            for (int i = 0; i < tokenRegExp.Length; i++)
            {
                if (tokenRegExp[i] == "(")
                {
                    line++;
                    string temp = String.Empty;
                    while (tokenRegExp[i] != ")")
                    {
                        i++;
                        if (tokenRegExp[i + 1] == "*")
                        {
                            row++;
                            sepReg[line][row] = tokenRegExp[i + 1];
                        }
                        if (tokenRegExp[i] != ")")
                        {
                            row++;
                            sepReg[line][row] = tokenRegExp[i];
                        }

                    }
                    row = -1;
                }
            }

            //for (int i = 0; i < sepReg.Length; i++)
            //{
            //    for (int j = 0; j < sepReg[i].Length; j++)
            //    {
            //        Console.Write(sepReg[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            return sepReg;
        }

        /// <summary>
        /// Сопоставляет регулярное выражение и допустимые по нему символы
        /// </summary>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        private Dictionary<string, char[]> ConformRegex(char[] alphabet)
        {
            Dictionary<string, char[]> regexChar = new Dictionary<string, char[]>();
            var lowerChar = alphabet.Where(c => Char.IsLower(c));
            var upperChar = alphabet.Where(c => Char.IsUpper(c));
            var digitsChar = alphabet.Where(c => Char.IsDigit(c));
            var whiteSpaceChar = alphabet.Where(c => Char.IsWhiteSpace(c));
            var openParenthesesChar = alphabet.Where(c => c == '(');
            var closeParenthesesChar = alphabet.Where(c => c == ')');
            var plusChar = alphabet.Where(c => c == '+');
            var multChar = alphabet.Where(c => c == '*');
            regexChar.Add(@"\w", lowerChar.ToArray());
            regexChar.Add(@"\W", upperChar.ToArray());
            regexChar.Add(@"\d", digitsChar.ToArray());
            regexChar.Add(@"\s", whiteSpaceChar.ToArray());
            regexChar.Add(@"\(", openParenthesesChar.ToArray());
            regexChar.Add(@"\)", closeParenthesesChar.ToArray());
            regexChar.Add(@"\+", plusChar.ToArray());
            regexChar.Add(@"\*", multChar.ToArray());
            return regexChar;
        }

        public void Test(char[] alphabet)
        {
            string[][] sepReg = SeparateRegExp();
            Dictionary<string, char[]> regexChar = ConformRegex(alphabet);
            Dictionary<string, char[]> selectedRegexChar = RegExpSelection(sepReg, regexChar);
            UnionRegExp(sepReg, selectedRegexChar);
        }

        /// <summary>
        /// Отбирает те регулярные выражения и допустимые по ним символы, которые присутствуют в строке описания токена
        /// </summary>
        /// <param name="sepReg"></param>
        /// <param name="regexChar"></param>
        /// <returns></returns>
        public Dictionary<string, char[]> RegExpSelection(string [][]sepReg, Dictionary<string, char[]> regexChar)
        {
            Dictionary<string, char[]> selectedRegexChar = new Dictionary<string, char[]>();
            for (int i = 0; i < sepReg.Length; i++)
            {  
                for (int j = 0; j < sepReg[i].Length; j++)
                {
                    if (!selectedRegexChar.Keys.Contains(sepReg[i][j]))
                    {
                        string key = sepReg[i][j];
                        if (regexChar.Keys.Contains(key))
                        {
                            selectedRegexChar.Add(key, regexChar[key]);
                        }
                        else
                        {
                            selectedRegexChar.Add(key, key.ToCharArray());
                        }
                    }
                }
            }

            //foreach(var item in selectedRegexChar)
            //{
            //    Console.WriteLine(item.Value);
            //}

            return selectedRegexChar;
        }

        /// <summary>
        /// Объединяет алфавиты регулярных выражений по символу объединения '|'
        /// </summary>
        /// <param name="sepReg"></param>
        /// <param name="selectedRegexChar"></param>
        public char[][] UnionRegExp(string [][]sepReg, Dictionary<string, char[]> selectedRegexChar)
        {
            char[][] unionRegExp = new char[sepReg.Length][];
            for (int i = 0; i < sepReg.Length; i++)
            {
                if (sepReg[i].Contains("|"))
                {
                    //int row = sepReg[i].Count(c => c.Equals("|"));
                    for (int j = 0; j < sepReg[i].Length; j++)
                    {
                        if (sepReg[i][j] == "|")
                        {
                            //var temp = new char[selectedRegexChar[sepReg[i][j - 1]].Union(selectedRegexChar[sepReg[i][j + 1]]).ToArray().Length];
                            var temp = selectedRegexChar[sepReg[i][j - 1]].Union(selectedRegexChar[sepReg[i][j + 1]]).ToArray();
                            //Console.Write(selectedRegexChar[sepReg[i][j - 1]]);
                            //Console.Write(" | ");
                            //Console.Write(selectedRegexChar[sepReg[i][j + 1]]);
                            //Console.Write(" = ");
                            //Console.Write(temp.ElementAt(0));
                            //Console.WriteLine();
                            if (unionRegExp[i] == null)
                            {
                                unionRegExp[i] = temp;
                            }
                            else
                            {
                                unionRegExp[i] = temp.Union(unionRegExp[i]).ToArray();
                            }
                            
                            //unionRegExp[i] = unionRegExp[i].Union(temp).ToArray();
                        }
                    }
                }
                else
                {
                    unionRegExp[i] = new char[selectedRegexChar[sepReg[i][0]].Length];
                    unionRegExp[i] = selectedRegexChar[sepReg[i][0]];
                }
            }
            for (int i = 0; i < unionRegExp.Length; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < unionRegExp[i].Length; j++)
                {
                    Console.Write(unionRegExp[i][j] + " ");
                }
                Console.WriteLine();
            }
            return unionRegExp;
        }

        public override string ToString()
        {
            return $"{tokenName}: {tokenPriority}: {String.Join(" ", tokenRegExp)}";
        }
    }
}
