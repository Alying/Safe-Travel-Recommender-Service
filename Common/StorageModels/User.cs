using System;

namespace Common.StorageModels
{
    public class User
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string CountryCode { get; set; }

        public string CreatedAt { get; set; }

        public string PassportId { get; set; }
    }
}
