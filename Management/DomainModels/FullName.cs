using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of the full name of a user.
    /// </summary>
    public class FullName : TaggedString<FullName>
    {
        private FullName(string userName)
                : base(userName)
        {
        }

        /// <summary>
        /// Wrape the user's full name.
        /// </summary>
        /// <param name="value">user's full name.</param>
        /// <returns>The wrapped user's full name.</returns>
        public static FullName Wrap(string value)
        => new FullName(value);
    }
}
