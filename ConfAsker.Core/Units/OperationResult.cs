using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Units
{
    public class OperationResult
    {
        public OperationResult(bool successful = true) 
        {
            Successful = successful;
        }

        public OperationResult(bool successful, string description = "")
        {
            Successful = successful;
            Description = description;
        }

        public bool Successful { get; set; }

        public string Description { get; set; }
    }
}
