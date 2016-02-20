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

        public override DataAccess VisitSchemas(DataAccessParser.SchemasContext context)
        {
            _dataAccess = base.VisitSchemas(context) ?? _dataAccess;

            var dictContext = context.partial_dictionary_initializer();
            if (dictContext != null)
            {
                if (dictContext.ALL() != null)
                    _dataAccess.SchemaRules.IsGoodByDefault = true;
                if (dictContext.NOTALL() != null)
                    _dataAccess.SchemaRules.IsGoodByDefault = false;

                var pairsContext = dictContext.partial_dictionary_pairs();
                if (pairsContext != null)
                {
                    foreach (var pairContext in pairsContext.partial_dictionary_pair())
                    {
                        string pattern = pairContext.STRING_LITERAL()
                                                    .GetText();

                        if (pairContext.BANG() == null)
                        {
                            _dataAccess.SchemaRules.ProcessRules.Add(new IncludeRule(pattern));
                        }
                        else
                        {
                            _dataAccess.SchemaRules.ProcessRules.Add(new ExcludeRule(pattern));
                        }

                        _dataAccess.ProcedureRules[pattern] = new Rules();

                        var listContext = pairContext.string_list2();
                        if (listContext != null)
                        {
                            if (listContext.ALL() != null)
                                _dataAccess.ProcedureRules[pattern].IsGoodByDefault = true;
                            if (listContext.NOTALL() != null)
                                _dataAccess.ProcedureRules[pattern].IsGoodByDefault = false;

                            var stringListContext = listContext.string_list_items2();
                            if (stringListContext != null)
                            {
                                foreach (var node in stringListContext.STRING_LITERAL())
                                {
                                    /// !!!!!
                                }
                            }
                        }
                    }
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