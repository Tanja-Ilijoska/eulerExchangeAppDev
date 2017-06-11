using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models
{
    public class ModelList
    {
        public List<IList> data { get; set; }
        public Dictionary<string, int> names;
        private int index;

        public ModelList()
        {
            data = new List<IList>();
            names = new Dictionary<string, int>();
        }

        public void add(IList list, string name)
        {
            data.Add(list);
            names.Add(name, index++);
        }
    }
}