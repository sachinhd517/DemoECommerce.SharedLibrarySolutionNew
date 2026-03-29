# DemoECommerce.SharedLibrarySolutionNew

![.NET](https://img.shields.io/badge/-.NET-blue?logo=.net&logoColor=white)

## 📝 Description

DemoECommerce.SharedLibrarySolutionNew is a foundational .NET library designed to streamline the development of modern e-commerce applications. By providing a centralized, reusable suite of shared models, data transfer objects (DTOs), and core utility classes, this solution ensures architectural consistency and promotes DRY (Don't Repeat Yourself) principles across multiple microservices or modules within an e-commerce ecosystem.

## 🛠️ Tech Stack

- 🔷 .NET


## 📦 Key Dependencies

```
Microsoft.AspNetCore.Authentication.JwtBearer: 9.0.1
Microsoft.EntityFrameworkCore: 9.0.1
Microsoft.EntityFrameworkCore.SqlServer: 9.0.1
Serilog.AspNetCore: 9.0.0
```

## 📁 Project Structure

```
.
├── DemoECommerce.SharedLibrarySolution.sln
└── eCommerce.SharedLibrary
    ├── DependancyInjection
    │   ├── JWTAuthenticationSchema.cs
    │   └── SharedServiceContainer.cs
    ├── Interface
    │   └── IGenericInterface.cs
    ├── Middleware
    │   ├── GobalException.cs
    │   └── ListenToOnlyApiGateway.cs
    ├── Response
    │   └── Response.cs
    └── eCommerce.SharedLibrary.csproj
```

## 🛠️ Development Setup

### .NET Setup
1. Install [.NET SDK](https://dotnet.microsoft.com/)
2. Restore dependencies: `dotnet restore`
3. Build the project: `dotnet build`
4. Run the project: `dotnet run`


## 👥 Contributing

Contributions are welcome! Here's how you can help:

1. **Fork** the repository
2. **Clone** your fork: `git clone https://github.com/sachinhd517/DemoECommerce.SharedLibrarySolutionNew.git`
3. **Create** a new branch: `git checkout -b feature/your-feature`
4. **Commit** your changes: `git commit -am 'Add some feature'`
5. **Push** to your branch: `git push origin feature/your-feature`
6. **Open** a pull request
