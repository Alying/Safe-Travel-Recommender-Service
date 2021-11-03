using Management.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Ports
{
    public class RecommendationPort
    {
        private readonly IDecisionEngine _decisionEngine;

        public RecommendationPort(IDecisionEngine decisionEngine)
        {
            _decisionEngine = decisionEngine ?? throw new ArgumentNullException(nameof(decisionEngine));
        }
    }
}
