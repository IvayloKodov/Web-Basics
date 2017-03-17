namespace CarDealer.Services
{
    using System.Linq;
    using Data.Repositories;
    using Models;
    using Models.BindingModels;

    public class UsersService
    {
        private readonly IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public void RegisterUser(RegisterUserBindingModel userBm)
        {
            var user = new User()
            {
                Email = userBm.Email,
                Password = userBm.Password,
                Username = userBm.Username,
            };

            this.users.Add(user);
            this.users.SaveChanges();
        }

        public User LogInUser(LoginUserBm loginBm)
        {
            var userDb = this.users
                .All()
                .FirstOrDefault(u => u.Username == loginBm.Username &&
                                     u.Password == loginBm.Password);

            return userDb;
        }
    }
}