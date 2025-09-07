# GitHub Copilot Repository Instructions

These instructions help GitHub Copilot provide better code suggestions for this repository.

## Project Purpose
This repository contains an Azure Function App (.NET 8) that exposes an HTTP endpoint for running SQL queries against a SQL Server database. It is intended for use by AI agents or automated systems to execute queries and retrieve results or errors as JSON.

## Coding Guidelines
- Use C# and .NET 8 for all Azure Function code.
- Use `System.Data.SqlClient` or `Microsoft.Data.SqlClient` for SQL Server connectivity.
- Store the database connection string in configuration (`local.settings.json` for local, Azure App Settings for cloud).
- The main function should be HTTP-triggered and accept a JSON body with a `query` property.
- Return results as JSON. If an error occurs, return a JSON object with error details.
- Follow best practices for error handling and input validation.

## File Structure
- `README.md`: Project overview and setup instructions
- `.github/copilot-repository-instructions.md`: Copilot custom instructions
- `local.settings.json`: Local configuration (not committed)
- `src/`: Source code for Azure Function

## Example Usage
POST request body:
```json
{
  "query": "SELECT TOP 10 * FROM YourTable"
}
```

## References
- [Azure Functions documentation](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [GitHub Copilot repository instructions](https://docs.github.com/en/copilot/how-tos/configure-custom-instructions/add-repository-instructions)
