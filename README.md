🏦 Banking API (Clean Architecture + CQRS + EF Core + SQL Server)
=================================================================

A production-style banking service demonstrating how to design and implement a clean, scalable, testable .NET application.  
This project intentionally models **real enterprise architecture**, including proper layering, CQRS, idempotency, structured logging, global exception handling, and prepared extensibility for messaging and distributed systems. 

* * *
 
🚀 Tech Stack
=============

**Backend**
*   .NET 10 Web API
    
*   ASP.NET Core
    
*   C# 12
    
*   EF Core 10 (SQL Server Provider)
    
*   FluentValidation
    
*   Custom CQRS (Commands/Queries + Dispatchers)
    
*   Native DI
    
**Database**
*   SQL Server
    
*   EF Core Migrations
    
**Cross-Cutting**
*   Serilog / ILogger (structured logging)
    
*   Global Exception Handling (middleware)
    
*   Idempotency Middleware (financial safety)
    
**Testing**
*   xUnit
    
*   WebApplicationFactory
    
    

* * *

📁 Solution Structure
=====================

`BankingApp/   BankingApp.Domain/   BankingApp.Application/   BankingApp.Infrastructure/   BankingApp.Web/   BankingApp.Web.Tests/`

This solution follows **Clean Architecture** with strict separation of concerns.

* * *

🧱 Architecture Overview
========================

`[ Web API ] → [ Controllers ] → [ Dispatchers ] → [ CQRS Handlers ] → [ Repositories ] → [ SQL Server ]`

### Layer Responsibilities

**Banking.Web (Presentation)**
*   Controllers
    
*   DTO mapping
    
*   Middleware (Error + Idempotency)
    
*   DI configuration
    
**Banking.Application (Use Cases)**
*   Commands & Queries
    
*   Handlers (business rules)
    
*   Repository interfaces
    
*   Validators
    
*   DTOs
    
**Banking.Infrastructure (Technical)**
*   EF Core DbContext
    
*   SQL Server persistence
    
*   Repository implementations
    
*   Outbox pattern setup
    
*   Idempotent request storage
    
**Banking.Domain (Enterprise Core)**
*   Entities (Account, Customer)
    
*   Enums (AccountType, AccountStatus)
    
*   Pure business logic / no external dependencies
    

* * *

🧩 Domain Model
===============

### Entities

*   `Account`
    
*   `Customer`
    

### Enums

*   `AccountStatus` → Open / Closed
    
*   `AccountType` → Checking / Savings
    
Domain layer contains **no infrastructure**, **no EF**, **no HTTP**, **no controllers**—pure business structures.

* * *

🧠 Application Layer (CQRS)
===========================

### Commands

*   `DepositCommand`
    
*   `WithdrawCommand`
    
*   `CloseAccountCommand`
    
*   `CreateAccountCommand`
    

### Queries

*   `GetAccountByIdQuery`
    

### Handlers (Business Logic)

Implements all financial rules:
*   Deposit must be > 0
    
*   Withdraw cannot overdraw
    
*   Close requires balance = 0
    
*   First account must be Savings
    
*   Account must belong to the customer
    
*   Account must be Open
    

### Repository Interfaces

*   `IAccountRepository`
    
*   `ICustomerRepository`
    

### Dispatchers

*   `CommandDispatcher`
    
*   `QueryDispatcher`
    
These resolve handlers dynamically from DI—no external libraries required.

* * *

🗄️ Infrastructure Layer
========================

### EF Core & SQL Server

`BankingDbContext` exposes:
*   `DbSet<Account>`
    
*   `DbSet<Customer>`
    
*   `DbSet<OutboxMessage>`
    
*   `DbSet<IdempotentRequest>`
    

### Repository Implementations

*   `EfAccountRepository`
    
*   `EfCustomerRepository`
    

### Outbox Pattern (Enterprise-Ready)

The infrastructure layer includes:
*   `OutboxMessage` entity
    
*   Will enable reliable event publishing (ServiceBus / EventGrid)
    

### Idempotency System

*   `IdempotencyMiddleware`
    
*   Prevents double deposits/withdrawals
    
*   Uses `IdempotentRequests` table to store request/response pairs
    

* * *

🌐 Web Layer
============

### Controllers

`AccountsController` exposes:

| Endpoint | Description |
| --- | --- |
| POST `/deposit` | Deposit money |
| POST `/withdraw` | Withdraw funds |
| POST `/close` | Close account |
| POST `/create` | Create account |
| GET `/{id}` | Get account details |

Controllers are intentionally thin—only mapping and HTTP concerns.

### Middleware

**ErrorHandlingMiddleware**
*   Logs with correlation ID
    
*   Returns RFC 7807 ProblemDetails
    
**IdempotencyMiddleware**
*   Enforces safe write operations
    
*   Handles duplicate requests gracefully
    

* * *

🗃️ Database Schema
===================

### Accounts

`AccountId (PK) CustomerId (FK) Balance decimal(18,2) AccountType int Status int`

### Customers

`CustomerId (PK) FullName nvarchar(200)`

### OutboxMessages

`Id (PK) OccurredOnUtc Type Payload ProcessedOnUtc Error`

### IdempotentRequests

`IdempotencyKey (PK) RequestHash ResponseBody StatusCode CreatedOnUtc`

* * *

🧪 Testing
==========

### Integration Tests

Using `WebApplicationFactory<Program>`:
*   Deposit success
    
*   Withdraw insufficient funds
    
*   Idempotency rerun returns cached response
    
*   Close account with 0 balance
    
*   Getting seeded account
    
Tests run against a real, in-memory pipeline with controllers/middleware.

### Performance Tests (k6)

*   Stress-test deposits
    
*   Unique idempotency keys
    
*   Measures throughput and correctness
    

* * *

▶️ Running the Application
==========================

### 1. Apply migrations (creates the DB)

`dotnet ef migrations add InitialCreate -p BankingApp.Infrastructure -s BankingApp.Web dotnet ef database update -p BankingApp.Infrastructure -s BankingApp.Web`

### 2. Run the API

`dotnet run --project BankingApp.Web`

Swagger at:
**https://localhost:7007/swagger**

* * *

🔮 Future Enterprise Enhancements (Next Steps)
==============================================

This project intentionally leaves room for enterprise-grade evolution:

### ✔️ Domain Events + Outbox Processor

Background service to publish events to:
*   RabbitMQ
    
*   Azure Service Bus
    
*   EventGrid
    

### ✔️ Authentication / Authorization

*   JWT auth
    
*   Roles (`Customer`, `Teller`, `Admin`)
    

### ✔️ Observability

*   OpenTelemetry tracing
    
*   Centralized logging (ELK, Seq, Datadog)
    
*   Metrics + dashboards
    

### ✔️ API Versioning

*   `/api/v1/accounts`
    
*   `/api/v2/accounts`
    

### ✔️ Caching

*   Redis for read models
    
*   Cache invalidation via events
    

### ✔️ Resilience Patterns

*   Polly retry
    
*   Circuit breaker
    
*   Timeout strategies
    

### ✔️ DDD Enhancements

*   Value objects (Money, AccountNumber)
    
*   Aggregates & invariants
    
*   Specifications
    

### ✔️ Microservices or Modular Monolith Ready

*   Event-driven integration via Outbox
    
*   Clear bounded contexts
    
*   Easy separation into services if needed

### ✔️ Performance/ Load Testing
    
*   k6 or similar (load testing)

* * *

🎯 Summary
==========

This solution provides a **real-world architectural foundation** for financial or transactional systems.  
It demonstrates:
*   Proper separation of concerns
    
*   CQRS done cleanly without external libraries
    
*   Safe financial operations via idempotency
    
*   Real testing strategy
    
*   SQL-backed reliability
    
*   Enterprise extensibility
    
It’s intentionally built small, but behaves like a production-grade, scalable banking service.
