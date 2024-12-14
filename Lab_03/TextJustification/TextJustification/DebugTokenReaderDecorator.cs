using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextJustification;

class DebugTokenReaderDecorator
{
    public Token Print(Token token, bool printStats = false) 
    {
        if (printStats)
        {
            Console.WriteLine(token.ToString());
        }
        return token;
    }
}
