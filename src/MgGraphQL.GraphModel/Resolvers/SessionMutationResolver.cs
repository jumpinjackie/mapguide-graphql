using GraphQL.Types;
using MgGraphQL.GraphModel.Input;
using MgGraphQL.GraphModel.Model;
using MgGraphQL.GraphModel.Output;
using MgGraphQL.GraphModel.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel.Resolvers
{
    public class SessionMutationResolver : ResolverBase, IMutationResolver
    {
        readonly ISiteService _siteService;

        public SessionMutationResolver(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public void Resolve(GraphQLMutation mutation)
        {
            mutation.Field<ResponseGraphType<StringGraphType>>(
                "createSession",
                arguments: new QueryArguments(
                    new QueryArgument<CreateSessionInputGraphType> { Name = "options" }
                ),
                resolve: context =>
                {
                    var options = context.GetArgument<CreateSessionModel>("options");
                    var session = _siteService.CreateSession(options);
                    return Response(session);
                });
        }
    }
}
