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

### Logging with Serilog:
Serilog is used for logging to track application events and errors.

### Exception Handling:
Exception handling is implemented primarily at the controller level to provide meaningful error responses to clients.
Since this is single service and all errors could be handled at controller, Global error handling is implemented.

### API Versioning:
Versioning is implemented, the current API Version is v1.

### Swagger Documentation:
Swagger is integrated to provide interactive API documentation.
Allows users to explore and test API endpoints with ease.

### Input Validation
Input validation is implemented to restrict the size of the request and to ensure that the request is valid.

### Rate limit
A fixed window rate limiter policy is applied, allowing 10 requests per second.

## Key Components:

### MathController (Presentation Layer):
Exposes API endpoints for expression evaluation.
Handles input validation and response formatting.
Includes support for API versioning and Swagger documentation.

### MathService (Application Layer):
Implements the core business logic for expression evaluation.

### CacheService (Infrastructure Layer):
Manages an in-memory cache for storing and retrieving previously evaluated results.


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
