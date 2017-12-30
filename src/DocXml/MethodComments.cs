using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    public class MethodComments
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public List<Tuple<string,string>> Parameters { get; set; } = new List<Tuple<string,string>>();
        public string Returns { get; set; }
        public List<Tuple<string, string>> Responses { get; set; } = new List<Tuple<string, string>>();
    }
}
