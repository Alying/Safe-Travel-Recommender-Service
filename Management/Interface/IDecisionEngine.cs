using Management.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Management.Interface
{
    public interface IDecisionEngine
    {
        Task<IEnumerable<Recommendation>> CalculateDesiredLocationAsync();
    }
}
