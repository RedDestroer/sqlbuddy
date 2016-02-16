using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SqlBuddy.Domain;

namespace SqlBuddy.Parsers
{
    public class DataAccessBuilder
    {
        private readonly IList<string> _errors;
        private DataAccess _dataAccess;

        public DataAccessBuilder()
        {
            _errors = new List<string>();
        }

        public void Build(string specification)
        {
            if (specification == null) throw new ArgumentNullException("specification");

            _errors.Clear();

            var input = new AntlrInputStream(specification);

            var lexer = new DataAccessLexer(input);
            lexer.AddErrorListener(new LexerStringLogListener(_errors));

            var tokens = new CommonTokenStream(lexer);

            var parser = new DataAccessParser(tokens);
            parser.AddErrorListener(new ParserStringLogListener(_errors));

            if (_errors.Count > 0)
                return;

            IParseTree tree = parser.compileUnit();

            var visitor = new DataAccessVisitor();
            _dataAccess = visitor.Visit(tree);
        }

        public DataAccess GetProduct()
        {
            if (_errors.Count > 0)
                throw new InvalidOperationException("Can't build product.");

            return _dataAccess;
        }

        public IList<string> Errors
        {
            get { return new ReadOnlyCollection<string>(_errors); }
        }
    }
}