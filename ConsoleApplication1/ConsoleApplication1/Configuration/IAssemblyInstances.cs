using ConsoleApplication1.Repositories;

namespace ConsoleApplication1.Configuration
{
  public interface IAssemblyInstances
  {
    ITemplateRepository TemplateRepository { get; }
  }
}
