namespace MgGraphQL.GraphModel.Errors
{
    public class AccessDeniedError : GraphQLError
    {
        public AccessDeniedError() : base(nameof(AccessDeniedError), $"Access denied error. Are you sure that you have access to specific resource?")
        {
        }
    }
}
