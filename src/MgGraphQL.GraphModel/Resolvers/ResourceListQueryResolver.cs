using GraphQL.Types;
using MgGraphQL.GraphModel.Input;
using MgGraphQL.GraphModel.Output;
using MgGraphQL.GraphModel.Output.Resource;
using MgGraphQL.GraphModel.Services;

namespace MgGraphQL.GraphModel.Resolvers
{
    public class ResourceListQueryResolver : ResolverBase, IQueryResolver
    {
        readonly IResourceService _resSvc;

        public ResourceListQueryResolver(IResourceService resSvc)
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
                    if (string.IsNullOrEmpty(options.SessionId))
                        return AccessDeniedError();

                    var result = _resSvc.GetFolderResources(options);
                    return Response(result);
                });
        }
    }
}
