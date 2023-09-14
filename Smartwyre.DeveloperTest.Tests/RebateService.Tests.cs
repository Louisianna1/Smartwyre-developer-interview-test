using System;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Abstractions;

using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Tests.DummyServices;
using Smartwyre.DeveloperTest.Tests.Fixtures;
using Smartwyre.DeveloperTest.Types;


namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests : Xunit.Microsoft.DependencyInjection.Abstracts.TestBed<TestFixture>
{
    public RebateServiceTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
        : base(testOutputHelper, fixture)
    {
    }


    [Fact]
    public void Calculate_NonExistentRebateIdentifier_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.NonExistentRebateIdentifier.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success, 
            "The Calculate request fails when a non-existent Rebate Identifier is supplied in the request.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.RebateNotFoundByIdentifier,
            "RebateNotFoundByIdentifier error code is returned in the response.");

    }


    [Fact]
    public void Calculate_NonExistentProductIdentifier_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_PercentageZero.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.NonExistentProductIdentifier.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails when a non-existent Product Identifier is supplied in the request.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.ProductNotFoundByIdentifier,
            "ProductNotFoundByIdentifier error code is returned in the response.");

    }


    [Fact]
    public void Calculate_ProductDoesNotSupportIncentiveType_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_PercentageZero.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatDoesNotSupportFixedRateRebate.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails when the supported incentive types for the product do not match that of the rebate.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.ProductDoesNotSupportIncentiveType,
            "ProductDoesNotSupportIncentiveType error code is returned in the response.");

    }


    [Fact]
    public void AmountPerUomCalculator_RebateAmountZero_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_AmountPerUom_AmountZero.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails for AmountPerUom rebate incentive type with Amount zero.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.RebateAmountIsZero,
            "RebateAmountIsZero error code is returned in the response.");

    }


    [Fact]
    public void AmountPerUomCalculator_RequestVolumeZero_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.Volume = 0; // Setting explicitly to zero - invalid value
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_AmountPerUom_AllValuesSet.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails for AmountPerUom rebate incentive type with request Volume zero.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.RequestVolumeIsZero,
            "RequestVolumeIsZero error code is returned in the response.");

    }


    [Fact]
    public void AmountPerUomCalculator_ValidRequest_ReturnsValue()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.Volume = 2; // Valid value
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_AmountPerUom_AllValuesSet.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.True(result.Success,
            "The Calculate request was successful.");
        Assert.True(result.RebateAmount == 10,
            "RebateAmount returned the expected value - 10.");

    }


    [Fact]
    public void FixedCashAmountCalculator_RebateAmountZero_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedCashAmount_AmountZero.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails for FixedCashAmount rebate incentive type with Amount zero.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.RebateAmountIsZero,
            "RebateAmountIsZero error code is returned in the response.");

    }


    [Fact]
    public void FixedCashAmountCalculator_ValidRequest_ReturnsValue()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.Volume = 2; // Valid value
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedCashAmount_AllValuesSet.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.True(result.Success,
            "The Calculate request was successful.");
        Assert.True(result.RebateAmount == 5,
            "RebateAmount returned the expected value - 5.");

    }


    [Fact]
    public void FixedRateRebateCalculator_RebatePercentageZero_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_PercentageZero.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails for FixedRateRebate rebate incentive type with Percentage zero.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.RebatePercentageIsZero,
            "RebatePercentageIsZero error code is returned in the response.");

    }


    [Fact]
    public void FixedRateRebateCalculator_ProductPriceZero_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_AllValuesSet.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatHasPriceZero.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails for FixedRateRebate rebate incentive type with Product Price zero.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.ProductPriceIsZero,
            "ProductPriceIsZero error code is returned in the response.");

    }


    [Fact]
    public void FixedRateRebateCalculator_RequestVolumeZero_ReturnsError()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.Volume = 0; // invalid
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_AllValuesSet.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.False(result.Success,
            "The Calculate request fails for FixedRateRebate rebate incentive type with request Volume zero.");
        Assert.True(result.ErrorCode == CalculateRebateErrorCode.RequestVolumeIsZero,
            "RequestVolumeIsZero error code is returned in the response.");

    }


    [Fact]
    public void FixedRateRebateCalculator_ValidRequest_ReturnsValue()
    {
        var rebateService = _fixture.GetService<IRebateService>(_testOutputHelper);
        CalculateRebateRequest request = new CalculateRebateRequest();
        request.Volume = 4; // Valid value
        request.RebateIdentifier = DummyRebateDataStore.DummyRebateIdentifier.ExistingRebateIdentifier_FixedRateRebate_AllValuesSet.ToString();
        request.ProductIdentifier = DummyProductDataStore.DummyProductIdentifier.ProductThatSupportsAllIncentiveTypes.ToString();
        CalculateRebateResult result = rebateService.Calculate(request);

        Assert.True(result.Success,
            "The Calculate request was successful.");
        Assert.True(result.RebateAmount == 1000,
            "RebateAmount returned the expected value - 1000.");

    }

}
