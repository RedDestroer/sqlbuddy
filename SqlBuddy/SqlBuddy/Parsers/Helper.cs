using System;
using System.Collections.Generic;
using System.Data;
using SqlBuddy.Domain;

namespace SqlBuddy.Parsers
{
    public class Helper
    {
        private static readonly Dictionary<SqlDbType, Type> DbConversion = new Dictionary<SqlDbType, Type>
                                                                               {
                                                                                   { SqlDbType.BigInt, typeof(long) },
                                                                                   { SqlDbType.Binary, typeof(byte[]) },
                                                                                   { SqlDbType.Bit, typeof(bool) },
                                                                                   { SqlDbType.Char, typeof(char) }, // TODO: need to check
                                                                                   { SqlDbType.DateTime, typeof(DateTime) },
                                                                                   { SqlDbType.Decimal, typeof(decimal) },
                                                                                   { SqlDbType.Float, typeof(float) }, // TODO: need to check
                                                                                   { SqlDbType.Image, typeof(byte[]) },
                                                                                   { SqlDbType.Int, typeof(int) },
                                                                                   { SqlDbType.Money, typeof(decimal) },
                                                                                   { SqlDbType.NChar, typeof(string) }, // TODO: need to check
                                                                                   { SqlDbType.NText, typeof(string) }, // TODO: need to check
                                                                                   { SqlDbType.NVarChar, typeof(string) }, // TODO: need to check
                                                                                   { SqlDbType.Real, typeof(double) }, // TODO: need to check
                                                                                   { SqlDbType.UniqueIdentifier, typeof(Guid) },
                                                                                   { SqlDbType.SmallDateTime, typeof(DateTime) },
                                                                                   { SqlDbType.SmallInt, typeof(short) }, // TODO: need to check
                                                                                   { SqlDbType.SmallMoney, typeof(decimal) }, // TODO: need to check
                                                                                   { SqlDbType.Text, typeof(string) }, // TODO: need to check
                                                                                   { SqlDbType.Timestamp, typeof(byte[]) },
                                                                                   { SqlDbType.TinyInt, typeof(byte) }, // TODO: need to check
                                                                                   { SqlDbType.VarBinary, typeof(byte[]) },
                                                                                   { SqlDbType.VarChar, typeof(string) },
                                                                                   { SqlDbType.Variant, typeof(object) }, // TODO: need to check
                                                                                   { SqlDbType.Xml, typeof(string) }, // TODO: need to check
                                                                                   ////{ SqlDbType.Udt, typeof(???) }, // unsupported
                                                                                   ////{ SqlDbType.Structured, typeof(???) }, // unsupported
                                                                                   { SqlDbType.Date, typeof(DateTime) }, // TODO: need to check
                                                                                   ////{ SqlDbType.Time, typeof(DateTime) }, // unsupported
                                                                                   ////{ SqlDbType.DateTime2, typeof(???) }, // unsupported
                                                                                   ////{ SqlDbType.DateTimeOffset, typeof(???) }, // unsupported
                                                                               };

        private static readonly Dictionary<Type, string> NetConversion = new Dictionary<Type, string>
                                                                             {
                                                                                 { typeof(long), "long" },
                                                                                 { typeof(ulong), "ulong" },
                                                                                 { typeof(byte[]), "byte[]" },
                                                                                 { typeof(bool), "bool" },
                                                                                 { typeof(char), "char" },
                                                                                 { typeof(DateTime), "DateTime" },
                                                                                 { typeof(decimal), "decimal" },
                                                                                 { typeof(float), "float" },
                                                                                 { typeof(int), "int" },
                                                                                 { typeof(uint), "uint" },
                                                                                 { typeof(string), "string" },
                                                                                 { typeof(double), "double" },
                                                                                 { typeof(Guid), "Guid" },
                                                                                 { typeof(short), "short" },
                                                                                 { typeof(ushort), "ushort" },
                                                                                 { typeof(byte), "byte" },
                                                                                 { typeof(object), "object" },
                                                                             };

        public static string Capitalize(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            try
            {
                if (name.Length > 2)
                    return name[0] + name[1].ToString().ToLower() + name.Substring(2, name.Length - 2);
                if (name.Length > 1)
                    return name[0].ToString().ToUpper() + name.Substring(1, name.Length - 1);

                return name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string FilterWhiteSpace(string name)
        {
            string n = string.Empty;
            foreach (var ch in name)
            {
                if (char.IsLetter(ch))
                {
                    n += ch;
                }
                else
                {
                    if (char.IsDigit(ch))
                    {
                        n += ch;
                    }
                    else
                    {
                        if (ch == '_')
                        {
                            n += ch;
                        }
                        else
                        {
                            n += '_';
                        }
                    }
                }
            }

            return n;
        }

        public static Type GetNetType(SqlDbType sqlDbType, bool isNullable)
        {
            if (!DbConversion.ContainsKey(sqlDbType))
                throw new KeyNotFoundException(string.Format("Key {0} not exists.", sqlDbType));

            var type = DbConversion[sqlDbType];
            
            if (isNullable)
                type = GetNullableType(type);

            return type;
        }

        public static string NormalizeNetTypeName(Type type)
        {
            if (NetConversion.ContainsKey(type))
                return NetConversion[type];

            var baseType = Nullable.GetUnderlyingType(type);

            if (baseType != null)
            {
                if (NetConversion.ContainsKey(baseType))
                    return NetConversion[baseType] + "?";
            }
            
            return type.Name;
        }

        public static string NormalizeNetName(string name)
        {
            string name1 = name;
            
            if (name1.StartsWith("@"))
                name1 = name1.Substring(1, name1.Length - 1);

            name1 = name1.Substring(0, 1).ToLower() + name1.Substring(1, name1.Length - 1);

            string copy = null;
            bool isUpper = false;
            for (int i = 0; i < name1.Length; i++)
            {
                var c = name1[i];

                if (isUpper && char.IsUpper(c) && i > 0)
                {
                    copy += char.ToLower(name1[i]);
                }
                else
                {
                    if (char.IsLower(c))
                    {
                        isUpper = false;
                        copy += c;
                    }
                    else
                    {
                        isUpper = true;
                        copy += c;
                    }
                }
            }

            return copy;
        }

        public static Type GetNullableType(Type type)
        {
            // Use Nullable.GetUnderlyingType() to remove the Nullable<T> wrapper if type is already nullable.
            var uType = Nullable.GetUnderlyingType(type);
            var aType = type;
            if (uType != null)
                aType = uType;

            if (aType.IsValueType)
                return typeof(Nullable<>).MakeGenericType(aType);

            return type;
        }

        public static bool TryGetDefaultAsString(SqlParameterDefinition parameter, out string @value)
        {
            if (parameter.DefaultValue == null)
            {
                @value = null;

                return false;
            }

            if (parameter.DefaultValue.Value == null)
            {
                @value = null;

                return true;
            }

            if (!parameter.TypeDefinition.SqlType.HasValue)
            {
                @value = null;

                return false;
            }

            switch (parameter.TypeDefinition.SqlType.Value)
            {
                case SqlDbType.Bit:
                    @value = (bool)parameter.DefaultValue.Value ? "true" : "false";
                    return true;
                case SqlDbType.Char:
                case SqlDbType.NChar:
                    @value = string.Format("'{0}'", parameter.DefaultValue.Value);
                    return true;
                case SqlDbType.Text:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.VarChar:
                    @value = string.Format("\"{0}\"", parameter.DefaultValue.Value);
                    return true;
                case SqlDbType.UniqueIdentifier:
                    @value = parameter.DefaultValue.Value.ToString();
                    if (@value == "00000000-0000-0000-0000-000000000000")
                    {
                        @value = parameter.Nullable ? "new Guid?()" : "new Guid()";
                        return true;
                    }

                    return false;
                default:
                    @value = parameter.DefaultValue.Value.ToString();
                    return true;
            }
        }
    }
}