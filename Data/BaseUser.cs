using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace UserAuthBlazorApp.Data
{
	public class BaseUser
	{
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }

		//[BsonIgnoreIfNull]
		[BsonRequired]
        public string Login { get; set; }
		//[BsonIgnoreIfNull]
		public string Password { get; set; }
		[BsonIgnoreIfNull]
		public string Name { get; set; }
		[BsonIgnoreIfNull]
		public string Lastname { get; set; }
        [BsonRequired]
        public string Email { get; set; }
		[BsonIgnoreIfNull]
		public bool IsAuthorized { get; set; }
	}
}
