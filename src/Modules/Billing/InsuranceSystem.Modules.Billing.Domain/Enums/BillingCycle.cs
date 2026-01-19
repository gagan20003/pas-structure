// File: InsuranceSystem.Modules.Billing.Domain/Enums/BillingCycle.cs
namespace InsuranceSystem.Modules.Billing.Domain.Enums;

/// <summary>
/// Defines the billing cycle frequency.
/// </summary>
public enum BillingCycle
{
    /// <summary>
    /// Annual billing cycle.
    /// </summary>
    Annual = 1,

    /// <summary>
    /// Quarterly billing cycle.
    /// </summary>
    Quarterly = 2,

    /// <summary>
    /// Monthly billing cycle.
    /// </summary>
    Monthly = 3
}
