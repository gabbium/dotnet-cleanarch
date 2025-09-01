# CleanArch

![GitHub last commit](https://img.shields.io/github/last-commit/gabbium/dotnet-cleanarch)
![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gabbium_dotnet-cleanarch?server=https%3A%2F%2Fsonarcloud.io)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gabbium_dotnet-cleanarch?server=https%3A%2F%2Fsonarcloud.io)
![NuGet](https://img.shields.io/nuget/v/Gabbium.CleanArch)

A lightweight **.NET library** providing **Clean Architecture building blocks** like a **Result pattern**, **CQRS abstractions** and a **Specification pattern**.

---

## ✨ Features

-   ✅ **Result Pattern** for explicit success/failure handling
-   ✅ **Standardized Error Types** (`Failure`, `Validation`, `Problem`, `NotFound`, `Conflict`)
-   ✅ **CQRS Abstractions** (`ICommandHandler`, `IQueryHandler`)
-   ✅ **Specification Pattern** (`ISpecification<T>`, `Specification<T>`)
-   ✅ **Unit of Work abstractions** (`IUnitOfWork`)
-   ✅ **Clean, dependency-free core implementation**

---

## 🧱 Tech Stack

| Layer   | Stack                             |
| ------- | --------------------------------- |
| Runtime | .NET 9                            |
| Package | NuGet                             |
| CI/CD   | GitHub Actions + semantic-release |

---

## 📦 Installation

```bash
dotnet add package Gabbium.CleanArch
```

---

## 🚀 Usage

**Results**

```csharp
Result<string> user = Result<string>.Success("John Doe");

if (user.IsSuccess)
{
    Console.WriteLine(user.Value);
}
else
{
    Console.WriteLine(user.Error.Description);
}
```

**Messaging**

```csharp
public class CreateUserHandler : ICommandHandler<CreateUserCommand>
{
    public Task<Result> HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        // business logic...
        return Task.FromResult(Result.Success());
    }
}
```

**Specification**

```csharp
public sealed class ProductBySkuSpec(string sku) : Specification<Product>
{
    public ProductBySkuSpec : this(sku)
    {
        Criteria = p => p.Sku == sku;
    }
}
```

---

## 🧱 Error Types & Usage

The library defines a **small, explicit set of error categories** to represent failures consistently across **domain**, **application**, and **infrastructure** layers.

Each `ErrorType` communicates **why** an operation failed, without assuming how it will be presented (HTTP, gRPC, messaging, etc.).

-   **Validation** → request contains invalid/missing fields or violates a domain rule.
-   **Problem** → known business rule prevents the operation (not invalid input).
-   **NotFound** → entity or resource does not exist.
-   **Conflict** → valid operation but conflicting state prevents it.
-   **Failure** → unexpected/unhandled error.

**Design intention:**

-   Use **Validation** when multiple field errors at once.
-   Use **Problem** when a single business rule violation.
-   Errors are **transport-agnostic** — describe the reason, not the protocol.

---

## 🪪 License

This project is licensed under the MIT License – see [LICENSE](LICENSE) for details.
