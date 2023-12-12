using System.Text;
using Mangifier.Source.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Mangifier.Api.Generator;

[Generator]
public class RepositoryGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() =>
            new AttributeSyntaxReceiver<RepositoryServiceAttribute>());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var syntaxReceiver = (AttributeSyntaxReceiver<RepositoryServiceAttribute>) context.SyntaxReceiver;
        foreach (var repo in syntaxReceiver!.Classes)
        {
            var source = $$"""
                           // Auto-generated code

                           using Mangifier.Api.Shared.IocFactory;
                           using MongoSharpen;

                           namespace {{repo.GetNamespace()}};

                           public sealed partial class {{repo.Identifier.ValueText}} : RepositoryBase
                           {
                              public {{repo.Identifier.ValueText}}(IAbstractFactory<IDbContext> contextFactory) : base(contextFactory)
                              {
                              }
                           }
                           """;
            var id = Guid.NewGuid().ToString().Replace("-", "");
            context.AddSource($"{id}.{repo.Identifier.ValueText}.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}