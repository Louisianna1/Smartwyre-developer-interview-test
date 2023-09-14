using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types
{
    public enum CalculateRebateErrorCode
    {
        RebateNotFoundByIdentifier = 0,
        ProductNotFoundByIdentifier = 1,
        ProductDoesNotSupportIncentiveType = 2,
        RebateAmountIsZero = 3,
        RequestVolumeIsZero = 4,
        RebatePercentageIsZero = 5,
        ProductPriceIsZero = 6
    }
}
