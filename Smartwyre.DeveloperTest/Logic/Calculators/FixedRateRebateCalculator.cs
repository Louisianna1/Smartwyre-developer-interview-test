using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Logic
{
    public class FixedRateRebateCalculator : RebateCalculator
    {
        public override IncentiveType IncentiveType
        {
            get => IncentiveType.FixedRateRebate;
        }


        protected override void ValidateRequest(CalculateRebateResult result)
        {
            base.ValidateRequest(result);

            if (!result.HasFailed)
            {
                if (_rebate.Percentage == 0)
                {
                    result.SetErrorResponse(CalculateRebateErrorCode.RebatePercentageIsZero);
                    return;
                }
                if (_product.Price == 0)
                {
                    result.SetErrorResponse(CalculateRebateErrorCode.ProductPriceIsZero);
                    return;
                }
                if (_request.Volume == 0)
                {
                    result.SetErrorResponse(CalculateRebateErrorCode.RequestVolumeIsZero);
                }

            }
        }


        protected override void CalculateRebate(CalculateRebateResult result)
        {
            result.RebateAmount += _product.Price * _rebate.Percentage * _request.Volume;
        }
    }
}
