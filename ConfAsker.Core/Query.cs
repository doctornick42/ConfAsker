using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core
{
    public class Query
    {
        public ECommand Command { get; set; }

        public List<string> Paths { get; set; }

        public string KeyValue { get; set; }

        public string ConnectionString { get; set; }

        public string Section { get; set; }

        public string Expected { get; set; }
    }

    public enum ECommand
    {
        get,
        check
    }
}
