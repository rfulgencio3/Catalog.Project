using Catalog.Application.ViewModels;

namespace Catalog.Application.Services
{
    public class BaseCharacteristicService
    {
        public BaseCharacteristicService()
        {
            ResponseBase = new ResponseBase();
        }
        public ResponseBase ResponseBase { get; set; }
    }
}
