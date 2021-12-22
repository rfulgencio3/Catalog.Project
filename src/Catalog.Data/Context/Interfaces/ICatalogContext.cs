using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Data.Context
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
