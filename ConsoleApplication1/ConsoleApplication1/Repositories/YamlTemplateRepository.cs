using ConsoleApplication1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ConsoleApplication1.Repositories
{
  internal class YamlTemplateRepository : ITemplateRepository
  {
    async Task<Template> ITemplateRepository.LoadTemplate(Uri path)
    {
      using (var sourceStream = new FileStream(path.OriginalString, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
      {
        var stringBuilder = new StringBuilder();

        var buffer = new byte[0x1000];
        int numRead;
        while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
          string text = Encoding.UTF8.GetString(buffer, 0, numRead);
          stringBuilder.Append(text);
        }

        var deserializer = new DeserializerBuilder().Build();

        return deserializer.Deserialize<DeserializeableTemplate>(stringBuilder.ToString());
      }
    }

    private class DeserializeableTemplate : Template
    {
      [YamlMember(Alias = "HtmlTemplate")]
      public string HtmlTemplateWrapper
      {
        get { return HtmlTemplate; }
        set { HtmlTemplate = value; }
      }

      [YamlMember(Alias = "Keys")]
      public List<string> KeysWrapper
      {
        get { return (List<string>)Keys; }
        set { Keys = value; }
      }

      [YamlMember(Alias = "DefaultValues")]
      public Dictionary<string, string> DefaultValuesWrapper
      {
        get { return (Dictionary<string, string>)DefaultValues; }
        set { DefaultValues = value; }
      }
    }
  }
}
