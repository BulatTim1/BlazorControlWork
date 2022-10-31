using UserAuthBlazorApp.Data;

namespace UserAuthBlazorApp.Services
{
    public class MenuService
    {
        public delegate void MenuAuthHandler();
        public event MenuAuthHandler Notify;
        public void NotifyEvent()
        {
            Notify();
        }
    }
}
