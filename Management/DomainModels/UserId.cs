using Common;

namespace Management.DomainModels
{
    public class UserId : TaggedString<UserId>
    {
        private UserId(string id) : base(id) { }

        public static UserId Wrap(string value) => new UserId(value);
    }
}
