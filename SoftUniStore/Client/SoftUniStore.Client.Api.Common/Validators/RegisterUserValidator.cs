namespace SoftUniStore.Client.Api.Common.Validators
{
    using System.Linq;
    using CommonModels.BindingModels.Users;

    public class RegisterUserValidator
    {
        public static bool IsValid(RegisterUserBindingModel user)
        {
            if (string.IsNullOrEmpty(user.FullName))
            {
                return false;
            }

            if (!user.Email.Contains("@"))
            {
                return false;
            }

            var containsUppercase = user.Password.Any(char.IsUpper);
            var containsLowercase = user.Password.Any(char.IsLower);
            var containsDigit = user.Password.Any(char.IsDigit);

            if (!containsUppercase || !containsLowercase || !containsDigit)
            {
                return false;
            }

            if (user.Password != user.ConfirmPassword)
            {
                return false;
            }

            return true;
        }
    }
}