using Catalog.Application.ViewModels;

namespace Catalog.Application.Services
{
    public class BaseCatalogService
    {
        public BaseCatalogService()
        {
            ResponseBase = new ResponseBase();
        }

        public ResponseBase ResponseBase { get; set; }
    }
}
