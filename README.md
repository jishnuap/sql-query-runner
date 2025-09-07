# SQL Query Runner Azure Function (.NET 8)

This project is an Azure Function App built with .NET 8 that exposes an HTTP endpoint for running SQL queries against a SQL Server database. It is designed for integration with AI agents or other automated systems.

## Features
- HTTP-triggered Azure Function
- Accepts SQL queries via POST requests
- Connects to SQL Server using a connection string from configuration
- Returns query results or error details as JSON

## Getting Started

### Prerequisites
- .NET 8 SDK
- Azure Functions Core Tools
- SQL Server instance

### Local Development
1. Clone the repository:
   ```bash
   git clone https://github.com/jishnuap/sql-query-runner.git
   cd sql-query-runner
   ```
2. Set the SQL Server connection string in `local.settings.json`:
   ```json
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet",
       "SqlConnectionString": "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
     }
   }
   ```
3. Run the function locally:
   ```bash
   func start
   ```

### Usage
Send a POST request to the function endpoint with a JSON body:
```json
{
  "query": "SELECT TOP 10 * FROM YourTable"
}
```
Response will be JSON with either results or error details.

### Deployment
Follow [Azure Functions deployment documentation](https://learn.microsoft.com/en-us/azure/azure-functions/functions-deployment-technologies) to deploy to Azure.

## License
MIT

---
For more details, see the source code and comments.
