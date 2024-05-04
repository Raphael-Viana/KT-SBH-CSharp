using MongoDB.Bson.Serialization.Attributes;

namespace KTCSharpApi.Models
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Primary Key.
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }
    }
}
