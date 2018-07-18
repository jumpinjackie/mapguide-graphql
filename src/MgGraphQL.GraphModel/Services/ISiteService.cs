using MgGraphQL.GraphModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel.Services
{
    public interface ISiteService
    {
        string CreateSession(CreateSessionModel model);
    }
}
