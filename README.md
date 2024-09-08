Choose a `ASP.Net Core Web API` project in vs code
### Way to create a controller in VS
	- r.c on Controller -> Add -> Controller -> API -> API Controller -> GiveFileName
### With EF Core
- create a controller
- create AppDbContext
- create a model
- create a connection_strings
### With Dapper 
- create a controller
- create AppDbContext
- create a model
- create a connection_strings
- install `Dapper` from NuGet


# Applying N-Layer Architecture
It refers to a software design pattern that separates an application into distinct layers, each responsible for different aspects of the application. It provides a clean, organized, and scalable design.
- UI Layer: Displays product info and takes user inputs.
- BLL: Validates the request and applies business rules.
- DAL: Fetches or updates the product info from the database.
- Entity: Represents the product data (e.g., Product class).
