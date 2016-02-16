using SqlBuddy.Domain;

namespace SqlBuddy.Parsers
{
    public class DataAccessVisitor
        : DataAccessBaseVisitor<DataAccess>
    {
        private DataAccess _dataAccess;

        public DataAccessVisitor()
        {
            _dataAccess = new DataAccess();
        }

        public override DataAccess VisitCompileUnit(DataAccessParser.CompileUnitContext context)
        {
            _dataAccess = base.VisitCompileUnit(context) ?? _dataAccess;

            return _dataAccess;
        }

        public override DataAccess VisitClassName(DataAccessParser.ClassNameContext context)
        {
            _dataAccess = base.VisitClassName(context) ?? _dataAccess;

            _dataAccess.ClassName = RemoveDoubleQuotes(context.STRING_LITERAL().GetText());

            return _dataAccess;
        }

        public override DataAccess VisitConnection(DataAccessParser.ConnectionContext context)
        {
            _dataAccess = base.VisitConnection(context) ?? _dataAccess;

            var dictionaryInitializerContext = context.dictionary_initializer();
            var dictionaryPairsContext = dictionaryInitializerContext.dictionary_pairs();

            foreach (var dictionaryPairContext in dictionaryPairsContext.dictionary_pair())
            {
                var key = dictionaryPairContext.identifier().GetText();
                var value = RemoveDoubleQuotes(dictionaryPairContext.literal().GetText());

                _dataAccess.Connection.Add(key, value);

                switch (key)
                {
                    case ConnectionKeys.Database:
                        _dataAccess.DatabaseName = value;
                        break;
                }
            }

            return _dataAccess;
        }

        public override DataAccess VisitGenerator(DataAccessParser.GeneratorContext context)
        {
            _dataAccess = base.VisitGenerator(context) ?? _dataAccess;

            _dataAccess.Generator = RemoveDoubleQuotes(context.STRING_LITERAL().GetText());

            return _dataAccess;
        }

        public override DataAccess VisitTransformators(DataAccessParser.TransformatorsContext context)
        {
            _dataAccess = base.VisitTransformators(context) ?? _dataAccess;

            var stringListContext = context.string_list();
            var stringListItemsContext = stringListContext.string_list_items();
            
            if (stringListItemsContext != null)
            {
                var stringLiteralContext = stringListItemsContext.STRING_LITERAL();

                foreach (var literalContext in stringLiteralContext)
                {
                    var literal = RemoveDoubleQuotes(literalContext.GetText());
                    _dataAccess.Transformators.Add(literal);
                }
            }
            
            return _dataAccess;
        }

        private string RemoveDoubleQuotes(string s)
        {
            if (s == null) 
                return null;

            if (s.Length <= 1) 
                return s;

            if (s.StartsWith("\"") && s.EndsWith("\""))
                return s.Substring(1, s.Length - 2);

            return s;
        }
    }
}