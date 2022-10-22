using MongoDB.Driver;

namespace UserAuthBlazorApp.Data
{
	public static class MongoDBHelper
	{
		private static readonly MongoClient client = new MongoClient("mongodb://localhost");
		internal static readonly IMongoDatabase db = client.GetDatabase("UserAuthBlazorApp");
		internal static readonly IMongoCollection<BaseUser> collection = db.GetCollection<BaseUser>("Users");
	}
}