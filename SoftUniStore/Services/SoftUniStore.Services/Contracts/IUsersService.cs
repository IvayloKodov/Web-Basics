namespace SoftUniStore.Services.Contracts
{
    using Client.Api.CommonModels.BindingModels.Users;
    using SimpleHttpServer.Models;

    public interface IUsersService
    {
        string RegisterUser(RegisterUserBindingModel userModel);

        string LoginUser(LoginUserBindingModel loginModel, HttpSession session, HttpResponse response);


    }
}