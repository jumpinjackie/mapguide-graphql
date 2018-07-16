using GraphQL.Types;
using MgGraphQL.GraphModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel.Output.Resource
{
    public class ResourceItemGraphType : ObjectGraphType<ResourceItemModel>, IGraphQLType
    {
        public ResourceItemGraphType()
        {
            Field(i => i.Name).Description("The name of the resource");
            Field(i => i.Type).Description("The type of the resource");
        }
    }
}
