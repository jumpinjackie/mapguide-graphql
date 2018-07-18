using MgGraphQL.GraphModel.Model;
using MgGraphQL.GraphModel.Services;
using OSGeo.MapGuide;
using OSGeo.MapGuide.ObjectModels.Common;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MgGraphQL.Services
{
    public class ResourceService : IResourceService
    {
        public ResourceService()
        {

        }

        static string EnsureSlash(string path)
        {
            if (!path.EndsWith("/"))
                return path + "/";
            return path;
        }

        public ResourceListModel GetFolderResources(GetFolderResourcesInputModel input)
        {
            var conn = new MgSiteConnection();
            var userInfo = new MgUserInformation(input.SessionId);
            conn.Open(userInfo);
            var resSvc = (MgResourceService)conn.CreateService(MgServiceType.ResourceService);

            var id = input.ResourceId ?? $"Library:/{EnsureSlash(input.Path ?? "/")}";

            var resId = new MgResourceIdentifier(id);
            var br = resSvc.EnumerateResources(resId, 1, string.Empty);
            using (var ms = new MgReadOnlyStream(br))
            {
                var ser = new XmlSerializer(typeof(ResourceList));
                var rl = (ResourceList)ser.Deserialize(ms);

                var model = new ResourceListModel();
                var folders = new List<ResourceFolderModel>();
                var children = new List<ResourceItemModel>();

                foreach (var it in rl.Children)
                {
                    if (it.IsFolder)
                        folders.Add(new ResourceFolderModel { Name = it.Name });
                    else
                        children.Add(new ResourceItemModel { Name = it.Name, Type = it.ResourceType });
                }

                model.Folders = folders.ToArray();
                model.Items = children.ToArray();

                return model;
            }
        }
    }
}
