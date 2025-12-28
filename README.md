# TicketForge

**TicketForge** is a microservices-based project focused on exploring real-world backend architecture patterns, distributed systems concepts, and clean code practices using .NET.

This project is intentionally built in **phases**, focusing on **architecture, communication patterns, and system design** rather than delivering a fully finished product.

---

## 🎯 Project Goals

The main goals of TicketForge are:

- Practice and demonstrate **Microservice Architecture** in .NET
- Apply **Clean Architecture**, **CQRS**, and **DDD-inspired modeling**
- Work with **asynchronous communication**, **event-driven systems**, and **Saga patterns**
- Gain hands-on experience with **testing strategies** (Unit & Integration)

---

## 🧱 Architecture Overview

- **Architecture Style:** Microservices (MonoRepo)
- **Patterns & Concepts:**
  - Clean Architecture (Domain / Application / Infrastructure / API)
  - CQRS (Command & Query separation)
  - DDD-inspired design
  - Saga Pattern (Choreography-based – planned)
- **Communication:**
  - gRPC (sync, internal)
  - RabbitMQ (async, event-based)
- **Database:**
  - MongoDB (local or in-memory for testing)
- **Testing:**
  - Unit Tests (xUnit)
  - Integration Tests (TestServer / WebApplicationFactory)

---

## 🗂️ Repository Structure (MonoRepo)

TicketForge/
├── src/
│ ├── AuthService/
│ ├── CatalogService/
│ ├── ReservationService/
│ └── ApiGateway/
├── shared/
│ └── SharedKernel/
├── tests/
│ ├── AuthService.Tests/
│ ├── CatalogService.Tests/
│ └── ReservationService.Tests/
└── TicketForgeSolution.sln

---

## 🔧 Microservices

### 1. AuthService
- User registration and authentication
- JWT-based authentication
- gRPC communication with API Gateway
- Factory pattern for user creation
- Unit tests for Domain and Application layers

---

### 2. CatalogService
- Ticket inventory management
- MongoDB for data storage
- **CQRS – Query side only**
- MediatR for query handling
- Integration tests for read operations

---

### 3. ReservationService
- Ticket reservation and cancellation
- **CQRS – Command side**
- Proxy pattern for reservation validation
- Publishes domain events to RabbitMQ

---

### 4. API Gateway
- Single entry point for clients
- Routes requests to internal services
- gRPC-based communication

---

## 📌 Project Status

This project is developed incrementally in **phases**.

### ✅ Completed Phases

#### Phase 1 – Core Infrastructure
- MonoRepo setup
- Clean Architecture structure
- AuthService base implementation
- Unit & Integration testing setup

#### Phase 2 – Authentication Service
- Auth domain & application logic
- MediatR-based commands/queries
- gRPC integration
- Unit tests

#### Phase 3 – Catalog Service (Query Side)
- MongoDB integration
- Read-only CQRS implementation
- Integration tests

#### Phase 4 – Reservation Command & Messaging
- Command-side CQRS for reservations
- RabbitMQ integration
- ReservationRequested event
- Unit tests for command logic
---

### 🚧 Planned Phases

#### Phase 5 – Saga & Compensation
- Choreography-based Saga
- Inventory update via events
- Compensation handling on failures
- Integration tests for end-to-end flow

#### Phase 6 – Finalization & Deployment
- API Gateway configuration (YARP / Ocelot)
- Docker & Docker Compose
- Facade & Decorator patterns
- Architecture diagrams & documentation

> Planned phases are intentionally documented to demonstrate architectural thinking and system evolution.

---

## 🧪 Testing Strategy

- **Unit Tests**
  - Domain logic
  - Application command/query handlers
- **Integration Tests**
  - MongoDB read operations
  - Service-level behavior using TestServer

---

## 🚀 Why This Project Exists

TicketForge exists to:
- Show **how I think about backend systems**
- Demonstrate **architecture-first development**
- Practice real-world patterns used in distributed systems

---

## 📎 Notes

- Docker and full infrastructure setup are **intentionally deferred**
- Some services are **partially implemented by design**
- Focus is on **code quality, architecture, and explainability**

---
