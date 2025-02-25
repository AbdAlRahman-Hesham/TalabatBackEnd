---

## Talabat BackEnd API

## Overview

This is a fully functional E-Commerce application built using ASP.NET Core and follows a clean architecture. The project is modular, with separate layers for APIs, domain entities, data repositories, services, and DTOs. The system is designed to support user authentication, product management, order processing, and more.

## Tech Stack

- **Backend:** ASP.NET Core 9, Entity Framework Core
- **Database:** SQL Server
- **Caching:** Redis
- **Authentication:** JWT Authentication
- **Design Patterns:** Repository Pattern, Unit of Work, Specification Pattern

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

## Installation & Setup

### Prerequisites

Ensure you have the following installed:

- .NET SDK 9.0
- SQL Server
- Redis Server
- Entity Framework Core

### Data Seeding

The project comes with predefined data for orders, users, and products. To apply the seed data, ensure you run the database migration command:

```sh
dotnet ef database update
```

Got it! Let's update the `README.md` file to include the updated API endpoints. Here's the revised section for the **API Endpoints**:

---

## API Endpoints

The API exposes multiple endpoints for various functionalities:

### Authentication

- `POST /api/account/login` - User login
- `POST /api/account/register` - User registration

### Products

- `GET /api/products` - Get all products (supports pagination and filtering)
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create a new product (Admin only)
- `PUT /api/products/{id}` - Update a product (Admin only)
- `DELETE /api/products/{id}` - Delete a product (Admin only)

### Orders

- `POST /api/orders` - Place an order
- `GET /api/orders/{userId}` - Get orders for a specific user
- `GET /api/orders/{orderId}` - Get order details by order ID
- `PUT /api/orders/{orderId}` - Update order status (Admin only)

### Basket

- `POST /api/basket` - Add items to the basket
- `GET /api/basket` - Retrieve basket contents
- `DELETE /api/basket/{itemId}` - Remove an item from the basket
- `PUT /api/basket/{itemId}` - Update the quantity of an item in the basket

### Users

- `GET /api/users` - Get all users (Admin only)
- `GET /api/users/{userId}` - Get user details by ID
- `PUT /api/users/{userId}` - Update user details
- `DELETE /api/users/{userId}` - Delete a user (Admin only)

### Categories

- `GET /api/categories` - Get all product categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create a new category (Admin only)
- `PUT /api/categories/{id}` - Update a category (Admin only)
- `DELETE /api/categories/{id}` - Delete a category (Admin only)

### Payments

- `POST /api/payments` - Process a payment for an order
- `GET /api/payments/{paymentId}` - Get payment details by ID

### Caching

- `GET /api/cache/clear` - Clear the Redis cache (Admin only)

---

## License

This project is licensed under the MIT License.

---

Made with ❤️ by Abd Al Rahman Hesham

---
