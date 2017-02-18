namespace SimpleMVC.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using BindingModels;
    using Data;
    using Models;
    using MVC.Attributes.Methods;
    using MVC.Controllers;
    using MVC.Interfaces;
    using MVC.Interfaces.Generic;
    using ViewModels;

    public class UsersController : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };

            using (var context = new NotesAppContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return this.View();
        }

        //[HttpGet]
        //public IActionResult<AllUsernamesViewModel> All()
        //{
        //    List<string> usernames = new List<string>();
        //    using (var context = new NotesAppContext())
        //    {
        //        usernames = context.Users.Select(u => u.Username).ToList();
        //    }

        //    var viewModel = new AllUsernamesViewModel()
        //    {
        //        Usernames = usernames
        //    };

        //    return this.View(viewModel);
        //}


            //try
        [HttpGet]
        public IActionResult<List<UserProfileViewModel>> All()
        {
            var usernamesAndids = new List<UserProfileViewModel>();
            using (var context = new NotesAppContext())
            {
                usernamesAndids = context.Users
                    .Select(u => new UserProfileViewModel()
                    {
                        UserId = u.Id,
                        Username = u.Username,
                        Notes = u.Notes.Select(n => new NoteViewModel()
                        {
                            Content = n.Content,
                            Title = n.Title
                        })
                    }).ToList();
            }

            return this.View(usernamesAndids);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            using (var context = new NotesAppContext())
            {
                var user = context.Users.Find(id);
                var viewModel = new UserProfileViewModel()
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Notes = user.Notes
                                .Select(n => new NoteViewModel()
                                {
                                    Title = n.Title,
                                    Content = n.Content
                                })
                };

                return this.View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            using (var context = new NotesAppContext())
            {
                var user = context.Users.Find(model.UserId);

                var note = new Note()
                {
                    Title = model.Title,
                    Content = model.Content,
                };

                user.Notes.Add(note);
                context.SaveChanges();
            }

            return this.Profile(model.UserId);
        }
    }
}