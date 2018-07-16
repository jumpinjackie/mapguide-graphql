using GraphQL.Types;
using MgGraphQL.GraphModel.Services;

namespace MgGraphQL.GraphModel.Input
{
    public class GetResourceListInputGraphType : InputObjectGraphType<GetFolderResourcesInputModel>, IGraphQLType
    {
        public GetResourceListInputGraphType()
        {
            Field(i => i.ResourceId).Description("The resource id of the folder to get resources for");
        }
    }
}
