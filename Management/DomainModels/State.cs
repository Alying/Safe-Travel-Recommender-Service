using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.DomainModels
{
    public class State: TaggedString<State>
    {
        private enum StateCode
        {
            UNKNOWN = 0,
            NY = 1,
            NJ = 2,
        }

        private State(string stateCode) : base(stateCode)
        {
            if(!System.Enum.TryParse<StateCode>(stateCode, out var _))
            {
                throw new ArgumentException("Invalid stateCode {0}", stateCode);
            }
        }

        public static State Wrap(string stateCode) => new State(stateCode);
    }
}
