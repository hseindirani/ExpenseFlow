# ExpenseFlow API

ExpenseFlow is a secure approval workflow backend built with ASP.NET Core (.NET 8).
Employees submit expenses and managers approve or reject them with a full audit trail.

---

## Features

Expense submission and listing

Approve / Reject workflow with enforced state rules
(Pending → Approved or Pending → Rejected)

Audit logging (Created, Approved, Rejected) including PerformedBy

JWT authentication with role-based authorization
(Employee, Manager, Admin-ready)

Health check endpoint: /health

PostgreSQL persistence using EF Core + migrations

Unit tests (xUnit)

GitHub Actions CI (build + test on every push/PR)

Fully containerized with Docker Compose (API + PostgreSQL)

---

## Architecture Overview

Controllers handle HTTP concerns

Business rules live in ExpenseWorkflowService

EF Core handles data persistence

JWT handles authentication and role authorization

Health checks expose infrastructure readiness

CI ensures build + test validation on every commit

---

## Tech Stack

- .NET 8 Web API
- EF Core
- PostgreSQL
- JWT Bearer Authentication
- xUnit
- Docker / Docker Compose
- GitHub Actions

---

## Run with Docker
```bash
docker compose up --build

---


