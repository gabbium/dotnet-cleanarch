# CleanArch

![GitHub last commit](https://img.shields.io/github/last-commit/gabbium/dotnet-cleanarch)
![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gabbium_dotnet-cleanarch?server=https%3A%2F%2Fsonarcloud.io)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gabbium_dotnet-cleanarch?server=https%3A%2F%2Fsonarcloud.io)
![NuGet](https://img.shields.io/nuget/v/Gabbium.CleanArch)

A lightweight **.NET library** providing **Clean Architecture building blocks** like a **Result pattern** and **CQRS abstractions**.

---

## ✨ Features

-   ✅ **Result Pattern** for explicit success/failure handling
-   ✅ **Standardized Error Types** (`Validation`, `NotFound`, `Conflict`, `Unauthorized`, `Forbidden`, `Failure`, )
-   ✅ **CQRS Abstractions** (`ICommand`, `IQuery`, `IDomainEvent`)

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

## 🪪 License

This project is licensed under the MIT License – see [LICENSE](LICENSE) for details.
