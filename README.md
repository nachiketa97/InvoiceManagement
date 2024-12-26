Invoice Management API

This .NET Core Web API provides a foundation for managing invoices, including core CRUD operations (Create, Read, Update, Delete), basic validation, and data mapping.

Key Features:

Core Functionality:
Create invoices: Create new invoices with associated items.
Retrieve invoices:
Get a list of all invoices.
Retrieve a single invoice by ID.
Update invoices: Modify existing invoice details.
Delete invoices: Soft-delete invoices (mark them as deleted instead of physically removing them from the database).
Data Validation:
Basic data validation for invoice and invoice item properties using FluentValidation.
Data Mapping:
Utilize AutoMapper for efficient object mapping between DTOs (Data Transfer Objects) and domain models.
Technical Stack:

.NET Core: A modern, high-performance, and cross-platform framework for building web applications.
Entity Framework Core: An Object-Relational Mapper (ORM) for interacting with the database.
SQL Server: (Assumed database)
FluentValidation: Enforces data integrity and consistency.
AutoMapper: Simplifies object mapping between different classes.
Getting Started:

Clone the repository: git clone <repository_url>
Restore dependencies: dotnet restore
Configure the connection string:
Update the ConnectionStrings section in appsettings.json with your SQL Server connection details.
Run the application: dotnet run
API Endpoints:

GET /invoices: Get a list of all invoices.
GET /invoices/{id}: Get a single invoice by ID.
POST /invoices: Create a new invoice.
PUT /invoices/{id}: Update an existing invoice.
DELETE /invoices/{id}: Soft-delete an invoice.
