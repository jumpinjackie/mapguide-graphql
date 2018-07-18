using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgGraphQL.GraphModel
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (GraphQLQuery)resolveType(typeof(GraphQLQuery));
            Mutation = (GraphQLMutation)resolveType(typeof(GraphQLMutation));
        }
    }
}
