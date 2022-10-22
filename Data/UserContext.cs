using MongoDB.Bson;
using MongoDB.Driver;

namespace UserAuthBlazorApp.Data
{
	public class UserContext
	{

		public bool AddUser(BaseUser user)
		{
			try
			{
				MongoDBHelper.collection.InsertOne(user);
				return true;
			}
            catch
			{
                return false;
            }
		}

		public List<BaseUser> GetAllUsers()
		{
				
            try
            {
                var users = MongoDBHelper.collection.Find(new BsonDocument()).ToList();
                return users;
            }
            catch
            {
                return new List<BaseUser>();
            }
        }

		public bool DeleteCharacter(string id)
		{
			try
			{
				MongoDBHelper.collection.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public BaseUser? GetCharacter(string id)
		{
			try
			{
				var userFromDB = MongoDBHelper.collection.Find(x => x._id == id).FirstOrDefault();
				return userFromDB;
			}
			catch
			{
				return null;
			}
		}

		public BaseUser? Authorization(string login, string password)
		{
			try
			{
				var userFromDB = MongoDBHelper.collection.Find(x => x.Login == login).FirstOrDefault();
                if (userFromDB.Password == password)
				{
					userFromDB.IsAuthorized = true;
                    MongoDBHelper.collection.ReplaceOne(x => x._id == userFromDB._id, userFromDB);
                    return userFromDB;
                }
			}
			catch
			{ }
            return null;
        }

		public List<BaseUser> GetOnlineUsers()
		{
			try
			{
				var users = MongoDBHelper.collection.Find(x => x.IsAuthorized == true).ToList();
				return users;
            }
			catch {
                return new List<BaseUser>();
            }
		}

        public void LogOut(BaseUser baseUser)
		{
            try
            {
                baseUser.IsAuthorized = false;
                MongoDBHelper.collection.ReplaceOne(x => x._id == baseUser._id, baseUser);
            }
            catch { }
        }

		//public bool UpdateCharacter(string id, BaseUser newCharacter)
		//{
		//    var filter = Builders<BaseUser>.Filter.Eq(x => x.Id, id);
		//    //var character = ToBaseCharacter(newCharacter);
		//    character.Id = id;
		//    MongoDBHelper.collection.ReplaceOne(new BsonDocument("_id", new ObjectId(id)),
		//        character);
		//    return true;
		//}
	}
}