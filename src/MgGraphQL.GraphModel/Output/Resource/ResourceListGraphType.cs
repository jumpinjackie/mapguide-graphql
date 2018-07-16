using GraphQL.Types;
using MgGraphQL.GraphModel.Model;

namespace MgGraphQL.GraphModel.Output.Resource
{
    public class ResourceListGraphType : ObjectGraphType<ResourceListModel>, IGraphQLType
    {
        public ResourceListGraphType()
        {
            Field(lst => lst.Folders, type: typeof(ListGraphType<ResourceFolderGraphType>)).Description("Child folders");
            Field(lst => lst.Items, type: typeof(ListGraphType<ResourceItemGraphType>)).Description("Child resources");
        }
    }
}
