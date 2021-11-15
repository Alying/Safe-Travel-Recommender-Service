using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of passport information (for visa -- may not be fully implemented)
    /// </summary>
    public class PassportId : TaggedString<FullName>
    {
        private PassportId(string fullname)
        : base(fullname) { }

        public static PassportId Wrap(string value)
        => new PassportId(value);
    }
}
