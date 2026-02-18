# MasPatas - Clean Architecture ASP.NET Core Web API (.NET 8)

This repository contains a production-ready starter Web API built with **Clean Architecture** and the requested stack:

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- FluentValidation
- AutoMapper
- Swagger/OpenAPI
- Global exception handling middleware

## Solution Structure

```text
MasPatas.sln
src/
  MasPatas.API/             --> Presentation layer (controllers, middleware, startup)
  MasPatas.Application/     --> Use cases, DTOs, validation, mapping, interfaces
  MasPatas.Domain/          --> Core entities + repository contracts
  MasPatas.Infrastructure/  --> EF Core DbContext + repository implementations
```

## Projects and Responsibilities

### 1) MasPatas.Domain
- `Product` entity.
- `IProductRepository` contract.

### 2) MasPatas.Application
- DTOs: `CreateProductDto`, `ProductDto`.
- Use cases encapsulated in `ProductService`:
  - `CreateProductAsync`
  - `GetAllProductsAsync`
  - `GetProductByIdAsync`
- FluentValidation rules for product creation.
- AutoMapper profile for DTO/entity mapping.

### 3) MasPatas.Infrastructure
- `MasPatasDbContext`.
- EF Core entity configuration (`ProductConfiguration`).
- `ProductRepository` implementation of `IProductRepository`.
- Infrastructure DI registration.

### 4) MasPatas.API
- `ProductsController` endpoints:
  - `POST /api/products`
  - `GET /api/products`
  - `GET /api/products/{id}`
- Global exception handling middleware.
- Swagger configuration.
- Composition root (`Program.cs`).

## Run the API

From the repo root:

```bash
dotnet restore
cd src/MasPatas.API
dotnet run
```

Swagger UI is available at:

- `https://localhost:<port>/swagger`

## EF Core Migrations (Code First)

> Run commands from the repository root.

### Create migration

```bash
dotnet ef migrations add InitialCreate \
  --project src/MasPatas.Infrastructure \
  --startup-project src/MasPatas.API \
  --output-dir Persistence/Migrations
```

### Apply migration

```bash
dotnet ef database update \
  --project src/MasPatas.Infrastructure \
  --startup-project src/MasPatas.API
```

## Notes

- Update `ConnectionStrings:DefaultConnection` in `src/MasPatas.API/appsettings.json` for your SQL Server instance.
- All repository/service operations are asynchronous and support cancellation tokens.
