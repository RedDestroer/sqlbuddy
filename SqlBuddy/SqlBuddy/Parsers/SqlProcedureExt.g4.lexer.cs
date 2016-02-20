using Antlr4.Runtime;

namespace SqlBuddy.Parsers
{
    public partial class SqlProcedureExtLexer
    {
        private bool _insideBlock;
        private bool _insideDescription;

        public override IToken NextToken()
        {
            if (!_insideBlock)
            {
                while (true)
                {
                    var nextToken = base.NextToken();
                    if (nextToken.Type == Eof)
                        return nextToken;

                    if (nextToken.Type == OPEN_EXTBLOCK)
                    {
                        _insideBlock = true;

                        return nextToken;
                    }
                }
            }
            else
            {
                var nextToken = base.NextToken();

                if (_insideDescription && nextToken.Type != OPEN_DESCR && nextToken.Type != CLOSE_EXTBLOCK && nextToken.Type != Eof)
                {
                    nextToken = new CommonToken(ANY, nextToken.Text);
                }

                if (nextToken.Type == OPEN_DESCR)
                    _insideDescription = true;

                if (nextToken.Type == CLOSE_EXTBLOCK)
                {
                    _insideBlock = false;
                    _insideDescription = false;
                }

                return nextToken;
            }
        }
    }
}
