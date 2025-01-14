
# MyMedicationApp

A simple medication management REST API built with **.NET 8** using a **Clean Architecture** approach. This solution demonstrates:

- **Domain**: Core entity definitions  
- **Application**: Business logic (services, interfaces)  
- **Infrastructure**: EF Core (with PostgreSQL), repository implementations  
- **API**: ASP.NET Core Web API  
- **Tests**: xUnit + Moq for unit testing

---

## Requirements

1. **.NET 8 SDK** (Preview/RC)  
   - Verify with `dotnet --list-sdks` – look for a `8.0.x` entry.
2. **Docker & Docker Compose** (recommended for running the API and DB containers)
3. **PostgreSQL** (if not using Docker for the DB)

---

## Project Structure

```
MyMedicationApp
├── MyMedicationApp.sln
├── src
│   ├── MyMedicationApp.Domain
│   │   └── Entities
│   ├── MyMedicationApp.Application
│   │   ├── Interfaces
│   │   └── Services
│   ├── MyMedicationApp.Infrastructure
│   │   ├── Data
│   │   └── Repositories
│   └── MyMedicationApp.Api
│       └── Controllers
└── tests
    └── MyMedicationApp.Tests
```

### Layers

1. **Domain**  
   - Core entities (e.g., `Medication`), potential value objects, enums.
2. **Application**  
   - Interfaces (`IMedicationRepository`), services (`MedicationService`).
3. **Infrastructure**  
   - EF Core (`AppDbContext`), repository implementations (`MedicationRepository`).
4. **API**  
   - REST controllers, dependency injection configuration.
5. **Tests**  
   - xUnit tests with Moq, coverage via Coverlet.

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/milad-ahmd/MyMedicationApp.git
cd MyMedicationApp
```

### 2. Build the Solution

```bash
dotnet build
```
If everything compiles successfully, proceed.

---

## Running the API Locally (Without Docker)

1. **Ensure PostgreSQL is running** (and matches your `appsettings.json`):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=MyMedicationDb;Username=postgres;Password=postgres;"
   }
   ```
2. **Apply EF Migrations**:
   ```bash
   dotnet ef migrations add InitialCreate        --project ./src/MyMedicationApp.Infrastructure/MyMedicationApp.Infrastructure.csproj        --startup-project ./src/MyMedicationApp.Api/MyMedicationApp.Api.csproj

   dotnet ef database update        --project ./src/MyMedicationApp.Infrastructure/MyMedicationApp.Infrastructure.csproj        --startup-project ./src/MyMedicationApp.Api/MyMedicationApp.Api.csproj
   ```
3. **Run the Application**:
   ```bash
   dotnet run --project ./src/MyMedicationApp.Api/MyMedicationApp.Api.csproj
   ```
4. **Swagger** available at:
   - `http://localhost:5000/swagger` (HTTP)  
   - or `https://localhost:7140/swagger` (HTTPS)

---

## Running with Docker Compose

1. **Build and start** containers:
   ```bash
   docker-compose build
   docker-compose up -d
   ```
2. This spins up:
   - `mymedication-db` (Postgres) on **5432**
   - `mymedication-api` (.NET 8 Web API) on **8080**
3. **Swagger** endpoint:
   - `http://localhost:8080/swagger`

---

## Project Highlights

1. **Clean Architecture**  
   - Domain, Application, Infrastructure, and API separation.
2. **EF Core**  
   - `AppDbContext` for PostgreSQL persistence.
3. **Dependency Injection**  
   - Services and Repositories registered in `Program.cs`.
4. **Docker**  
   - Dockerfile + docker-compose for containerizing the API and DB.
5. **Unit Testing**  
   - **xUnit** + **Moq** + **Coverlet** coverage.

---

## Testing

- Run unit tests with coverage:

  ```bash
  dotnet test --collect:"XPlat Code Coverage"
  ```

  Coverage results appear in the output. Increase coverage by adding more tests if needed.

---

## Endpoints Overview

- **GET** `/api/medications`  
  Returns a list of medications.

- **POST** `/api/medications`  
  Creates a new medication. Body example:
  ```json
  {
    "name": "Ibuprofen",
    "quantity": 10
  }
  ```
  - Must have `quantity > 0`.

- **DELETE** `/api/medications/{id}`  
  Deletes a medication by **Guid**.  
  - Returns `204 No Content` on success, `404 Not Found` if not found.

---

## Configuration & Environment

- `appsettings.json` (in `MyMedicationApp.Api`) holds the **DefaultConnection** for the database.
- Override connection strings with environment variables:
  ```bash
  ConnectionStrings__DefaultConnection="Host=...;Database=...;Username=...;Password=..."
  ```
- In `docker-compose.yml`, environment variables are set so the API container connects to the Postgres container internally.

---

## Troubleshooting

1. **.NET 8 not recognized**  
   - Install .NET 8 Preview/RC. Check `dotnet --list-sdks`.
2. **Cannot connect to DB**  
   - Verify PostgreSQL is running. Check your connection string.
3. **Cannot find `Microsoft.EntityFrameworkCore`**  
   - Ensure EF packages are added to **Infrastructure**:
     ```bash
     dotnet add src/MyMedicationApp.Infrastructure/MyMedicationApp.Infrastructure.csproj package Microsoft.EntityFrameworkCore
     dotnet add src/MyMedicationApp.Infrastructure/MyMedicationApp.Infrastructure.csproj package Npgsql.EntityFrameworkCore.PostgreSQL
     ```
   - Then `dotnet restore`, and ensure `using Microsoft.EntityFrameworkCore;` is present.
---

### Author

- **Milad Ahmadi** – [GitHub](https://github.com/milad-ahmd) – *miladahmadi803@gmail.com*

Feel free to open issues or submit PRs for improvements!
