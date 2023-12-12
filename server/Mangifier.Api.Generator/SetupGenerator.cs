using System.Text;
using Mangifier.Source.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Mangifier.Api.Generator;

[Generator]
public class SetupGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() =>
            new AttributeSyntaxReceiver<RepositoryServiceAttribute>());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var rootNamespace = context.GetRootNameSpace();
        var syntaxReceiver = (AttributeSyntaxReceiver<RepositoryServiceAttribute>) context.SyntaxReceiver;
        var classDeclarations = syntaxReceiver!.Classes;

        var classes = classDeclarations.Select(c => c.GetNamespace() + "." + c.Identifier.ValueText).Distinct().ToList();
        var reg = classes.Aggregate(string.Empty, (current, repo) => current + $"\n        services.AddTransient<{repo}>();");

        var source = $$"""
                       // Auto-generated code

                       using Mangifier.Api.Shared.IocFactory;
                       using Mangifier.Api.DataAccess;
                       using Microsoft.Extensions.DependencyInjection;
                       using MongoSharpen;
                     
                       namespace {{rootNamespace}};

                       public static class RepositoriesSetup
                       {
                           public static void AddDataAccessServices(this IServiceCollection services)
                           {
                               services.AddSingleton<DbInitializer>();
                               services.AddSingleton<Func<IDbContext>>(x => () => DbFactory.Get());
                               services.AddSingleton<IAbstractFactory<IDbContext>, AbstractFactory<IDbContext>>();
                               services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
                               {{reg}}
                           }
                       }
                       """;

        context.AddSource("RepositoriesSetup.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}