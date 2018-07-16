using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgGraphQL.GraphModel
{
    public class GraphQLQuery : ObjectGraphType
    {
        public GraphQLQuery(IEnumerable<IResolver> resolvers)
        {
            foreach (var resolver in resolvers)
            {
                resolver.Resolve(this);
            }
        }

        /*
        public GraphQLQuery(IServiceProvider serviceProvider)
        {
            var type = typeof(IResolver);
            var resolversTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var resolverType in resolversTypes)
            {
                var resolverTypeInterface = resolverType.GetInterfaces().Where(x => x != type).FirstOrDefault();
                if (resolverTypeInterface != null)
                {
                    var resolver = serviceProvider.GetService(resolverTypeInterface) as IResolver;
                    resolver.Resolve(this);
                }
            }
        }
        */
    }
}
