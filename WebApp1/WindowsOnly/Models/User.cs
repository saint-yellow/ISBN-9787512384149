using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WindowsOnly.Models
{
    public class User : IIdentity
    {
        public User(string username, string password, string[] roles, List<string> validIpAddresses)
        {
            Name = username;
            Password = password;
            Roles = roles;
            ValidIpAddress = validIpAddresses;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; }

        public List<string> ValidIpAddress { get; set; }

        public string AuthenticationType { get { return "Basic"; } }

        public bool IsAuthenticated { get { return true; } }
    }

    public static class AuthenticatedUsers
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User("saint-yellow", "saint-yellow", null, new List<string>{ "::1" })
        };
    }
}