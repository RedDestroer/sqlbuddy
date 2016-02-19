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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="DataAccessParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
[System.CLSCompliant(false)]
public interface IDataAccessVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifier([NotNull] DataAccessParser.IdentifierContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.dataaccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataaccess([NotNull] DataAccessParser.DataaccessContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.dictionary_initializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDictionary_initializer([NotNull] DataAccessParser.Dictionary_initializerContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.dictionary_pair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDictionary_pair([NotNull] DataAccessParser.Dictionary_pairContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.boolean_literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolean_literal([NotNull] DataAccessParser.Boolean_literalContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>generator</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGenerator([NotNull] DataAccessParser.GeneratorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>className</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassName([NotNull] DataAccessParser.ClassNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] DataAccessParser.LiteralContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.literal_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral_list([NotNull] DataAccessParser.Literal_listContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>transformators</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTransformators([NotNull] DataAccessParser.TransformatorsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.literal_list_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral_list_items([NotNull] DataAccessParser.Literal_list_itemsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompileUnit([NotNull] DataAccessParser.CompileUnitContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.string_list_items"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString_list_items([NotNull] DataAccessParser.String_list_itemsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.property"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProperty([NotNull] DataAccessParser.PropertyContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>connection</c>
	/// labeled alternative in <see cref="DataAccessParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConnection([NotNull] DataAccessParser.ConnectionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.string_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString_list([NotNull] DataAccessParser.String_listContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.keyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyword([NotNull] DataAccessParser.KeywordContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.dictionary_pairs"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDictionary_pairs([NotNull] DataAccessParser.Dictionary_pairsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DataAccessParser.properties"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProperties([NotNull] DataAccessParser.PropertiesContext context);
}
} // namespace SqlBuddy.Parsers
