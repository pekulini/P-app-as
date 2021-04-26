using System;
using System.Collections.Generic;
using System.Text;

namespace pAPPas
{

    public class RootData
    {
        public string modhash { get; set; }
        public int dist { get; set; }
        public IList<Child> children { get; set; }
        public string after { get; set; }
        public object before { get; set; }
    }
}
