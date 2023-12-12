using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Mangifier.Api.Generator;

[Generator]
public class BaseRepositoryGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace", out var rootNamespace);

        var source = $$"""
                       // Auto-generated code

                       using Mangifier.Api.Shared.IocFactory;
                       using Microsoft.Extensions.DependencyInjection;
                       using MongoSharpen;

                       namespace {{rootNamespace}};

                       public interface IRepository
                       {
                          IAbstractFactory<IDbContext> ContextFactory { get; internal set; }
                       }

                       public abstract class RepositoryBase : IRepository
                       {
                           protected RepositoryBase(IAbstractFactory<IDbContext> contextFactory)
                           {
                               ContextFactory = contextFactory;
                           }
                       
                           public IAbstractFactory<IDbContext> ContextFactory { get; set; }
                       }

                       public interface IRepositoryFactory
                       {
                           T Create<T>() where T : IRepository;
                       }

                       public class RepositoryFactory : IRepositoryFactory
                       {
                           private readonly IServiceProvider _provider;
                       
                           public RepositoryFactory(IServiceProvider provider)
                           {
                               _provider = provider;
                           }
                       
                           public T Create<T>() where T : IRepository => _provider.GetRequiredService<T>();
                       }

                       """;
        context.AddSource("Repository.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}