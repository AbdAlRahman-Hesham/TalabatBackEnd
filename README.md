---

## Talabat BackEnd API

# E-Commerce API

A comprehensive e-commerce API built with ASP.NET Core, implementing clean architecture patterns and featuring Redis caching and Stripe payment integration.

## Project Overview

This e-commerce solution is built using a clean onion architecture approach with several design patterns to ensure maintainability, scalability, and separation of concerns. The API provides endpoints for managing accounts, products, orders, baskets, and payments within an e-commerce ecosystem.

## Project Structure
The project is divided into several layers:
```
E-Commerce
├── E-Commerce.APIs            # API Layer (Controllers, Middlewares, Extensions)
├── E-Commerce.Domain          # Domain Entities & Service Interfaces
├── E-Commerce.DTOs            # Data Transfer Objects (DTOs)
├── E-Commerce.HELPERs         # Helper classes and utilities
├── E-Commerce.Repository      # Data Access Layer (Repositories, Migrations, Configurations)
├── E-Commerce.Services        # Business Logic Layer (Service Implementations)
```

## Features
- **User Authentication & Authorization**
- **Product & Inventory Management**
- **Order Processing & Payment Handling**
- **Basket Management**
- **Pagination & Filtering**
- **Data Seeding for Initial Setup**
- **Middleware for Exception Handling & Logging**
- **Caching with Redis for Improved Performance**

## Tech Stack
- **Backend:** ASP.NET Core 9, Entity Framework Core
- **Database:** SQL Server
- **Caching:** Redis
- **Authentication:** JWT Authentication
- **Design Patterns:** Repository Pattern, Unit of Work, Specification Pattern

## Architecture

The project follows onion architecture principles with the following layers:

### E-Commerce.API
The presentation layer containing API controllers and endpoints:
- AccountController
- BaseApiController
- BasketController
- BuggyController
- ErrorsController
- OrderController
- PaymentController
- ProductsController

### E-Commerce.Domain
The core domain layer with business entities.

### E-Commerce.DTOs
Data transfer objects for communication between layers.

### E-Commerce.HELPERs
Helper classes and utility functions.

### E-Commerce.Repository
Data access layer implementing Repository Pattern and Specification Pattern:
- Repositories (Generic and specific implementations)
- Repository Interfaces
- Specifications for querying data
- Unit of Work implementation

### E-Commerce.Service
Business logic layer with various services:
- AuthServices
- CacheServices (Redis implementation)
- OrderServices
- PaymentServices (Stripe integration)
- ProductServices

## Key Features

### Caching with Redis
The application leverages Redis for caching frequently accessed data to improve performance. Caching behavior is controlled through custom attributes.

```csharp
// Example of cache attribute usage
[CacheData(timeLifeInSeconds: 30)]
public async Task<ActionResult<ProductDto>> GetProduct(int id)
{
    // Method implementation
}
```

### Payment Processing with Stripe
Secure payment processing is implemented using Stripe's API, including webhook support for event handling.

### Repository Pattern
Data access is abstracted through repositories, providing a clean separation between business logic and data access.

### Specification Pattern
Custom query specifications for filtering, sorting, and pagination of data.

### Unit of Work Pattern
The Unit of Work pattern ensures transaction integrity when working with multiple repositories.

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Redis server
- SQL Server (or your preferred database)
- Stripe account and API keys

### Configuration
1. Update the connection strings in `appsettings.json`
2. Configure Redis connection settings
3. Add your Stripe API keys to the configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=ECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true",
  },
  "Stripe": {
    "PublishableKey": "your-publishable-key",
    "SecretKey": "your-secret-key",
  }
}
```

### Running the Application
```bash
# Clone the repository
git clone https://github.com/yourusername/e-commerce-api.git

# Navigate to the API project
cd e-commerce-api/E-Commerce.APIs

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update

# Run the application
dotnet run
```

## API Endpoints

### Accounts
- `POST /api/Accounts/Login` - User login
- `POST /api/Accounts/Register` - User registration
- `GET /api/Accounts` - Get current user information
- `GET /api/Accounts/address` - Get user's address
- `PUT /api/Accounts/address` - Update user's address
- `GET /api/Accounts/emailexists` - Check if email is already registered

### Basket
- `GET /api/Basket` - Get current user's basket
- `POST /api/Basket` - Create or update basket
- `DELETE /api/Basket` - Delete basket

### Orders
- `GET /api/Orders/deliveryMethods` - Get available delivery methods
- `GET /api/Orders` - Get user's orders
- `POST /api/Orders` - Create a new order
- `GET /api/Orders/{id}` - Get order by ID

### Payments
- `POST /api/Payments/{basketId}` - Process payment with Stripe
- `POST /webhook` - Stripe webhook endpoint for event processing

### Products
- `GET /api/Products/brands` - Get all product brands
- `GET /api/Products/categories` - Get all product categories
- `GET /api/Products` - Get all products (with filtering, sorting, and pagination)
- `GET /api/Products/{id}` - Get product by ID

## Data Models

### User & Account
- `Address` - User's shipping address
- `AddressDto` - DTO for user's address
- `UserDto` - User information DTO
- `RegisterDto` - Registration request DTO
- `LoginDto` - Login request DTO
- `UserAddressDto` - User's address DTO

### Product
- `Product` - Product entity
- `ProductBrand` - Product brand entity
- `ProductCategory` - Product category entity

### Basket
- `UserBasket` - User's shopping basket
- `UserBasketDto` - Shopping basket DTO
- `BasketItem` - Item in basket
- `BasketItemDto` - Basket item DTO

### Order
- `DeliveryMethod` - Delivery method entity
- `OrderDto` - Order DTO for creation
- `OrderToReturnDto` - Order information DTO
- `OrderItemToReturn` - Order item DTO

### API Response
- `ApiResponse` - Standard API response
- `ApiValidationResponse` - Validation error response

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## License

This project is licensed under the MIT License.

---

Made with ❤️ by Abd Al Rahman Hesham 

---
