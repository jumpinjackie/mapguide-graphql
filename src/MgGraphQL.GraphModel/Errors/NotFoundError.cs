namespace MgGraphQL.GraphModel.Errors
{
    public class NotFoundError : GraphQLError
    {
        public NotFoundError(string id) : base(nameof(NotFoundError), $"Resource '{id}' not found.")
        {
        }
    }
}
