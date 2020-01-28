using System;

namespace OpenChat.Application.Users
{
    public class RegisteredUserApiModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
    }
}