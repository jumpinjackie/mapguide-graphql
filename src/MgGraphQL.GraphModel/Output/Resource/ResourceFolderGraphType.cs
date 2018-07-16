using GraphQL.Types;
using MgGraphQL.GraphModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel.Output.Resource
{
    public class ResourceFolderGraphType : ObjectGraphType<ResourceFolderModel>, IGraphQLType
    {
        public ResourceFolderGraphType()
        {
            Field(f => f.Name).Description("The name of the folder");
        }
    }
}
