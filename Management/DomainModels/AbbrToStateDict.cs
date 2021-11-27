using System;
using System.Collections.Generic;
using System.Text;

namespace Management.DomainModels
{
    class AbbrToStateDict
    {
        public AbbrToStateDict()
        {
            abbrToStateDict = new Dictionary<string, State>()
            {
                { "CA", State.Wrap("California") },
                { "GA", State.Wrap("Georgia") },
                { "IL", State.Wrap("Illinois") },
                { "MA", State.Wrap("Massachusetts") },
                { "WA", State.Wrap("Washington") },
            };
        }

        public Dictionary<string, State> abbrToStateDict { get; }
    }
}
