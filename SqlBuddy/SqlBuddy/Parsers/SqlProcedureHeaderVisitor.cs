using System;
using System.Collections.Generic;
using System.Data;
using SqlBuddy.Conversion;
using SqlBuddy.Domain;

namespace SqlBuddy.Parsers
{
    public class SqlProcedureHeaderVisitor
        : SqlProcedureHeaderBaseVisitor<IEnumerable<SqlParameterDefinition>>
    {
        private IEnumerable<SqlParameterDefinition> _sqlParameterDefinitions;

        public SqlProcedureHeaderVisitor()
        {
            _sqlParameterDefinitions = new List<SqlParameterDefinition>();
        }

        public override IEnumerable<SqlParameterDefinition> VisitCompileUnit(SqlProcedureHeaderParser.CompileUnitContext context)
        {
            _sqlParameterDefinitions = base.VisitCompileUnit(context) ?? _sqlParameterDefinitions;

            return _sqlParameterDefinitions;
        }

        public override IEnumerable<SqlParameterDefinition> VisitProcedureParamNode(SqlProcedureHeaderParser.ProcedureParamNodeContext context)
        {
            _sqlParameterDefinitions = base.VisitProcedureParamNode(context) ?? _sqlParameterDefinitions;

            // Get name of parameter
            var paramIdentifierContext = context.paramIdentifier();
            var paramIdentifier = paramIdentifierContext.GetText();
            var paramTypeContext = context.paramType();
            Tuple<SqlDbType, Type, int?, int?, bool> tuple = GetTypeFromContext(paramTypeContext);
            SqlDbType sqlDbType = tuple.Item1;
            Type type = tuple.Item2;
            int? precision = tuple.Item3;
            int? scale = tuple.Item4;
            bool isNullable = tuple.Item5;
            var sqlTypeDefinition = new SqlTypeDefinition(sqlDbType, type, precision, scale);
            var direction = Direction.Input;

            // Decide if we have default value
            DefaultValue defaultValue = null;
            if (context.ASSIGNMENT() != null)
            {
                object @default = GetDefaultValue(type, context.defaultLiteral());

                defaultValue = new DefaultValue(@default);
            }

            if (context.OUTPUT() != null)
            {
                direction = Direction.InputOutput;
            }

            /*
                if (defaultValue == null && isNullable)
                    isNullable = false;
            */

            var sqlParameterDefinition = new SqlParameterDefinition(paramIdentifier, sqlTypeDefinition, defaultValue, direction, (uint)paramIdentifierContext.Start.Line, (uint)paramIdentifierContext.Start.Column);
            sqlParameterDefinition.Context[SqlParameterContextKeys.IsNullable] = isNullable.ToString();
            AddSqlParameterDefinition(sqlParameterDefinition);

            return _sqlParameterDefinitions;
        }

        private object GetDefaultValue(Type type, SqlProcedureHeaderParser.DefaultLiteralContext context)
        {
            if (context.NULL() != null)
            {
                object result;
                if (DataConversion.TryConvert(type, null, out result))
                    return result;

                throw new InvalidOperationException(string.Format("Can't convert '{0}' to type {1}", "NULL", type.FullName));
            }

            if (context.TRUE() != null)
            {
                return true;
            }

            if (context.FALSE() != null)
            {
                return false;
            }

            if (context.INTEGER_LITERAL() != null)
            {
                string text = context.INTEGER_LITERAL()
                                     .GetText();

                if (context.MINUS() != null)
                    text = "-" + text;

                object result;
                if (DataConversion.TryConvert(type, text, out result))
                    return result;

                throw new InvalidOperationException(string.Format("Can't convert '{0}' to type {1}", text, type.FullName));
            }

            if (context.REAL_LITERAL() != null)
            {
                string text = context.REAL_LITERAL()
                                     .GetText();

                if (context.MINUS() != null)
                    text = "-" + text;

                object result;
                if (DataConversion.TryConvert(type, text, out result))
                    return result;

                throw new InvalidOperationException(string.Format("Can't convert '{0}' to type {1}", text, type.FullName));
            }

            if (context.STRING_LITERAL() != null)
            {
                var text = context.STRING_LITERAL().GetText();
                if (text == null)
                    return null;

                if (text == string.Empty)
                    return string.Empty;

                if (text.Length < 2)
                    return text;

                return text.Substring(1, text.Length - 2);
            }

            throw new InvalidOperationException("Cant figure out default value.");
        }

        private void AddSqlParameterDefinition(SqlParameterDefinition sqlParameterDefinition)
        {
            ((List<SqlParameterDefinition>)_sqlParameterDefinitions).Add(sqlParameterDefinition);
        }

        private Tuple<SqlDbType, Type, int?, int?, bool> GetTypeFromContext(SqlProcedureHeaderParser.ParamTypeContext context)
        {
            SqlDbType sqlDbType;
            int? precision = null;
            int? scale = null;
            bool isNullable = true;

            var ctx = context.paramTypeName();

            if (ctx.BIGINT() != null)
                sqlDbType = SqlDbType.BigInt;
            else if (ctx.BINARY() != null)
                sqlDbType = SqlDbType.Binary;
            else if (ctx.BIT() != null)
                sqlDbType = SqlDbType.Bit;
            else if (ctx.CHAR() != null)
                sqlDbType = SqlDbType.Char;
            else if (ctx.DECIMAL() != null)
                sqlDbType = SqlDbType.Decimal;
            else if (ctx.NUMERIC() != null)
                sqlDbType = SqlDbType.Money;
            else if (ctx.FLOAT() != null)
                sqlDbType = SqlDbType.Float;
            else if (ctx.IMAGE() != null)
                sqlDbType = SqlDbType.Image;
            else if (ctx.INTEGER() != null)
                sqlDbType = SqlDbType.Int;
            else if (ctx.MONEY() != null)
                sqlDbType = SqlDbType.Money;
            else if (ctx.NCHAR() != null)
                sqlDbType = SqlDbType.NChar;
            else if (ctx.NTEXT() != null)
                sqlDbType = SqlDbType.NText;
            else if (ctx.NVARCHAR() != null)
                sqlDbType = SqlDbType.NVarChar;
            else if (ctx.REAL() != null)
                sqlDbType = SqlDbType.Real;
            else if (ctx.UNIQUEIDENTIFIER() != null)
                sqlDbType = SqlDbType.UniqueIdentifier;
            else if (ctx.SMALLDATETIME() != null)
                sqlDbType = SqlDbType.SmallDateTime;
            else if (ctx.SMALLINT() != null)
                sqlDbType = SqlDbType.SmallInt;
            else if (ctx.SMALLMONEY() != null)
                sqlDbType = SqlDbType.SmallMoney;
            else if (ctx.TEXT() != null)
                sqlDbType = SqlDbType.Text;
            else if (ctx.TIMESTAMP() != null)
                sqlDbType = SqlDbType.Timestamp;
            else if (ctx.TINYINT() != null)
                sqlDbType = SqlDbType.TinyInt;
            else if (ctx.VARBINARY() != null)
                sqlDbType = SqlDbType.VarBinary;
            else if (ctx.VARCHAR() != null)
                sqlDbType = SqlDbType.VarChar;
            else if (ctx.VARIANT() != null)
                sqlDbType = SqlDbType.Variant;
            else if (ctx.XML() != null)
                sqlDbType = SqlDbType.Xml;
            else if (ctx.DATETIME() != null)
                sqlDbType = SqlDbType.DateTime;
            else if (ctx.SYSNAME() != null)
            {
                sqlDbType = SqlDbType.NVarChar;
                precision = 128;
                scale = null;
                isNullable = false;
            }
            else if (ctx.CURSOR() != null)
                throw new NotSupportedException("Type 'CURSOR' not supported by ADO.NET.");
            else
                throw new NotSupportedException(string.Format("Can't figure out a TOKEN '{0}'.", ctx.GetText()));

            var type = Helper.GetNetType(sqlDbType, isNullable);
            precision = precision ?? GetPrecision(context.paramConstrains());
            scale = scale ?? GetScale(context.paramConstrains());

            return Tuple.Create<SqlDbType, Type, int?, int?, bool>(sqlDbType, type, precision, scale, isNullable);
        }

        private int? GetPrecision(SqlProcedureHeaderParser.ParamConstrainsContext context)
        {
            if (context == null)
                return null;

            if (context.MAX() != null)
                return int.MaxValue;

            var precisionContext = context.precision();

            return precisionContext != null
                    ? DataConversion.Convert<int?>(precisionContext.INTEGER_LITERAL()[0].ToString())
                    : null;
        }

        private int? GetScale(SqlProcedureHeaderParser.ParamConstrainsContext context)
        {
            if (context == null)
                return null;

            var scaleContext = context.scale();

            return scaleContext != null
                    ? DataConversion.Convert<int?>(scaleContext.INTEGER_LITERAL()[1].ToString())
                    : null;
        }
    }
}