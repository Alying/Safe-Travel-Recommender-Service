using System;

namespace Common
{
    public class TaggedString<TTag>
    {
        public string Value { get; set; }

        public TaggedString(string value)
        {
            Value = value;
        }

        public static string UnWrap(TaggedString<TTag> tStr)
            => tStr.Value;
    }
}
