using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.Domain.Entities
{
    public class Base
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}