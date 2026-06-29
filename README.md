ERP-Lite Enterprise SaaS Starter Kit
Overview

ERP-Lite is an enterprise-grade SaaS starter platform built using modern Microsoft and JavaScript technologies. The project demonstrates real-world software engineering practices including Clean Architecture, RESTful API design, secure authentication, scalable cloud deployment, CI/CD automation, distributed caching, messaging systems, and modern frontend architectures using Angular and React.

The primary objective is to create a production-ready ERP foundation that can serve as a reusable starter kit for future SaaS applications.

```
## Architecture

```text
┌───────────────────────────────┐
│         Angular UI            │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│          React UI             │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│     ASP.NET Core Web API      │
│          (.NET 9)             │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│       Application Layer       │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│         Domain Layer          │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│     Infrastructure Layer      │
│ EF Core | Redis | RabbitMQ    │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│      SQL Server / Azure SQL   │
└───────────────────────────────┘
```
```

**Technology Stack**
Backend
ASP.NET Core 9 Web API
Entity Framework Core 9
SQL Server
Azure SQL Database
Clean Architecture
Repository Pattern
Dependency Injection
RESTful API Design
API Versioning
Global Exception Handling
Structured Logging (Serilog)
Health Checks
JWT Authentication
Refresh Token Architecture
Redis Distributed Cache
RabbitMQ Messaging
SignalR Real-Time Communication

**Frontend (Angular)**
Angular 20
Angular Material
TypeScript
RxJS
Signals
Signal Forms
Reactive Forms
Route Guards
HTTP Interceptors

**Frontend (React)**
React
TypeScript
Redux Toolkit
React Query
Axios
Material UI / Tailwind CSS
Protected Routes
State Management


**DevOps & Cloud**
Azure App Service
Azure Static Web Apps
Azure Blob Storage
Azure Key Vault
Azure SQL Database
GitHub Actions
CI/CD Pipelines
Docker Ready Architecture


**Key Features**
Enterprise Architecture
Clean Architecture
Separation of Concerns
SOLID Principles
Dependency Injection
Repository Pattern
Service Layer Pattern
API Design
RESTful Endpoints
Versioned APIs
Standardized API Response Contracts
Standardized Error Contracts
OpenAPI Documentation
Health Check Endpoints

**Security**
JWT Authentication
Refresh Token Rotation
Role-Based Authorization
Policy-Based Authorization
Secure Configuration Management
CORS Configuration
Rate Limiting

**Data Management**
One-to-One Relationships
One-to-Many Relationships
Many-to-Many Relationships
Code-First Migrations
Audit Logging
Soft Delete Support
File Management
Single File Upload
Multiple File Upload
Image Upload
Document Management
Azure Blob Storage Integration
Pre-Signed Upload URLs
Performance
Redis Caching
Pagination
Query Optimization
Asynchronous Processing
Background Jobs
Real-Time Features
SignalR Notifications
Live Dashboard Updates
Activity Feed
Real-Time Task Updates

**Repository Structure**

erp-lite-starter-kit
│
├── backend
│   ├── ERP-Lite.sln
│   │
│   ├── src
│   │   ├── ERPLite.API
│   │   ├── ERPLite.Application
│   │   ├── ERPLite.Domain
│   │   └── ERPLite.Infrastructure
│   │
│   └── tests
│       ├── ERPLite.UnitTests
│       └── ERPLite.IntegrationTests
│
├── frontend-angular
│
├── frontend-react
│
├── docs
│
├── deployment
│
└── scripts



**Current Modules**
Implemented
Organization Management
API Versioning
Global Exception Handling
Standardized API Responses
Health Check Endpoint
OpenAPI Documentation
Repository Layer
Service Layer

**Planned**
Identity & Access Management
Employee Management
Department Management
Asset Management
Inventory Management
Document Management
Media Management
Project & Task Management
Dashboard & Analytics
Notification System
