using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of the full name of a user. 
    /// </summary>
    public class FullName : TaggedString<FullName>
    {
        private FullName(string userName)
        : base(userName) { }

        public static FullName Wrap(string value)
        => new FullName(value);
    }
}
