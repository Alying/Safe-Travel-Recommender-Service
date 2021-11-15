using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a user's unique id.
    /// </summary>
    public class UserId : TaggedString<UserId>
    {
        private UserId(string id) : base(id) { }

        public static UserId Wrap(string value) => new UserId(value);
    }
}
