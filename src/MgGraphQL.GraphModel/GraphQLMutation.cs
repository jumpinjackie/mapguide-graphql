using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel
{
    public class GraphQLMutation : ObjectGraphType
    {
        public GraphQLMutation(IEnumerable<IMutationResolver> resolvers)
        {
            foreach (var resolver in resolvers)
            {
                resolver.Resolve(this);
            }
        }
    }
}
