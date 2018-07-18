using GraphQL.Types;
using MgGraphQL.GraphModel.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel
{
    public static class ExtensionMethods
    {
        public static void DefineCommonOperationProperties<T>(this InputObjectGraphType<T> gt) where T : CommonOperationInputModel
        {
            gt.Field(i => i.SessionId).Description("The current MapGuide session id");
        }
    }
}
