using System.Text;
using Humanizer;
using Mangifier.Source.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Mangifier.Api.Generator;

[Generator]
public class DocumentNameGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() =>
            new CollectionSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var rootNamespace = context.GetRootNameSpace();
        var syntaxReceiver = (CollectionSyntaxReceiver) context.SyntaxReceiver;
        var classDeclarations = syntaxReceiver!.Classes;

        var collections = new List<string>();
        foreach (var syntax in classDeclarations)
        {
            var attribute = syntax.AttributeLists.SelectMany(sm => sm.Attributes).First(x =>
                x.Name.ToString().EnsureEndsWith("Attribute").Equals("CollectionAttribute"));

            var collectionName = attribute.ArgumentList!.Arguments.First().GetLastToken().ValueText;
            collections.Add(collectionName);
        }

        var props = string.Empty;

        foreach (var name in collections.OrderBy(s => s))
            if (string.IsNullOrEmpty(props))
                props += $"public static string {name.Humanize().Dehumanize()} => \"{name}\";";
            else
                props += $"\n    public static string {name.Humanize().Dehumanize()} => \"{name}\";";

        var source = $$"""
                       // Auto-generated code

                       namespace {{rootNamespace}};

                       public static class Collection
                       {
                           {{props}}
                       }
                       """;

        context.AddSource("Collection.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}

public class CollectionSyntaxReceiver : ISyntaxReceiver
{
    public IList<ClassDeclarationSyntax> Classes { get; } = new List<ClassDeclarationSyntax>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax { AttributeLists.Count: > 0 } classDeclarationSyntax &&
            classDeclarationSyntax.AttributeLists
                .Any(al => al.Attributes
                    .Any(a => a.Name.ToString().EnsureEndsWith("Attribute").Equals("CollectionAttribute"))))
            Classes.Add(classDeclarationSyntax);
    }
}