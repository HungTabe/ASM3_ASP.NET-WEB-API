```markdown
# ASM3_ASP.NET WEB API Backend

## Overview
This is the backend for the **FUNewsManagementSystem**, a News Management System built for universities to manage news articles, categories, and accounts. The backend is developed using **ASP.NET Core Web API** with **OData**, **JWT authentication**, and **Entity Framework Core** to interact with a Microsoft SQL Server database. It follows a **3-layer architecture** and implements **Repository** and **Singleton** patterns.

## Features
- **CRUD Operations**: Create, Read, Update, Delete, and Search for accounts, categories, and news articles.
- **OData Support**: Flexible querying for news articles and categories (e.g., filtering, sorting).
- **JWT Authentication**: Secure access with role-based authorization (Admin and Staff roles).
- **Role-Based Access**:
  - **Admin**: Manage accounts (cannot delete accounts linked to news articles).
  - **Staff**: Manage categories and news articles (cannot delete categories linked to articles).
- **Database**: MS SQL Server with predefined schema (Categories, NewsArticles, NewsTags, SystemAccounts, Tags).
- **Configuration**: Database connection and admin account details stored in `appsettings.json`.

## Technologies
- **ASP.NET Core 8.0**: Web API framework.
- **Entity Framework Core**: ORM for database operations.
- **Microsoft SQL Server**: Database for storing data.
- **OData**: For advanced querying capabilities.
- **JWT**: For secure authentication and authorization.
- **Repository Pattern**: Separates data access logic.
- **Singleton Pattern**: Ensures single instance of services.

## Project Structure

FUNewsManagementSystem_BE/
├── Data/
│   └── ApplicationDbContext.cs      # EF Core DbContext for database interaction
├── Models/
│   ├── Category.cs                  # Entity models for database tables
│   ├── NewsArticle.cs
│   ├── SystemAccount.cs
│   └── ...
├── Repositories/
│   ├── IRepository.cs              # Generic repository interface
│   ├── CategoryRepository.cs       # Repository for CRUD operations
│   └── ...
├── Services/
│   ├── IAccountService.cs          # Business logic interfaces
│   ├── AccountService.cs           # Service implementations
│   └── ...
├── Controllers/
│   ├── AccountController.cs        # API endpoints for account management
│   ├── NewsArticleController.cs    # API endpoints for news articles
│   └── ...
├── appsettings.json                # Configuration (database, JWT, admin account)
└── Program.cs                      # Application entry point

## Setup Instructions
1. **Prerequisites**:
   - Visual Studio 2019 or later with .NET 8.0 SDK.
   - Microsoft SQL Server 2012 or later.
   - Git for cloning the repository.

2. **Clone the Repository**:

   git clone https://github.com/HungTabe/ASM3_ASP.NET-WEB-API


3. **Database Setup**:
   - Run the provided SQL script (`FUNewsManagement.sql`) to create the database and seed initial data.
   - Update the connection string in `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=FUNewsManagement;Trusted_Connection=True;"
     }
     ```

4. **Install Dependencies**:
   Open the solution in Visual Studio and restore NuGet packages:

   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   Install-Package Microsoft.EntityFrameworkCore.Design
   Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
   Install-Package Microsoft.OData.ModelBuilder
   Install-Package Microsoft.AspNetCore.OData


5. **Run the Application**:
   - Press `F5` in Visual Studio to start the API.
   - The API will run at `https://localhost:5001` (default).

6. **Test Endpoints**:
   - Use tools like **Postman** or **Swagger** to test API endpoints.
   - Example: `GET /odata/NewsArticles?$filter=NewsStatus eq true` to retrieve active news articles.
   - Login: `POST /api/Account/login` with credentials (e.g., `admin@FUNewsManagementSystem.org`, `@@abc123@@`).

## API Endpoints
| Endpoint                          | Method | Description                          | Role         |
|-----------------------------------|--------|--------------------------------------|--------------|
| `/api/Account/login`              | POST   | Authenticate and return JWT token    | Any          |
| `/api/Account`                    | GET    | Get all accounts                     | Admin        |
| `/api/Account`                    | POST   | Create a new account                 | Admin        |
| `/odata/NewsArticles`             | GET    | Get news articles (OData queryable)  | Any (active) |
| `/odata/NewsArticles`             | POST   | Create a new news article            | Staff        |
| `/odata/Categories`               | GET    | Get categories (OData queryable)     | Any          |
| `/odata/Categories`               | POST   | Create a new category                | Staff        |

## Security
- **JWT**: Tokens are required for protected endpoints. Tokens expire after 1 hour.
- **Role Restrictions**: Admin (role 0) and Staff (role 1) have specific permissions.
- **Data Validation**: Ensures fields like email, title, and content meet requirements.
- **Deletion Constraints**: Prevents deleting accounts or categories linked to news articles.

## Notes
- The admin account is configured in `appsettings.json` and does not require database storage.
- Ensure the database connection string is correct before running.
- Use OData queries for advanced filtering (e.g., `$select`, `$filter`, `$orderby`).

## License
This project is licensed under the MIT License.

## Contact
For issues or questions, contact [trandinhhung717@gmail.com].
```
