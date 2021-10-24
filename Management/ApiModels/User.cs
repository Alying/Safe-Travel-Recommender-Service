using System;

namespace Management.ApiModels
{
    public class User
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string PassportId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string CountryCode { get; set; }
    }
}
