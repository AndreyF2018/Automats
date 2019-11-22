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
        static void Main(string[] args)
        {
            CreateAutomat automat = new CreateAutomat();
            automat.TokensRead("RegExp.txt");
        }
    }
}
