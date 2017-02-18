namespace SimpleMVC.App.Views.Users
{
    using System.Collections.Generic;
    using System.Text;
    using MVC.Interfaces;
    using MVC.Interfaces.Generic;
    using ViewModels;
    // Changed allusernamesviewmodel => List<userprofileviewmodel>
    public class All : IRenderable<List<UserProfileViewModel>>
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<a href=\"/home/index\">&lt;Home</a>");
            sb.AppendLine("<h3> All users</h3>");
            sb.AppendLine("<ul>");

            foreach (var user in this.Model)
            {//added a href="..."
                sb.AppendLine($"<li><a href=\"/users/profile?id={user.UserId}\">{user.Username}</a></li>");
            }

            sb.AppendLine("</ul>");

            return sb.ToString();
        }
        // Changed allusernamesviewmodel => List<userprofileviewmodel>
        public List<UserProfileViewModel> Model { get; set; }
    }
}