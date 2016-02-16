using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace SqlBuddy.Parsers
{
    public class LexerStringLogListener
        : IAntlrErrorListener<int>
    {
        private readonly IList<string> _log;

        public LexerStringLogListener(IList<string> log)
        {
            if (log == null) throw new ArgumentNullException("log");

            _log = log;
        }

        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            _log.Add("Lexer error. Line " + line + ":" + charPositionInLine + " " + msg);
        }
    }
}