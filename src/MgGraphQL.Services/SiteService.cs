using MgGraphQL.GraphModel.Model;
using MgGraphQL.GraphModel.Services;
using OSGeo.MapGuide;
using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.Services
{
    public class SiteService : ISiteService
    {
        public string CreateSession(CreateSessionModel model)
        {
            var conn = new MgSiteConnection();
            var userInfo = new MgUserInformation(model.Username, model.Password);
            conn.Open(userInfo);

            var site = conn.GetSite();
            return site.CreateSession();
        }
    }
}
