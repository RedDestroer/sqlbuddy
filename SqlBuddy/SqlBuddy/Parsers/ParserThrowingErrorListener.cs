using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace SqlBuddy.Parsers
{
    public class ParserThrowingErrorListener
        : BaseErrorListener
    {
        public static readonly ParserThrowingErrorListener Instance = new ParserThrowingErrorListener();

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ParseCanceledException("Parser error. Line " + line + ":" + charPositionInLine + " " + msg);
        }
    }
}