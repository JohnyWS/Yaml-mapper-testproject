using ConsoleApplication1.Configuration;
using ConsoleApplication1.Model;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Initiating project instances...");
      IAssemblyInstances assemblyInstances = InstanceProvider.CreateAssemblyInstances();

      Console.WriteLine("Loading template...");
      Template template = DeAsyncResult(() => assemblyInstances.TemplateRepository.LoadTemplate(new Uri("TestTemplate.yaml", UriKind.Relative)));

      Console.WriteLine("Parsed template (before replacements):");
      Console.WriteLine(template.HtmlTemplate);

      Console.WriteLine("Execution finished. Press any key to exit...");
      Console.ReadKey(intercept: true);
    }

    private static T DeAsyncResult<T>(Func<Task<T>> asyncMethod)
    {
      // TODO: Add better fault handling
      var task = asyncMethod.Invoke();
      try
      {
        task.Wait();
        return task.Result;
      }
      catch (AggregateException exception)
      {
        if (exception.InnerExceptions.Count == 1)
        {
          ExceptionDispatchInfo.Capture(exception.InnerException ?? exception).Throw();
          return default(T);
        }
        else
        {
          throw new Exception($"Multiple exceptions occured. First exception message was: {exception.InnerException.Message}", exception);
        }
      }
    }
  }
}
