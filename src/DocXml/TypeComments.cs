using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    public class TypeComments
    {
        public string Summary { get; set; } = "";
        public List<Tuple<string, string>> Parameters { get; set; } = new List<Tuple<string, string>>();
    }
}
