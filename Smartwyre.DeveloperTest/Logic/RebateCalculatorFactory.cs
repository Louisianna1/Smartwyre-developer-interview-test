using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Logic
{
    public class RebateCalculatorFactory
    {
        private readonly IEnumerable<RebateCalculator> _rebateCalculators;

        public RebateCalculatorFactory(IEnumerable<RebateCalculator> rebateCalculators)
        {
            _rebateCalculators = rebateCalculators;
        }


        public RebateCalculator GetRebateCalculator(IncentiveType incentiveType)
        {
            return _rebateCalculators.FirstOrDefault(e => e.IncentiveType == incentiveType)
                ?? throw new NotSupportedException();
        }
    }
}
