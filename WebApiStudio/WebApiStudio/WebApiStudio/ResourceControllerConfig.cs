using System.ComponentModel.Composition;
using Eze.WebApiEx;

namespace WebApiStudio
{
    [Export( typeof( IResourceControllerConfiguration ) )]
    public class ResourceControllerConfiguration : IResourceControllerConfiguration
    {
        [Export( typeof( IDocumentStore<> ) ), PartCreationPolicy( CreationPolicy.Shared )]
        public class DefaultDocumentStore<T> : InMemoryDocumentStore<T> where T : class, IResource, new()
        {
        }

        public void Configure( IResourceControllerHelper controller )
        {
            controller.EnableAll();
        }
    }
}
