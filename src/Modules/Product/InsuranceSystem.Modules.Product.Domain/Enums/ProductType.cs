// File: InsuranceSystem.Modules.Product.Domain/Enums/ProductType.cs
namespace InsuranceSystem.Modules.Product.Domain.Enums;

/// <summary>
/// Defines the type of insurance product.
/// </summary>
public enum ProductType
{
    /// <summary>
    /// Health insurance product.
    /// </summary>
    Health = 1,

    /// <summary>
    /// Life insurance product.
    /// </summary>
    Life = 2,

    /// <summary>
    /// Dental insurance product.
    /// </summary>
    Dental = 3,

    /// <summary>
    /// Vision insurance product.
    /// </summary>
    Vision = 4,

    /// <summary>
    /// Disability insurance product.
    /// </summary>
    Disability = 5
}
