namespace Smartwyre.DeveloperTest.Types;

public class CalculateRebateResult
{
    public bool Success { get; set; }

    // Property added to store the error code in case of validation error 
    public CalculateRebateErrorCode? ErrorCode { get; set; }

    public bool HasFailed {
        get => Success == false && ErrorCode != null;
    }

    // Property added to store the result of the rebate calculation
    public decimal RebateAmount { get; set; }


    public void SetErrorResponse(CalculateRebateErrorCode calculateRebateErrorCode)
    {
        ErrorCode = calculateRebateErrorCode;
        Success = false;

    }
}
