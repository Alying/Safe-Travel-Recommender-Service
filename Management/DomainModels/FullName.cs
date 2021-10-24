using Common;

namespace Management.DomainModels
{
    public class FullName: TaggedString<FullName>
    {
        private FullName(string userName)
        : base(userName) { }

        public static FullName Wrap(string value)
        => new FullName(value);
    }
}
