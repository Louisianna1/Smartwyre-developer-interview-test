using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.DummyServices
{
    public class DummyRebateDataStore : IRebateDataStore
    {
        public enum DummyRebateIdentifier
        {
            NonExistentRebateIdentifier,
            ExistingRebateIdentifier_FixedRateRebate_PercentageZero,
            ExistingRebateIdentifier_AmountPerUom_AmountZero,
            ExistingRebateIdentifier_AmountPerUom_AllValuesSet,
            ExistingRebateIdentifier_FixedCashAmount_AmountZero,
            ExistingRebateIdentifier_FixedCashAmount_AllValuesSet,
            ExistingRebateIdentifier_FixedRateRebate_AllValuesSet
        }


        public Rebate GetRebate(string rebateIdentifier)
        {
            if (rebateIdentifier == DummyRebateIdentifier.NonExistentRebateIdentifier.ToString())
            {
                return null;
            }

            Rebate dummyRebate = new Rebate();
            dummyRebate.Identifier = rebateIdentifier;

            if (rebateIdentifier == DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_PercentageZero.ToString())
            {
                dummyRebate.Incentive = IncentiveType.FixedRateRebate;
                dummyRebate.Percentage = 0; // invalid
                dummyRebate.Amount = 10; // aribrary non-zero value (valid)
            }

            else if (rebateIdentifier == DummyRebateIdentifier.ExistingRebateIdentifier_AmountPerUom_AmountZero.ToString())
            {
                dummyRebate.Incentive = IncentiveType.AmountPerUom;
                dummyRebate.Percentage = 50; // aribrary non-zero value (valid)
                dummyRebate.Amount = 0; // invalid
            }

            else if (rebateIdentifier == DummyRebateIdentifier.ExistingRebateIdentifier_AmountPerUom_AllValuesSet.ToString())
            {
                dummyRebate.Incentive = IncentiveType.AmountPerUom;
                dummyRebate.Percentage = 50; // aribrary non-zero value (valid)
                dummyRebate.Amount = 5; // aribrary non-zero value (valid)
            }

            else if (rebateIdentifier == DummyRebateIdentifier.ExistingRebateIdentifier_FixedCashAmount_AmountZero.ToString())
            {
                dummyRebate.Incentive = IncentiveType.FixedCashAmount;
                dummyRebate.Percentage = 50; // aribrary non-zero value (valid)
                dummyRebate.Amount = 0; // invalid
            }

            else if (rebateIdentifier == DummyRebateIdentifier.ExistingRebateIdentifier_FixedCashAmount_AllValuesSet.ToString())
            {
                dummyRebate.Incentive = IncentiveType.FixedCashAmount;
                dummyRebate.Percentage = 50; // aribrary non-zero value (valid)
                dummyRebate.Amount = 5; // aribrary non-zero value (valid)
            }

            else if (rebateIdentifier == DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_AllValuesSet.ToString())
            {
                dummyRebate.Incentive = IncentiveType.FixedRateRebate;
                dummyRebate.Percentage = 50; // aribrary non-zero value (valid)
                dummyRebate.Amount = 5; // aribrary non-zero value (valid)
            }

            return dummyRebate;

        }

        public void StoreCalculationResult(Rebate account, decimal rebateAmount)
        {
            // Todo: Implement dummy logic to allow testing in the future
        }
    }
}
