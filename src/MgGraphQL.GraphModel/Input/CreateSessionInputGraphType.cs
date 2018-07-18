using GraphQL.Types;
using MgGraphQL.GraphModel.Model;

namespace MgGraphQL.GraphModel.Input
{
    public class CreateSessionInputGraphType : InputObjectGraphType<CreateSessionModel>, IGraphQLType
    {
        public CreateSessionInputGraphType()
        {
            Field(c => c.Username).Description("Username");
            Field(c => c.Password).Description("Password");
        }
    }
}
