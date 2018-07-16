using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel.Model
{
    public class ResourceListModel
    {
        public ResourceFolderModel[] Folders { get; set; }

        public ResourceItemModel[] Items { get; set; }
    }

    public class ResourceItemModel
    {
        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class ResourceFolderModel
    {
        public string Name { get; set; }
    }
}
