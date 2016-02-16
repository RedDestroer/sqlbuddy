using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace SqlBuddy.Parsers
{
    public class ParserStringLogListener
        : BaseErrorListener
    {
        private readonly IList<string> _log;

        public ParserStringLogListener(IList<string> log)
        {
            if (log == null) throw new ArgumentNullException("log");

            _log = log;
        }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            _log.Add("Parser error. Line " + line + ":" + charPositionInLine + " " + msg);
        }
    }
}