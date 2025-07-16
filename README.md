
# TODO.Common

`TODO.Common` is a shared library used across microservices in the TODO application ecosystem. It provides standardized components for JWT authentication, exception handling, JSON serialization, repository patterns, and identity management in ASP.NET Core applications.

---

## 🧩 Tech Stack

- .NET 9  
- ASP.NET Core Middleware & Extensions  
- JWT Bearer Authentication  
- Entity Framework Core  
- Clean Architecture-compatible utilities  

---

## ⚙️ Features

### 🔐 Authentication & Authorization

- Centralized JWT configuration for issuer, audience, and signing key
- Built-in policies (`Authenticated`, `AllowAnonymous`)
- Simple setup via `AddAuthConfiguration()` and `UseAuth()`

### 🧱 Exception Handling

- Middleware to catch common exceptions (e.g., `Unauthorized`, `NotFound`, `BadRequest`)
- Returns consistent camelCase JSON error responses

### 📦 JSON Serialization

- Configure global camelCase serialization for API responses
- Works with both `System.Text.Json` and MVC JSON options

### 🗃️ Generic Repository

- Base repository with common async CRUD operations
- Designed for EF Core and cleanly abstracted

### 👤 Identity Abstraction

- Provides access to current user's ID, email, roles, and name
- Works via `IHttpContextAccessor`

---

## 🧪 Example Integration

```csharp
// Program.cs or Startup.cs
services.AddAuthConfiguration("issuer", "audience", "secret");
services.AddCamelCaseJson();
services.AddIdentityService();

app.UseJsonExceptionHandler();
app.UseAuth();
```

---

## 📁 Structure

- `Middlewares/` – Auth, exception handling, JSON config  
- `Repositories/` – Generic base repository and interfaces  
- `Services/` – Identity abstraction for current user context  
- `Models/` – Common exceptions and error types  

---

## 🧱 Intended Use

This library is meant to be referenced by all microservices in the TODO system to ensure a consistent approach to:

- Auth
- Error handling
- Identity access
- Data access conventions

---

## 📄 License

MIT — see `LICENSE` file.
