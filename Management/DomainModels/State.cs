using System;
using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a state for the location.
    /// Currently, we only deal within the United States.
    /// </summary>
    public class State : TaggedString<State>
    {
        private State(string stateCode)
                : base(stateCode)
        {
        }

        public static State Wrap(string stateCode) => new State(stateCode);
    }
}
