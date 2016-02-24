//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\git\inet\sqlbuddy\SqlBuddy\SqlBuddy\Parsers\DataAccess.g4 by ANTLR 4.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace SqlBuddy.Parsers {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="DataAccessParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
[System.CLSCompliant(false)]
public interface IDataAccessListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.dataaccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDataaccess([NotNull] DataAccessParser.DataaccessContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.dataaccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDataaccess([NotNull] DataAccessParser.DataaccessContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.dictionary_initializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDictionary_initializer([NotNull] DataAccessParser.Dictionary_initializerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.dictionary_initializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDictionary_initializer([NotNull] DataAccessParser.Dictionary_initializerContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.dictionary_pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDictionary_pair([NotNull] DataAccessParser.Dictionary_pairContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.dictionary_pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDictionary_pair([NotNull] DataAccessParser.Dictionary_pairContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.boolean_literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoolean_literal([NotNull] DataAccessParser.Boolean_literalContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.boolean_literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoolean_literal([NotNull] DataAccessParser.Boolean_literalContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>generator</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGenerator([NotNull] DataAccessParser.GeneratorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>generator</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGenerator([NotNull] DataAccessParser.GeneratorContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>className</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassName([NotNull] DataAccessParser.ClassNameContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>className</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassName([NotNull] DataAccessParser.ClassNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.partial_dictionary_pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPartial_dictionary_pair([NotNull] DataAccessParser.Partial_dictionary_pairContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.partial_dictionary_pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPartial_dictionary_pair([NotNull] DataAccessParser.Partial_dictionary_pairContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] DataAccessParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] DataAccessParser.LiteralContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>transformators</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTransformators([NotNull] DataAccessParser.TransformatorsContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>transformators</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTransformators([NotNull] DataAccessParser.TransformatorsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.literal_list_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral_list_items([NotNull] DataAccessParser.Literal_list_itemsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.literal_list_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral_list_items([NotNull] DataAccessParser.Literal_list_itemsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.string_list_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString_list_items([NotNull] DataAccessParser.String_list_itemsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.string_list_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString_list_items([NotNull] DataAccessParser.String_list_itemsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.property"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProperty([NotNull] DataAccessParser.PropertyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.property"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProperty([NotNull] DataAccessParser.PropertyContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>connection</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConnection([NotNull] DataAccessParser.ConnectionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>connection</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConnection([NotNull] DataAccessParser.ConnectionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.keyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterKeyword([NotNull] DataAccessParser.KeywordContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.keyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitKeyword([NotNull] DataAccessParser.KeywordContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.dictionary_pairs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDictionary_pairs([NotNull] DataAccessParser.Dictionary_pairsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.dictionary_pairs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDictionary_pairs([NotNull] DataAccessParser.Dictionary_pairsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifier([NotNull] DataAccessParser.IdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifier([NotNull] DataAccessParser.IdentifierContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.partial_dictionary_initializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPartial_dictionary_initializer([NotNull] DataAccessParser.Partial_dictionary_initializerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.partial_dictionary_initializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPartial_dictionary_initializer([NotNull] DataAccessParser.Partial_dictionary_initializerContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.schema_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSchema_items([NotNull] DataAccessParser.Schema_itemsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.schema_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSchema_items([NotNull] DataAccessParser.Schema_itemsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.partial_dictionary_pairs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPartial_dictionary_pairs([NotNull] DataAccessParser.Partial_dictionary_pairsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.partial_dictionary_pairs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPartial_dictionary_pairs([NotNull] DataAccessParser.Partial_dictionary_pairsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.literal_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral_list([NotNull] DataAccessParser.Literal_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.literal_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral_list([NotNull] DataAccessParser.Literal_listContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.string_list_items2"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString_list_items2([NotNull] DataAccessParser.String_list_items2Context context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.string_list_items2"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString_list_items2([NotNull] DataAccessParser.String_list_items2Context context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.schema_item"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSchema_item([NotNull] DataAccessParser.Schema_itemContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.schema_item"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSchema_item([NotNull] DataAccessParser.Schema_itemContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.string_list2"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString_list2([NotNull] DataAccessParser.String_list2Context context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.string_list2"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString_list2([NotNull] DataAccessParser.String_list2Context context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompileUnit([NotNull] DataAccessParser.CompileUnitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompileUnit([NotNull] DataAccessParser.CompileUnitContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>schemas</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSchemas([NotNull] DataAccessParser.SchemasContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>schemas</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSchemas([NotNull] DataAccessParser.SchemasContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.string_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString_list([NotNull] DataAccessParser.String_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.string_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString_list([NotNull] DataAccessParser.String_listContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.schema_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSchema_list([NotNull] DataAccessParser.Schema_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.schema_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSchema_list([NotNull] DataAccessParser.Schema_listContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DataAccessParser.properties"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProperties([NotNull] DataAccessParser.PropertiesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DataAccessParser.properties"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProperties([NotNull] DataAccessParser.PropertiesContext context);
}
} // namespace SqlBuddy.Parsers
