# GenericRepository
This class library implements the Generic Repository Pattern for SQL Server databases. It supports both asynchronous and synchronous operations and is designed to offer a clean and efficient data access layer.



## Key Features : 
-	Separation of Command and Query Repositories: Distinguishes between command (write) and query (read) operations to adhere to the CQRS pattern.
- Cache Repository with Decorator Pattern: Enhances the query repository with caching to improve performance and reduce database load.
- Domain Event Handling: Facilitates decoupled communication and business logic execution based on domain events.
- Audit Logging: Provides comprehensive logging of repository operations for traceability and accountability.
- Outbox Pattern for Reliable Event Processing: Ensures reliable communication and eventual consistency by storing outgoing messages in an “outbox” table, allowing them to be processed in a transactional way to avoid message loss due to failures.

## Components :

- Generic Repository Interface: Defines the standard CRUD methods for data access.
- Command Repository: Manages create, update, and delete operations.
- Query Repository: Manages read operations and is enhanced with caching capabilities.
- Cache Repository: Implements caching for the query repository using the decorator pattern.
- Domain Event Handler: Handles domain events and executes related actions.
- Audit Log Service: Records and manages audit logs for repository interactions.
- Support for Multiple DbContext: Enables the handling of multiple database contexts, allowing flexibility and separation of concerns for different data models.
- Raw SQL Execution: Allows execution of raw SQL commands for advanced query scenarios and performance optimization.
- SQL Transactions: Provides support for executing operations within transactions, ensuring data consistency and integrity during batch operations.
- Outbox Processor: Manages and processes events stored in the outbox, ensuring reliable delivery of events to external systems even in cases of system failure.

## Configuration:
Pass `IServiceCollection` to following method to add services to your DI:
```csharp
GenericRepositoryConfigurator.InjectServices(services);
```
