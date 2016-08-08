using System;
using System.Configuration;


namespace LittleBackstage.Infrastructure.Authentication
{
    public class AuthenticationSection : ConfigurationSection
    {
        [ConfigurationProperty("users")]
        [ConfigurationCollection(typeof(UserCollection))]
        public UserCollection Users
        {
            get
            {
                return (UserCollection)base["users"];
            }
        }
    }
}
