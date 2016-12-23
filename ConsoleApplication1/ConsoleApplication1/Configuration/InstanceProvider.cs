using ConsoleApplication1.Repositories;
using System;

namespace ConsoleApplication1.Configuration
{
  public sealed class InstanceProvider : IAssemblyInstances
  {
    public static IAssemblyInstances CreateAssemblyInstances()
    {
      var lazyProvider = new Lazy<YamlTemplateRepository>();
      return new InstanceProvider(
        () => lazyProvider.Value
      );
    }

    public InstanceProvider(Func<ITemplateRepository> templateRepositoryProvider)
    {
      TemplateRepositoryProvider = templateRepositoryProvider;
    }

    private Func<ITemplateRepository> TemplateRepositoryProvider { get; }
    ITemplateRepository IAssemblyInstances.TemplateRepository => TemplateRepositoryProvider.Invoke();
  }
}
