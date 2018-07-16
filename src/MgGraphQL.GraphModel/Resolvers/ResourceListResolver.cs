using GraphQL.Types;
using MgGraphQL.GraphModel.Input;
using MgGraphQL.GraphModel.Output;
using MgGraphQL.GraphModel.Output.Resource;
using MgGraphQL.GraphModel.Services;

namespace MgGraphQL.GraphModel.Resolvers
{
    public class ResourceListResolver : ResolverBase, IResolver
    {
        readonly IResourceService _resSvc;

        public ResourceListResolver(IResourceService resSvc)
        {
            _resSvc = resSvc;
        }

        public void Resolve(GraphQLQuery graphQLQuery)
        {
            graphQLQuery.Field<ResponseGraphType<ResourceListGraphType>>(
                "resources",
                arguments: new QueryArguments(
                    new QueryArgument<GetResourceListInputGraphType> { Name = "options", Description = "Resource listing options" }
                ),
                resolve: context =>
                {
                    var options = context.GetArgument<GetFolderResourcesInputModel>("options");
                    var result = _resSvc.GetFolderResources(options);

                    return Response(result);
                });
        }
    }
}
