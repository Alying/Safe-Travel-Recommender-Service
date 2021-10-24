using Common;

namespace Management.DomainModels
{
    public class PassportId: TaggedString<FullName>
    {
        private PassportId(string fullname)
        : base(fullname) { }

        public static PassportId Wrap(string value)
        => new PassportId(value);
    }
}
