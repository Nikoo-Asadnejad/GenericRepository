# GenericRepository
This class library implements the Generic Repository Pattern for SQL Server databases. It supports both asynchronous and synchronous operations and is designed to offer a clean and efficient data access layer.



## Key Features : 
-	Separation of Command and Query Repositories: Distinguishes between command (write) and query (read) operations to adhere to the CQRS pattern.
- Cache Repository with Decorator Pattern: Enhances the query repository with caching to improve performance and reduce database load.
- Domain Event Handling: Facilitates decoupled communication and business logic execution based on domain events.
- Audit Logging: Provides comprehensive logging of repository operations for traceability and accountability.

## Components :

- Generic Repository Interface: Defines the standard CRUD methods for data access.
- Command Repository: Manages create, update, and delete operations.
- Query Repository: Manages read operations and is enhanced with caching capabilities.
- Cache Repository: Implements caching for the query repository using the decorator pattern.
- Domain Event Handler: Handles domain events and executes related actions.
- Audit Log Service: Records and manages audit logs for repository interactions.

## Configuration:
Pass `IServiceCollection` to following method to add services to your DI:
```csharp
GenericRepositoryConfigurator.InjectServices(services);
```
