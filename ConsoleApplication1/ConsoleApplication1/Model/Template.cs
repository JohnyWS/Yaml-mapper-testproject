using System.Collections.Generic;

namespace ConsoleApplication1.Model
{
  public class Template
  {
    public string HtmlTemplate { get; internal set; }
    public IReadOnlyList<string> Keys { get; internal set; }
    public IReadOnlyDictionary<string, string> DefaultValues { get; internal set; }
  }
}
