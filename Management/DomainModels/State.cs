using Common;
using System;

namespace Management.DomainModels
{
    public class State: TaggedString<State>
    {
        private enum StateCode
        {
            NY,
            NJ,
        }

        private State(string stateCode) : base(stateCode)
        {
            if(!System.Enum.TryParse<StateCode>(stateCode, out var _))
            {
                throw new ArgumentException($"Invalid stateCode: {stateCode}");
            }
        }

        public static State Wrap(string stateCode) => new State(stateCode);
    }
}
