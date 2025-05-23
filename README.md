# 🛒 E-Commerce API (.NET Core)

A RESTful API built with ASP.NET Core for an e-commerce system. It includes user authentication, product and category management, order processing, favorites, and product photos.

---

## 🚀 Features

- 🔐 **Authentication** (JWT)
- 📁 **Category Management**
- 🛍️ **Product Management** (with photo uploads)
- ❤️ **Favorites System**
- 📦 **Order Processing**

---

## 🛠️ Built With

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- Swagger (OpenAPI)

---

## 📂 API Modules

### 🔐 Authentication
- Register new users.
- Login to receive JWT token.
- Role-based authorization.

### 📁 Categories
- Create, update, and list product categories.

### 🛍️ Products
- Add, update, delete, and retrieve products.
- Upload and manage product photos.

### ❤️ Favorites
- Add/remove products to/from user's favorites.
- Get all favorite products for the authenticated user.

### 📦 Orders
- Place orders with product list and quantities.
- Retrieve user-specific and admin-wide order histories.

---

## 🔧 Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server
- Visual Studio
- Postman or Swagger for testing

### Run the API

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/your-repo-name.git
   cd your-repo-name
