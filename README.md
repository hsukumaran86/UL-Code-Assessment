## Problem Statement:
Create a web API that evaluates mathematical expressions, supporting basic operations (addition, subtraction, multiplication, division) without parentheses.

## Key Design Decisions:

### Clean Architecture Pattern:
The project is structured following the Clean Architecture pattern.
Separation of concerns is achieved through distinct layers: Presentation, Application, Domain, and Infrastructure.

### Application Layer:
Logic for evaluating mathematical expressions is implemented in the Application layer.
Receives input expressions and returns results.
Utilizes a service for expression evaluation.

### Infrastructure Layer:
In-Memory caching for results is handled in the Infrastructure layer.
Caching is employed to improve response times by storing previously evaluated expressions.

### Logging with Serilog:
Serilog is used for logging to track application events and errors.
Log files provide insights into system behavior.

### Exception Handling:
Exception handling is implemented to provide meaningful error responses to clients.
Ensures graceful degradation of service in the face of exceptions.

### API Versioning:
Versioning is implemented to manage backward compatibility with existing clients.
Supports multiple API versions to accommodate changes in future releases.

### Swagger Documentation:
Swagger is integrated to provide interactive API documentation.
Allows users to explore and test API endpoints with ease.

## Key Components:

### MathController (Presentation Layer):
Exposes API endpoints for expression evaluation.
Handles input validation and response formatting.
Includes support for API versioning and Swagger documentation.

### MathService (Application Layer):
Evaluates mathematical expressions using a custom evaluator.
Implements the core business logic for expression evaluation.

### CacheService (Infrastructure Layer):
Manages an in-memory cache for storing and retrieving previously evaluated results.
Enhances response times by avoiding redundant evaluations.

## Testing 

### Unit Testing:
Unit tests ensure the correctness of the application and infrastructure layer's logic.
Validate mathematical expression evaluation.
validate the cache functions

### Integration Testing:
Integration tests validate the functionality of the MathController.
Ensure that the API endpoints function as expected.

## Future Extensions:

### Advanced Operators:
Extend expression evaluation to support more complex mathematical operators and functions.

### External Databases:
Implement caching with external databases for scalability and persistence.

### User Authentication:
Integrate user authentication and authorization to secure the API.

### Monitoring and Analytics:
Incorporate monitoring and analytics tools to gain insights into API usage and performance.
