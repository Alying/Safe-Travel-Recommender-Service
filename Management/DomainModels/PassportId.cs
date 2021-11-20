using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of passport information (for visa -- may not be fully implemented).
    /// </summary>
    public class PassportId : TaggedString<FullName>
    {
        private PassportId(string fullname)
        : base(fullname)
        {
        }

        /// <summary>
        /// Wrap passport id data.
        /// </summary>
        /// <param name="value">user's passport id.</param>
        /// <returns>Wrapped passport id data.</returns>
        public static PassportId Wrap(string value)
        => new PassportId(value);
    }
}
