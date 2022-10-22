using UserAuthBlazorApp.Data;

namespace UserAuthBlazorApp.Services
{
	public class UsersService
    {
        static UserContext _context = new UserContext();
        public bool IsAuthorizated 
        {
            get; 
            set;
        }
        public BaseUser? CurrentUser { get; set; }
        public List<BaseUser>? GetAllOnlineUsers()
        {
            if (IsAuthorizated)
            {
                return _context.GetOnlineUsers();
            }
            else
            {
                return null;
            }
        }

        public bool Authorization(string login, string password)
        {
            var res = _context.Authorization(login, password);
            if (res != null)
            {
                CurrentUser = res;
                IsAuthorizated = true;
                return true;
            }
            return false;
        }
        
        public bool Registration(string login, string password, 
            string email, string name, string lastname)
        {
            var newUser = new BaseUser
            {
                Login = login,
                Password = password,
                Email = email,
                Name = name,
                Lastname = lastname,
                IsAuthorized = true
            };
            try
            {
                if (_context.AddUser(newUser))
                {
                    CurrentUser = newUser;
                    IsAuthorizated = true;
                    return true;
                }
            } 
            catch { }
            return false;
        }
        
        public void LogOut()
        {
            _context.LogOut(CurrentUser);
            CurrentUser = null;
            IsAuthorizated = false;
        }
    }
}
