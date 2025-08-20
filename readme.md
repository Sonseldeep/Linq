# Ecommerce.API

A simple ASP.NET Core Web API for managing products and categories in an e-commerce system.  
Built with Entity Framework Core and LINQ.

## Features

- CRUD operations for Products
- Products belong to Categories (one-to-many relationship)
- Uses Entity Framework Core with code-first migrations
- LINQ-based filtering, searching, sorting, and pagination

## Entity Relationships

- **Product**
  - Primary Key: `Id` (Guid)
  - Foreign Key: `CategoryId` (Guid)
  - Navigation: Each product belongs to one category

- **Category**
  - Primary Key: `Id` (Guid)
  - Navigation: Each category can have many products

## Restor Dependencies
```bash
     dotnet restore
   dotnet tool restore
```

## After this, 
```bash
   dotnet ef migrations add InitialMigration
   dotnet ef database update
```
## API Endpoints

- `GET /api/products`  
  List products with optional filtering by category, search, sorting by price, and pagination.

- `GET /api/products/{id}`  
  Get a single product by ID.

- `POST /api/products`  
  Create a new product.

- `PUT /api/products/{id}`  
  Update an existing product.

- `DELETE /api/products/{id}`  
  Delete a product.

## Technologies

- ASP.NET Core
- Entity Framework Core
- LINQ
- SQL Server

## Usage

1. Update your connection string in `appsettings.json`.
2. Run database migrations:
3. Start the API


## Notes

- Decimal precision for product price is set using EF Core attributes.
- All queries use LINQ for filtering and sorting.
