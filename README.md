# Insurance System - Enterprise .NET Application

A Clean Architecture implementation of an enterprise Insurance System built as a Modular Monolith using .NET 9, C# 13, and Entity Framework Core with Oracle Database support.

## 🏗️ Architecture Overview

This solution follows **Clean Architecture** principles implemented as a **Modular Monolith**, providing clear separation of concerns while maintaining the simplicity of a single deployable unit.

```
InsuranceSystem/
├── InsuranceSystem.sln
└── src/
    ├── SharedKernel/                          # Common abstractions
    │   └── InsuranceSystem.SharedKernel/
    │       └── Domain/
    │           └── AuditableEntity.cs         # Base entity class
    └── Modules/
        ├── Billing/                           # Billing Module
        │   ├── InsuranceSystem.Modules.Billing.Domain/
        │   │   ├── Entities/
        │   │   │   ├── BillingAccount.cs
        │   │   │   ├── BillingInstallment.cs
        │   │   │   ├── Invoice.cs
        │   │   │   ├── InvoiceInstallment.cs
        │   │   │   └── Payment.cs
        │   │   └── Enums/
        │   │       ├── BillingAccountType.cs
        │   │       ├── BillingCycle.cs
        │   │       ├── InstallmentType.cs
        │   │       ├── InvoiceStatus.cs
        │   │       ├── PaymentMode.cs
        │   │       ├── PaymentStatus.cs
        │   │       ├── Status.cs
        │   │       └── TransactionType.cs
        │   └── InsuranceSystem.Modules.Billing.Infrastructure/
        │       ├── Configurations/
        │       │   ├── BillingAccountConfiguration.cs
        │       │   ├── BillingInstallmentConfiguration.cs
        │       │   ├── InvoiceConfiguration.cs
        │       │   ├── InvoiceInstallmentConfiguration.cs
        │       │   └── PaymentConfiguration.cs
        │       └── BillingDbContext.cs
        ├── Contract/                          # Contract Module
        │   ├── InsuranceSystem.Modules.Contract.Domain/
        │   │   ├── Entities/
        │   │   │   ├── Contract.cs
        │   │   │   ├── Endorsement.cs
        │   │   │   └── MasterContract.cs
        │   │   └── Enums/
        │   │       ├── ContractStatus.cs
        │   │       ├── EndorsementStatus.cs
        │   │       └── EndorsementType.cs
        │   └── InsuranceSystem.Modules.Contract.Infrastructure/
        │       ├── Configurations/
        │       │   ├── ContractConfiguration.cs
        │       │   ├── EndorsementConfiguration.cs
        │       │   └── MasterContractConfiguration.cs
        │       └── ContractDbContext.cs
        ├── Member/                            # Member Module
        │   ├── InsuranceSystem.Modules.Member.Domain/
        │   │   ├── Entities/
        │   │   │   └── Member.cs
        │   │   └── Enums/
        │   │       ├── Gender.cs
        │   │       └── MemberStatus.cs
        │   └── InsuranceSystem.Modules.Member.Infrastructure/
        │       ├── Configurations/
        │       │   └── MemberConfiguration.cs
        │       └── MemberDbContext.cs
        └── Product/                           # Product Module
            ├── InsuranceSystem.Modules.Product.Domain/
            │   ├── Entities/
            │   │   └── Product.cs
            │   └── Enums/
            │       ├── ProductStatus.cs
            │       └── ProductType.cs
            └── InsuranceSystem.Modules.Product.Infrastructure/
                ├── Configurations/
                │   └── ProductConfiguration.cs
                └── ProductDbContext.cs
```

## 🛠️ Technology Stack

| Component | Technology |
|-----------|------------|
| **Framework** | .NET 9.0 |
| **Language** | C# 13 |
| **ORM** | Entity Framework Core 9.0 |
| **Database** | Oracle Database 19c/21c |
| **Provider** | Oracle.EntityFrameworkCore 9.23.60 |

## 📦 Module Overview

### SharedKernel
Contains common abstractions used across all modules:
- **AuditableEntity**: Base class with `Id`, `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`

### Billing Module
Handles all billing-related operations based on the ER diagram:
- **Entities**: BillingAccount, BillingInstallment, Invoice, InvoiceInstallment, Payment
- **Relationships**: BillingAccount → BillingInstallments, Invoices, Payments
- **Features**: Payment tracking, invoice management, installment billing

### Contract Module
Manages insurance contracts and endorsements:
- **Entities**: MasterContract, Contract, Endorsement
- **Features**: Contract lifecycle management, endorsement processing

### Member Module
Handles insured member information:
- **Entities**: Member
- **Features**: Member registration, coverage tracking, age calculation

### Product Module
Manages insurance products:
- **Entities**: Product
- **Features**: Product catalog, premium calculation, availability checks

## ✨ C# 13+ Features Used

- **File-scoped namespaces**: Cleaner file structure
- **Primary constructors**: Used in DbContext classes
- **Required properties**: Ensures required fields are set at initialization
- **Collection expressions**: `ICollection<T>` initialized with `[]`

## 🗄️ Database Mapping Standards

### Oracle-Specific Configuration
- **Primary Keys**: Oracle IDENTITY columns (`.UseIdentityColumn()`)
- **Table Names**: UPPER_SNAKE_CASE (e.g., `BILLING_ACCOUNTS`)
- **Column Names**: UPPER_SNAKE_CASE (e.g., `CREATED_ON`)
- **Schemas**: Each module has its own schema (BILLING, CONTRACT, MEMBER, PRODUCT)

### Entity Configuration
- All mappings use **Fluent API** (`IEntityTypeConfiguration<T>`)
- No Data Annotations on entities
- Explicit foreign key constraints with named constraints
- Indexes for frequently queried columns

### Type Mappings
| C# Type | Oracle Mapping | Notes |
|---------|----------------|-------|
| `decimal` | NUMBER(18,2) | Monetary values |
| `DateOnly` | DATE | Dates without time (e.g., DOB) |
| `DateTime` | TIMESTAMP | Timestamps with time |
| `enum` | NUMBER | Integer conversion |

## 🏃 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Oracle Database 19c or 21c
- Oracle Client libraries

### Build the Solution
```bash
cd /Users/gaganmahtoliya/Desktop/pas
dotnet restore
dotnet build
```

### Configure Database Connection
Add your Oracle connection string to your configuration:
```json
{
  "ConnectionStrings": {
    "BillingDb": "User Id=billing;Password=xxx;Data Source=//host:1521/service",
    "ContractDb": "User Id=contract;Password=xxx;Data Source=//host:1521/service",
    "MemberDb": "User Id=member;Password=xxx;Data Source=//host:1521/service",
    "ProductDb": "User Id=product;Password=xxx;Data Source=//host:1521/service"
  }
}
```

### Register DbContexts
```csharp
services.AddDbContext<BillingDbContext>(options =>
    options.UseOracle(connectionString));
```

## 🎯 Domain Behaviors

Entities include domain behaviors to avoid anemic domain models:

```csharp
// BillingAccount
billingAccount.ApplyPayment(100.00m);
billingAccount.AddCharge(50.00m);
billingAccount.Activate();

// Invoice
invoice.Issue();
invoice.Cancel("Duplicate invoice");
invoice.CalculateBalance();

// Member
member.CalculateAge();
member.IsCoverageActive();
member.Terminate(DateOnly.FromDateTime(DateTime.Now));

// Contract
contract.IsActive();
contract.GetRemainingDays();
contract.Terminate(terminationDate);

// Endorsement
endorsement.Approve();
endorsement.Process("admin@company.com");
```

## 📋 Enum Definitions

All enums use explicit integer values as specified in the ER diagram:

```csharp
public enum BillingAccountType
{
    Employer = 1,
    Individual = 2
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3
}

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Cancelled = 3
}
```


---

*Generated with Clean Architecture principles for enterprise-grade maintainability and scalability.*
