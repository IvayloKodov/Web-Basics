namespace SoftUniStore.Client.Api.CommonModels.BindingModels.Users
{
    public class RegisterUserBindingModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}