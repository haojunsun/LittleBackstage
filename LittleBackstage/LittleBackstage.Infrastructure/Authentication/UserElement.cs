using System;
using System.Configuration;

namespace LittleBackstage.Infrastructure.Authentication
{
    public class UserElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (String)this["name"];
            }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (String)this["password"];
            }
        }
        [ConfigurationProperty("permissions", IsRequired = true)]
        public string Permissions
        {
            get
            {
                return (String)this["permissions"];
            }
        }
    }
}
