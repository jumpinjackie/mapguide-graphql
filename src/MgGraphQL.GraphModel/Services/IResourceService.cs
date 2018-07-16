using MgGraphQL.GraphModel.Model;

namespace MgGraphQL.GraphModel.Services
{
    public interface IResourceService
    {
        ResourceListModel GetFolderResources(GetFolderResourcesInputModel input);
    }
}
