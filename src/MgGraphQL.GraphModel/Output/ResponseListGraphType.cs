using GraphQL.Types;

namespace MgGraphQL.GraphModel.Output
{
    public class ResponseListGraphType<TGraphType> : ObjectGraphType<Response> where TGraphType : GraphType
    {
        public ResponseListGraphType()
        {
            Name = $"ResponseList{typeof(TGraphType).Name}";

            Field(x => x.StatusCode, nullable: true).Description("Status code of the request.");
            Field(x => x.ErrorMessage, nullable: true).Description("Error message if requests fails.");

            Field<ListGraphType<TGraphType>>(
                "data",
                "Project data returned by query.",
                resolve: context => context.Source.Data
            );
        }
    }
}
