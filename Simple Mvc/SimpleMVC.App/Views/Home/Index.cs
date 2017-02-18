namespace SimpleMVC.App.Views.Home
{
    using MVC.Interfaces;

    public class Index : IRenderable
    {
        public string Render()
        {
            return "<h3>NotesApp</h3>\r\n" +
                   "<a href=\"/users/all\">All Users</a>" +
                   "<span> </span>" +
                   "<a href=\"/users/register\">Register Users</a>";
        }
    }
}