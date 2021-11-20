using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a user's unique id.
    /// </summary>
    public class UserId : TaggedString<UserId>
    {
        private UserId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Wrap user id data.
        /// </summary>
        /// <param name="value">user id.</param>
        /// <returns>Wrapped user id data.</returns>
        public static UserId Wrap(string value) => new UserId(value);
    }
}
