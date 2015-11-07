using ConfAsker.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Interfaces
{
    public interface IQueryValidator
    {
        OperationResult ValidateQuerySyntax(string query);

        OperationResult ValidateQuerySyntax(string[] query);
    }
}
