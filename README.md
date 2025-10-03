# DeltaApi - Claims Management System

## 📋 Description

DeltaApi is a claims management system developed with **Clean Architecture** and **.NET 8**. The system allows creating, querying, updating, and managing insurance claims efficiently and scalably.

## 🏗️ Architecture

The project follows **Clean Architecture** principles with the following layers:

```
DeltaApi/
├── DeltaApi.API/           # Presentation layer (Web API)
├── DeltaApi.Application/   # Application layer (Use cases)
├── DeltaApi.Domain/        # Domain layer (Entities and business rules)
└── DeltaApi.Infrastructure/ # Infrastructure layer (Database and external services)
```

### 🎯 Clean Architecture Principles

- **Framework Independence**: Does not depend on external frameworks
- **Testability**: Easy to test without external dependencies
- **UI Independence**: Business logic does not depend on the interface
- **Database Independence**: Business logic does not depend on the database
- **External Services Independence**: Business logic does not depend on external services

## 🚀 Technologies Used

### Backend
- **.NET 8** - Main framework
- **ASP.NET Core Web API** - For REST API
- **Dapper** - ORM for data access
- **PostgreSQL** - Main database
- **FluentValidation** - Data validation
- **Serilog** - Structured logging

### Development Tools
- **Swagger/OpenAPI** - API documentation
- **Git** - Version control
- **PowerShell** - Migration scripts

## 📁 Project Structure

### DeltaApi.API (Presentation Layer)
```
DeltaApi.API/
├── Controllers/
│   └── ClaimsController.cs          # Claims controller
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs # Global exception handling
├── Extensions/
│   ├── ServiceCollectionExtensions.cs # Service configuration
│   └── WebApplicationExtensions.cs   # Application configuration
├── appsettings.json                 # Application configuration
└── Program.cs                       # Entry point
```

### DeltaApi.Application (Application Layer)
```
DeltaApi.Application/
├── DTOs/Claims/                     # Data transfer objects
│   ├── CreateClaimRequest.cs
│   ├── UpdateClaimRequest.cs
│   ├── ClaimResponse.cs
│   ├── AddDocumentToClaimRequest.cs
│   └── AddEventToClaimRequest.cs
├── Interfaces/Claims/               # Use case interfaces
├── UseCases/Claims/                 # Use case implementations
├── Services/                        # Application services
├── Validators/Claims/               # FluentValidation validators
└── Extensions/                      # Configuration extensions
```

### DeltaApi.Domain (Domain Layer)
```
DeltaApi.Domain/
├── Claims/                          # Domain entities
│   ├── Claim.cs                    # Main claim entity
│   ├── ClaimDocument.cs            # Claim documents
│   ├── ClaimEvent.cs               # Claim events
│   ├── ClaimType.cs                # Claim types
│   ├── Doctor.cs                   # Doctor information
│   └── StateOfLoss.cs              # Loss states
├── ValueObjects/                    # Value objects
│   ├── PolicyInfo.cs               # Policy information
│   ├── InjuryInfo.cs               # Injury information
│   ├── DoctorInfo.cs               # Doctor information
│   └── Address.cs                  # Addresses
├── Repositories/                    # Repository interfaces
├── Services/                        # Domain services
└── Events/                         # Domain events
```

### DeltaApi.Infrastructure (Infrastructure Layer)
```
DeltaApi.Infrastructure/
├── Data/                           # Data configuration
│   ├── ConnectionFactory.cs        # Connection factory
│   ├── DapperContext.cs            # Dapper context
│   └── GuidTypeHandler.cs          # GUID type handler
├── Repositories/                   # Repository implementations
│   ├── ClaimRepository.cs          # Claims repository
│   ├── ClaimTypeRepository.cs      # Types repository
│   ├── DoctorRepository.cs         # Doctors repository
│   └── StateOfLossRepository.cs    # States repository
├── Mappings/                       # Data mappers
├── Migrations/                     # Database migration scripts
└── Extensions/                     # Configuration extensions
```

## 🛠️ Installation and Configuration

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- [Git](https://git-scm.com/)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/agustinaguirre-jbk/Claim-management.git
   cd Claim-management
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the database**
   
   Edit `src/CleanArchitecture/DeltaApi.API/DeltaApi.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=claims_db;Username=your_user;Password=your_password;"
     }
   }
   ```

4. **Run migrations**
   ```bash
   # Run the migration script
   .\src\CleanArchitecture\DeltaApi.Infrastructure\Migrations\ExecuteMigration.ps1
   ```

5. **Build the project**
   ```bash
   dotnet build
   ```

6. **Run the application**
   ```bash
   dotnet run --project "src\CleanArchitecture\DeltaApi.API\DeltaApi.API\DeltaApi.API.csproj"
   ```

## 🌐 API Endpoints

### Claims

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/Claims` | Create a new claim |
| `GET` | `/api/Claims/{id}` | Get claim by ID |
| `PUT` | `/api/Claims/{id}` | Update claim |
| `POST` | `/api/Claims/{id}/documents` | Add document to claim |
| `POST` | `/api/Claims/{id}/events` | Add event to claim |

### Usage Examples

#### Create a Claim
```http
POST /api/Claims
Content-Type: application/json

{
  "caseId": 12345,
  "claimTypeId": 1,
  "clientId": 100,
  "policyNumber": "POL-2024-001",
  "deltaFileNumber": "DEL-001",
  "clientFileNumber": "CLI-001",
  "doctorId": 1,
  "stateOfLossId": 1,
  "allegedInjury": "Back injury",
  "injuryDescription": "Severe lumbar pain",
  "attorneyRepresentation": true,
  "liability": "Insured's responsibility",
  "workersCompensation": false,
  "exposure": 50000.00
}
```

#### Get a Claim
```http
GET /api/Claims/09aae6fa-e4c6-4f21-b17f-9bf9792b323b
```

## 🗄️ Database

### Main Schema

The system uses PostgreSQL with the following main schema:

```sql
-- Migration: Create Claims Tables for PostgreSQL (snake_case + schema)
-- Description: Creates all tables for the Claims domain inside schema "claims"


-- Create claims table
CREATE TABLE claims.claim (
    claim_id UUID PRIMARY KEY,
    case_id INTEGER NOT NULL,
    claim_type_id INTEGER NOT NULL,
    claimant_id INTEGER NOT NULL,
    client_id INTEGER NOT NULL,
    policy_number VARCHAR(100) NOT NULL,
    delta_file_number VARCHAR(50) NULL,
    client_file_number VARCHAR(50) NULL,
    doctor_id INTEGER NULL,
    state_of_loss_id INTEGER NULL,
    alleged_injury VARCHAR(255) NULL,
    injury_description TEXT NULL,
    attorney_representation BOOLEAN NOT NULL DEFAULT FALSE,
    liability VARCHAR(255) NULL,
    workers_compensation BOOLEAN NOT NULL DEFAULT FALSE,
    exposure DECIMAL(18,2) NULL,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create claim_documents table
CREATE TABLE claims.claim_documents (
    document_id UUID PRIMARY KEY,
    claim_id UUID NOT NULL,
    document_name VARCHAR(255) NOT NULL,
    document_type VARCHAR(100) NOT NULL,
    file_path VARCHAR(500) NOT NULL,
    file_size BIGINT NOT NULL,
    uploaded_by_user INTEGER NOT NULL,
    uploaded_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL,
    FOREIGN KEY (claim_id) REFERENCES claims.claims(claim_id)
);

-- Create claim_events table
CREATE TABLE claims.claim_events (
    event_id UUID PRIMARY KEY,
    claim_id UUID NOT NULL,
    event_type VARCHAR(100) NOT NULL,
    event_description TEXT NOT NULL,
    event_date TIMESTAMP WITH TIME ZONE NOT NULL,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    FOREIGN KEY (claim_id) REFERENCES claims.claims(claim_id)
);

-- Create doctors table
CREATE TABLE claims.doctors (
    doctor_id SERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    specialization VARCHAR(100) NULL,
    license_number VARCHAR(50) NULL,
    phone_number VARCHAR(20) NULL,
    email VARCHAR(255) NULL,
    address TEXT NULL,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create claim_types table
CREATE TABLE claims.claim_types (
    claim_type_id SERIAL PRIMARY KEY,
    type_name VARCHAR(100) NOT NULL,
    description TEXT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create states_of_loss table
CREATE TABLE claims.states_of_loss (
    state_of_loss_id SERIAL PRIMARY KEY,
    state_name VARCHAR(100) NOT NULL,
    state_code VARCHAR(10) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_by_user INTEGER NOT NULL,
    created_on TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by_user INTEGER NULL,
    modified_on TIMESTAMP WITH TIME ZONE NULL
);

-- Create indexes for better performance
CREATE INDEX ix_claims_case_id ON claims.claims(case_id);
CREATE INDEX ix_claims_claimant_id ON claims.claims(claimant_id);
CREATE INDEX ix_claims_client_id ON claims.claims(client_id);
CREATE INDEX ix_claims_claim_type_id ON claims.claims(claim_type_id);
CREATE INDEX ix_claims_doctor_id ON claims.claims(doctor_id);
CREATE INDEX ix_claims_state_of_loss_id ON claims.claims(state_of_loss_id);
CREATE INDEX ix_claims_deleted ON claims.claims(deleted);
CREATE INDEX ix_claims_created_on ON claims.claims(created_on);

CREATE INDEX ix_claim_documents_claim_id ON claims.claim_documents(claim_id);
CREATE INDEX ix_claim_documents_document_type ON claims.claim_documents(document_type);
CREATE INDEX ix_claim_documents_deleted ON claims.claim_documents(deleted);

CREATE INDEX ix_claim_events_claim_id ON claims.claim_events(claim_id);
CREATE INDEX ix_claim_events_event_type ON claims.claim_events(event_type);
CREATE INDEX ix_claim_events_event_date ON claims.claim_events(event_date);
CREATE INDEX ix_claim_events_deleted ON claims.claim_events(deleted);

CREATE INDEX ix_doctors_last_name ON claims.doctors(last_name);
CREATE INDEX ix_doctors_specialization ON claims.doctors(specialization);
CREATE INDEX ix_doctors_deleted ON claims.doctors(deleted);

CREATE INDEX ix_claim_types_type_name ON claims.claim_types(type_name);
CREATE INDEX ix_claim_types_is_active ON claims.claim_types(is_active);
CREATE INDEX ix_claim_types_deleted ON claims.claim_types(deleted);

CREATE INDEX ix_states_of_loss_state_code ON claims.states_of_loss(state_code);
CREATE INDEX ix_states_of_loss_is_active ON claims.states_of_loss(is_active);
CREATE INDEX ix_states_of_loss_deleted ON claims.states_of_loss(deleted);

-- Insert sample data
INSERT INTO claims.claim_types (type_name, description, created_by_user) VALUES
('Personal Injury', 'Claims related to personal injuries', 1),
('Property Damage', 'Claims related to property damage', 1),
('Medical Malpractice', 'Claims related to medical malpractice', 1),
('Workers Compensation', 'Claims related to workplace injuries', 1);

INSERT INTO claims.states_of_loss (state_name, state_code, created_by_user) VALUES
('California', 'CA', 1),
('Texas', 'TX', 1),
('Florida', 'FL', 1),
('New York', 'NY', 1),
('Illinois', 'IL', 1);

INSERT INTO claims.doctors (first_name, last_name, specialization, license_number, created_by_user) VALUES
('John', 'Smith', 'Orthopedics', 'MD12345', 1),
('Sarah', 'Johnson', 'Neurology', 'MD67890', 1),
('Michael', 'Brown', 'Cardiology', 'MD11111', 1);

-- Insert sample claim
INSERT INTO claims.claims (
    claim_id,
    case_id,
    claim_type_id,
    claimant_id,
    client_id,
    policy_number,
    delta_file_number,
    client_file_number,
    doctor_id,
    state_of_loss_id,
    alleged_injury,
    injury_description,
    attorney_representation,
    liability,
    workers_compensation,
    exposure,
    deleted,
    created_by_user,
    created_on
) VALUES (
    gen_random_uuid(),  -- claim_id (UUID generado automáticamente)
    1001,               -- case_id
    1,                  -- claim_type_id (Personal Injury)
    1,                  -- claimant_id
    1,                  -- client_id
    'POL-2024-001',     -- policy_number
    'DELTA-2024-001',   -- delta_file_number
    'CLIENT-2024-001',  -- client_file_number
    1,                  -- doctor_id (John Smith)
    1,                  -- state_of_loss_id (California)
    'Back injury from car accident',  -- alleged_injury
    'Patient reports severe back pain after rear-end collision',  -- injury_description
    true,               -- attorney_representation
    'At fault driver rear-ended claimant',  -- liability
    false,              -- workers_compensation
    50000.00,           -- exposure
    false,              -- deleted
    1,                  -- created_by_user
    NOW()               -- created_on
);
 
```

## 🔧 Configuration

### Environment Variables

| Variable | Description | Default Value |
|----------|-------------|---------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string | `Host=localhost;Port=5432;Database=claims_db;Username=postgres;Password=password;` |
| `Logging__LogLevel__Default` | Logging level | `Information` |
| `Serilog__MinimumLevel__Default` | Serilog minimum level | `Information` |

### Logging Configuration

The system uses Serilog for structured logging:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/deltaapi-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7
        }
      }
    ]
  }
}
```

## 🧪 Testing

### Run Tests
```bash
dotnet test
```

### Run with Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 📊 Monitoring and Logging

### Structured Logs

The system generates structured logs in JSON format:

```json
{
  "Timestamp": "2024-01-02T16:32:45.123Z",
  "Level": "Information",
  "MessageTemplate": "Creating new claim for case {CaseId}",
  "Properties": {
    "CaseId": 12345,
    "SourceContext": "DeltaApi.API.Controllers.ClaimsController"
  }
}
```

### Log Locations

- **Console**: Standard output during development
- **Files**: `logs/deltaapi-YYYY-MM-DD.txt`

## 🚀 Deployment

### Docker (Coming Soon)

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DeltaApi.API/DeltaApi.API.csproj", "DeltaApi.API/"]
RUN dotnet restore "DeltaApi.API/DeltaApi.API.csproj"
COPY . .
WORKDIR "/src/DeltaApi.API"
RUN dotnet build "DeltaApi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeltaApi.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeltaApi.API.dll"]
```

### Azure (Coming Soon)

- Azure App Service
- Azure SQL Database
- Azure Application Insights

## 🤝 Contributing

### Workflow

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-functionality`)
3. Commit your changes (`git commit -am 'Add new functionality'`)
4. Push to the branch (`git push origin feature/new-functionality`)
5. Create a Pull Request

### Code Standards

- Follow C# (.NET) conventions
- Use Clean Architecture
- Write unit tests
- Document APIs with XML comments
- Use FluentValidation for validations

## 📝 Changelog

### v1.0.0 (2024-01-02)
- ✅ Initial implementation of claims management system
- ✅ Complete Clean Architecture
- ✅ REST API with Swagger
- ✅ PostgreSQL integration
- ✅ Structured logging with Serilog
- ✅ Validation with FluentValidation
- ✅ Global exception handling

## 📞 Support

For technical support or questions:

- **Email**: support@deltaapi.com
- **Issues**: [GitHub Issues](https://github.com/agustinaguirre-jbk/Claim-management/issues)
- **Documentation**: [Project Wiki](https://github.com/agustinaguirre-jbk/Claim-management/wiki)

## 📄 License

This project is under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

**Developed with ❤️ using Clean Architecture and .NET 8**
