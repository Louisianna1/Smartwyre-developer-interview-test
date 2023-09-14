using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Logic
{
    public class AmountPerUomCalculator : RebateCalculator
    {
        public override IncentiveType IncentiveType
        {
            get => IncentiveType.AmountPerUom;
        }


        protected override void ValidateRequest(CalculateRebateResult result)
        {
            base.ValidateRequest(result);

            if (!result.HasFailed)
            {
                if (_rebate.Amount == 0)
                {
                    result.SetErrorResponse(CalculateRebateErrorCode.RebateAmountIsZero);
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
            result.RebateAmount += _rebate.Amount * _request.Volume;
        }
    }
}
