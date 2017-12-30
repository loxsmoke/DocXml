using System;
using System.Collections.Generic;
using System.Text;

namespace LoxSmoke.DocXml
{
    public class EnumComments
    {
        public string Summary { get; set; }
        public List<Tuple<string,string>> ValueComments { get; set; } = new List<Tuple<string, string>>();
    }
}
