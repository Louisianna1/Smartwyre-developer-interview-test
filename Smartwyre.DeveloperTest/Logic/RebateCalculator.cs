using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Logic
{
    public abstract class RebateCalculator
    {
        // All contrete classes must provide an IncentiveType getter property
        public abstract IncentiveType IncentiveType 
        { 
            get; 
        }

        protected Rebate _rebate;

        protected Product _product;

        protected CalculateRebateRequest _request;


        public void InitialiseCalculator(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            _rebate = rebate;
            _product = product;
            _request = request;
        }


        public void ValidateRequestAndCalculateRebate(CalculateRebateResult result)
        {
            ValidateRequest(result);

            if (!result.HasFailed)
            {
                CalculateRebate(result);
                result.Success = true;
            }
            
        }


        protected virtual void ValidateRequest(CalculateRebateResult result)
        {
            // Common validation logic - the virtual keyword allows contrete classes to
            // override to replace / add specific logic
            SupportedIncentiveType supportedIncentiveTypeOfRebate = (SupportedIncentiveType)Enum.Parse(typeof(SupportedIncentiveType), _rebate.Incentive.ToString());
            if (!_product.SupportedIncentives.HasFlag(supportedIncentiveTypeOfRebate))
            {
                result.SetErrorResponse(CalculateRebateErrorCode.ProductDoesNotSupportIncentiveType);
            }

        }


        // Abstract method to calculate the rebate amount that must be implemented by contrete classes
        protected abstract void CalculateRebate(CalculateRebateResult result);
    }
}
