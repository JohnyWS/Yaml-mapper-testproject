using ConsoleApplication1.Model;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication1.Repositories
{
  public interface ITemplateRepository
  {
    Task<Template> LoadTemplate(Uri path);
  }
}
