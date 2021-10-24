using System;

namespace Management.StorageModels
{
    public class User
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserRole { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string AddressId { get; set; }
    }
}
