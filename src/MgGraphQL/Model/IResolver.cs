﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgGraphQL.Model
{
    public interface IResolver
    {
        void Resolve(GraphQLQuery graphQLQuery);
    }
}
