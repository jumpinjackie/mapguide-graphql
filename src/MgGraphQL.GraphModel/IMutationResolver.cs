using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel
{
    public interface IMutationResolver
    {
        void Resolve(GraphQLMutation mutation);
    }
}
