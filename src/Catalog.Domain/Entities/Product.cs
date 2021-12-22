using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.Domain.Entities
{
    public class Product : Base
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
