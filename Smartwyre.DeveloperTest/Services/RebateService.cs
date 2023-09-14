using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Logic;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;

    private readonly IProductDataStore _productDataStore;

    private readonly RebateCalculatorFactory _rebateCalculatorFactory;


    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, RebateCalculatorFactory rebateCalculatorFactory)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculatorFactory = rebateCalculatorFactory; 

    }


    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        // Attempt to load the rebate and product from the respective data stores
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        CalculateRebateResult result = new CalculateRebateResult();

        if (rebate == null) 
        {
            result.SetErrorResponse(CalculateRebateErrorCode.RebateNotFoundByIdentifier);
            return result;
        }
        if (product == null)
        {
            result.SetErrorResponse(CalculateRebateErrorCode.ProductNotFoundByIdentifier);
            return result;
        }

        // Use the Factory method and .NET Dependency Injection to validate and calculate
        // the rebate request based on Incentive Type
        RebateCalculator calculator = _rebateCalculatorFactory.GetRebateCalculator(rebate.Incentive);
        calculator.InitialiseCalculator(rebate, product, request);
        calculator.ValidateRequestAndCalculateRebate(result);

        // If the calculation was successful, store the result
        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, result.RebateAmount);
        }

        return result;
    }
}
