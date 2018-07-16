using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgGraphQL.GraphModel
{
    public interface IResolver
    {
        void Resolve(GraphQLQuery graphQLQuery);
    }
}
