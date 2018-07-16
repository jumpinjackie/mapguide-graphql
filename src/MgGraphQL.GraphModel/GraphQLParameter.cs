using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgGraphQL.GraphModel
{
    public class GraphQLParameter
    {
        public string OperationName { get; set; }

        public string NamedQuery { get; set; }

        public string Query { get; set; }

        public string Variables { get; set; }
    }
}
