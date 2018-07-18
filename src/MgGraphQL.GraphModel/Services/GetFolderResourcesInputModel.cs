using System;
using System.Collections.Generic;
using System.Text;

namespace MgGraphQL.GraphModel.Services
{
    public class GetFolderResourcesInputModel : CommonOperationInputModel
    {
        public string ResourceId { get; set; }

        public string Path { get; set; }
    }
}
