# SmartEventAPI

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black)

A RESTful **ASP.NET Core Web API** for an event booking system. It manages users, events and seat bookings, and exposes simple admin statistics. It is the backend for [SmartEventMVC](https://github.com/ShaileshXB/SmartEventMVC).

## Features

- **Users**: register and log in with email and password; each user gets a role (`User` by default).
- **Events**: full CRUD (name, date, location, description, price, available seats).
- **Bookings**: book 1 to 10 seats for an event. The API checks seat availability, calculates the total (`price × seats`), and reduces the event's seat count. Cancelling a booking returns the seats.
- **Admin dashboard data**: total bookings, total revenue, user count and event count in one call.
- Validation with data annotations and DTOs, and Swagger UI in development.

## Tech stack

| Layer | Technology |
| --- | --- |
| Framework | ASP.NET Core 8 Web API |
| Language | C# |
| ORM | Entity Framework Core 8 (code-first migrations) |
| Database | Microsoft SQL Server |
| API docs | Swashbuckle (Swagger / OpenAPI) |

## API endpoints

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/User/register` | Register a new user |
| `POST` | `/api/User/login` | Log in; returns `userId`, `name`, `role` |
| `GET` | `/api/Events` | List all events |
| `GET` | `/api/Events/{id}` | Get one event |
| `POST` | `/api/Events` | Create an event |
| `PUT` | `/api/Events/{id}` | Update an event |
| `DELETE` | `/api/Events/{id}` | Delete an event |
| `POST` | `/api/Booking` | Book seats (`eventId`, `userId`, `seats`) |
| `GET` | `/api/Booking/user/{userId}` | List a user's bookings |
| `DELETE` | `/api/Booking/{id}` | Cancel a booking and release its seats |
| `GET` | `/Display` | Admin stats: bookings, total amount, users, events |

## Data model

- **User**: `Id`, `Name`, `Email`, `Password`, `Role`
- **Event**: `EventId`, `EventName`, `Date`, `Location`, `Description`, `Price`, `Seat`
- **Booking**: `BookingId`, `EventId`, `UserId`, `Seats`, `BookingDate`, `TotalAmount`

A user has many bookings, and an event has many bookings.

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express or full)
- EF Core CLI: `dotnet tool install --global dotnet-ef`

### 1. Configure the database

Set the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SmartEventTbl;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2. Restore, migrate and run

```bash
git clone https://github.com/ShaileshXB/SmartEventAPI.git
cd SmartEventAPI
dotnet restore
dotnet ef database update
dotnet run --launch-profile https
```

The API runs at `https://localhost:7181` (HTTP: `http://localhost:5290`). Swagger UI is at `https://localhost:7181/swagger`.

## Project structure

```
SmartEventAPI/
├── Controllers/
│   ├── AdminController.cs     # admin statistics
│   ├── BookingController.cs   # create, list and cancel bookings
│   ├── EventController.cs     # event CRUD
│   └── UserController.cs      # register and login
├── Data/
│   └── AppDbContext.cs        # EF Core DbContext
├── Migrations/                # EF Core migrations
├── Models/                    # User, Event, Booking and their DTOs
├── Properties/launchSettings.json
├── Program.cs                 # service and middleware setup
├── SmartEventAPI.http         # sample requests
└── appsettings.json
```

## Roadmap

- Hash passwords and add JWT authentication
- Role-based authorization on admin endpoints
