using System;
using System.Collections.Generic;
using System.Text;
using Antlr.Runtime;

namespace DatAdmin
{
    public class AntlrParser : Antlr.Runtime.Parser
    {
        public AntlrParser(ITokenStream input, RecognizerSharedState state)
            : base(input, state)
        {
        }
    }
}
