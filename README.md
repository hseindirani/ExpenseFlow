# ExpenseFlow API

ExpenseFlow is a backend approval workflow system built with ASP.NET Core (.NET 8).

The system allows employees to submit expenses and managers to approve or reject them.

---

## Features

- Create expense requests
- View submitted expenses
- Approve or reject expenses
- Status transition validation (Pending → Approved/Rejected)
- DTO-based request/response models
- Input validation with Data Annotations
- Proper HTTP status codes (400, 404, 409)

---

## Workflow Rules

- All new expenses start as `Pending`
- Only `Pending` expenses can be approved or rejected
- Approved or Rejected expenses cannot be modified again

---

## Tech Stack

- .NET 8 Web API
- RESTful API design
- Clean separation using DTOs
- In-memory storage (temporary, will be replaced with database)

---

## Next Improvements (Planned)

- Audit logging
- PostgreSQL + EF Core
- JWT authentication & role-based authorization
- Docker containerization
- CI/CD with GitHub Actions
- Azure deployment

---

## How to Run

```bash
dotnet run
