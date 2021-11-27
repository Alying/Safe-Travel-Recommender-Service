using System.Collections.Generic;

namespace Management.DomainModels
{
    /// <summary>
    /// Static map that maps abbreviated states to full name states
    /// </summary>
    public class AbbrToStateDict
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbbrToStateDict"/> class.
        /// </summary>
        public AbbrToStateDict()
        {
            abbrToStateDict = new Dictionary<string, State>()
            {
                { "AK", State.Wrap("Alaska") },
                { "AL", State.Wrap("Alabama") },
                { "AR", State.Wrap("Arkansas") },
                { "AZ", State.Wrap("Arizona") },
                { "CA", State.Wrap("California") },
                { "CO", State.Wrap("Colorado") },
                { "CT", State.Wrap("Connecticut") },
                { "DC", State.Wrap("Washington, D.C.") },
                { "DE", State.Wrap("Delaware") },
                { "FL", State.Wrap("Florida") },
                { "GA", State.Wrap("Georgia") },
                { "HI", State.Wrap("Hawaii") },
                { "IA", State.Wrap("Iowa") },
                { "ID", State.Wrap("Idaho") },
                { "IL", State.Wrap("Illinois") },
                { "IN", State.Wrap("Indiana") },
                { "KS", State.Wrap("Kansas") },
                { "KY", State.Wrap("Kentucky") },
                { "LA", State.Wrap("Louisiana") },
                { "MA", State.Wrap("Massachusetts") },
                { "MD", State.Wrap("Maryland") },
                { "ME", State.Wrap("Maine") },
                { "MI", State.Wrap("Michigan") },
                { "MN", State.Wrap("Minnesota") },
                { "MO", State.Wrap("Missouri") },
                { "MS", State.Wrap("Mississippi") },
                { "MT", State.Wrap("Montana") },
                { "NC", State.Wrap("North Carolina") },
                { "ND", State.Wrap("North Dakota") },
                { "NE", State.Wrap("Nebraska") },
                { "NH", State.Wrap("New Hampshire") },
                { "NJ", State.Wrap("New Jersey") },
                { "NM", State.Wrap("New Mexico") },
                { "NV", State.Wrap("Nevada") },
                { "NY", State.Wrap("New York") },
                { "OH", State.Wrap("Ohio") },
                { "OK", State.Wrap("Oklahoma") },
                { "OR", State.Wrap("Oregon") },
                { "PA", State.Wrap("Pennsylvania") },
                { "RI", State.Wrap("Rhode Island") },
                { "SC", State.Wrap("South Carolina") },
                { "SD", State.Wrap("South Dakota") },
                { "TN", State.Wrap("Tennessee") },
                { "TX", State.Wrap("Texas") },
                { "UT", State.Wrap("Utah") },
                { "VA", State.Wrap("Virginia") },
                { "VT", State.Wrap("Vermont") },
                { "WA", State.Wrap("Washington") },
                { "WI", State.Wrap("Wisconsin") },
                { "WV", State.Wrap("West Virginia") },
                { "WY", State.Wrap("Wyoming") },
            };
        }

        /// <summary>
        /// Gets states' full names using static map/dictionary
        /// </summary>
        public Dictionary<string, State> abbrToStateDict { get; }
    }
}
