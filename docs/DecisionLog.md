Decision #003

Date: Day 1

Issue:
Application failed at startup with:

System.Reflection.ReflectionTypeLoadException

Root Cause:
Swashbuckle.AspNetCore introduced an OpenAPI assembly/version conflict with the built-in .NET 9 OpenAPI implementation.

Error:

Could not load type:
Microsoft.OpenApi.Models.OpenApiDocument

Resolution:
Temporarily remove/comment out Swashbuckle.AspNetCore package reference.

Current Approach:
Use only Microsoft.AspNetCore.OpenApi until API Versioning and Swagger customization are implemented in a later sprint.

Future Action:
Reintroduce Swagger after:
- API Versioning
- JWT Authentication
- Authorization Policies
- XML Documentation
- Standardized API Responses

Reason:
Avoid startup assembly conflicts during foundation development.