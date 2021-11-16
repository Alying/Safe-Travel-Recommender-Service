// <copyright file="TaggedString.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Common
{
    /// <summary>
    /// Tagged string.
    /// </summary>
    /// <typeparam name="TTag">tag.</typeparam>
    public class TaggedString<TTag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedString{TTag}"/> class.
        /// </summary>
        /// <param name="value">value of tagged string.</param>
        public TaggedString(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets value of tagged string.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Unwraps value. of tagged string.
        /// </summary>
        /// <param name="tStr">tagged string.</param>
        /// <returns>tagged string value.</returns>
        public static string UnWrap(TaggedString<TTag> tStr)
            => tStr.Value;
    }
}
