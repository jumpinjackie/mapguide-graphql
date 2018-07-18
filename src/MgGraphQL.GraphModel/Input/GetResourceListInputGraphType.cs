using GraphQL.Types;
using MgGraphQL.GraphModel.Services;

namespace MgGraphQL.GraphModel.Input
{
    public class GetResourceListInputGraphType : InputObjectGraphType<GetFolderResourcesInputModel>, IGraphQLType
    {
        public GetResourceListInputGraphType()
        {
            this.DefineCommonOperationProperties();
            Field(i => i.ResourceId, nullable: true).Description("The resource id of the folder to get resources for");
            Field(i => i.Path, nullable: true).Description("The path of the folder to get resources for. This is an alternative to ResourceId");
        }
    }
}
