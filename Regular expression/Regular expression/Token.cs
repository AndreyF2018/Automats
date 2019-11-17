using System;
using System.Collections.Generic;
using System.Text;

namespace Regular_expression
{
    public class Token
    {
        private string tokenName;
        private int tokenPriority;
        public string[] tokenRegExp;

        public Token (string name, int priority, string[] regExp)
        {
            tokenName = name;
            tokenPriority = priority;
            tokenRegExp = regExp;
        }

        public string[][] SepRegExp()
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

        public override string ToString()
        {
            return $"{tokenName}: {tokenPriority}: {String.Join(" ", tokenRegExp)}";
        }
    }
}
