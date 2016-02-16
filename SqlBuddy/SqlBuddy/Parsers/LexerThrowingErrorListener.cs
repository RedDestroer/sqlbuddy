using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace SqlBuddy.Parsers
{
    public class LexerThrowingErrorListener
        : IAntlrErrorListener<int>
    {
        public static readonly LexerThrowingErrorListener Instance = new LexerThrowingErrorListener();

        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ParseCanceledException("Lexer error. Line " + line + ":" + charPositionInLine + " " + msg);
        }
    }
}