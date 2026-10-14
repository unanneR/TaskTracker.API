# TaskTracker API

A small ASP.NET Core Web API for creating and managing project tasks.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Run locally

```powershell
dotnet restore
dotnet run
```

The development profile listens on `http://localhost:5234`. Swagger UI is
available at `http://localhost:5234/swagger` while running in the Development
environment.

## API endpoints

All task endpoints are rooted at `/api/ProjectTasks`.

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/api/ProjectTasks` | List all tasks. |
| `GET` | `/api/ProjectTasks/{id}` | Get one task. |
| `POST` | `/api/ProjectTasks` | Create a task. |
| `PUT` | `/api/ProjectTasks/{id}` | Update a task. |
| `DELETE` | `/api/ProjectTasks/{id}` | Delete a task. |

### Example: create a task

```powershell
$task = @{ name = 'Prepare sprint plan'; isComplete = $false } | ConvertTo-Json
Invoke-RestMethod -Method Post `
  -Uri 'http://localhost:5234/api/ProjectTasks' `
  -ContentType 'application/json' `
  -Body $task
```

### Example: mark a task complete

```powershell
$task = @{ id = 1; name = 'Prepare sprint plan'; isComplete = $true } | ConvertTo-Json
Invoke-RestMethod -Method Put `
  -Uri 'http://localhost:5234/api/ProjectTasks/1' `
  -ContentType 'application/json' `
  -Body $task
```

## Data storage

The API is configured to use Entity Framework Core's in-memory database
(`TaskTrackerList`). Data is kept only for the lifetime of the running
application and is cleared when the application stops.

## Other endpoint

`GET /weatherforecast` returns the starter weather forecast sample.
