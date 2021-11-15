using System;

namespace Management.StorageModels
{
    public class User
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string UserRole { get; set; }

        public string CreatedAt { get; set; }

        public string CountryCode { get; set; }
    }
}
