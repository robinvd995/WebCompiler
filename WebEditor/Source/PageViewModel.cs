using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEditor
{
    public class PageViewModel
    {
        public string Name { get; set; }

        public List<ReferenceViewModel> References { get; set; }

        public static PageViewModel FromJson(string filepath)
        {
            string content = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<PageViewModel>(content);
        }
    }

    public class ReferenceViewModel
    {
        public string Identifier { get; set; }

        public List<SectionViewModel> Sections { get; set; }
    }

    public class SectionViewModel
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
