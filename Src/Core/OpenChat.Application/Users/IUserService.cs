using System.Collections.Generic;

namespace OpenChat.Application.Users
{
    public interface IUserService
    {
        UserApiModel CreateUser(RegistrationInputModel registrationData);
        IEnumerable<UserApiModel> AllUsers();
    }
}