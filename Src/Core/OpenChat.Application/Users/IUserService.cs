using System.Collections.Generic;

namespace OpenChat.Application.Users
{
    public interface IUserService
    {
        UserApiModel CreateUser(RegistrationInputModel registrationData);
        UserApiModel Login(UserCredentials userCredentials);
        IEnumerable<UserApiModel> AllUsers();
    }
}