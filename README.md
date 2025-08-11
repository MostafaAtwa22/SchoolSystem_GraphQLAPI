# School Management System - GraphQL API

A comprehensive school management system built with **ASP.NET Core** and **GraphQL** using **HotChocolate**. This system provides a modern API for managing students, courses, departments, and enrollments with real-time subscriptions and Firebase authentication.

## 🏗️ Architecture

The solution follows a clean architecture pattern with three main projects:

- **`ShcoolGraphQL`** - Main API project with GraphQL schema and endpoints
- **`SchoolGraphQL.Entities`** - Domain models, DTOs, and interfaces
- **`SchoolGraphQL.DataAccess`** - Data layer with Entity Framework Core and repositories

## 🚀 Features

### Core Functionality
- **Student Management** - Create, read, update, and delete student records
- **Course Management** - Manage course catalog and course information
- **Department Management** - Organize courses and students by departments
- **Enrollment System** - Handle student course enrollments

### GraphQL Capabilities
- **Queries** - Retrieve data with flexible filtering, sorting, and projections
- **Mutations** - Modify data through GraphQL mutations
- **Subscriptions** - Real-time updates using in-memory subscriptions
- **Authorization** - Secure endpoints with Firebase authentication

### Advanced Features
- **Filtering & Sorting** - Built-in HotChocolate data filtering and sorting
- **Projections** - Efficient data loading with GraphQL projections
- **Real-time Updates** - WebSocket-based subscriptions for live data
- **Firebase Integration** - Authentication and authorization using Firebase Admin SDK

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **GraphQL**: HotChocolate 15.1.8
- **Database**: SQL Server with Entity Framework Core 8.0
- **Authentication**: Firebase Admin Authentication
- **Real-time**: HotChocolate In-Memory Subscriptions
- **UI Tools**: GraphQL Playground & Voyager

## 📋 Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or full instance)
- Firebase project with Admin SDK credentials
- Visual Studio 2022 or VS Code

## ⚙️ Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd SchoolSystem_GraphQLAPI
```

### 2. Configure Database
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\mssqllocaldb;Database=SchoolGraphQLDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Configure Firebase
1. Create a Firebase project
2. Generate a service account key
3. Place the JSON file as `firebase-config.json` in the main project
4. Update Firebase configuration in `appsettings.json`

### 4. Database Migration
```bash
cd SchoolGraphQL.DataAccess
dotnet ef database update
```

### 5. Run the Application
```bash
cd ShcoolGraphQL
dotnet run
```

## 🌐 API Endpoints

### GraphQL Endpoints
- **GraphQL API**: `https://localhost:5001/graphql`
- **GraphQL Playground**: `https://localhost:5001/ui/playground`
- **GraphQL Voyager**: `https://localhost:5001/ui/voyager`

### Sample Queries

#### Get All Students
```graphql
query {
  students {
    id
    firstName
    lastName
    email
    department {
      name
    }
    enrollments {
      course {
        title
      }
    }
  }
}
```

#### Create a New Student
```graphql
mutation {
  createStudent(input: {
    firstName: "John"
    lastName: "Doe"
    email: "john.doe@example.com"
    departmentId: 1
  }) {
    id
    firstName
    lastName
    email
  }
}
```

#### Subscribe to Student Updates
```graphql
subscription {
  onStudentAdded {
    id
    firstName
    lastName
    email
  }
}
```

## 📁 Project Structure

```
SchoolSystem_GraphQLAPI/
├── ShcoolGraphQL/                 # Main API Project
│   ├── Schema/                    # GraphQL Schema Definitions
│   │   ├── Student/              # Student queries, mutations, subscriptions
│   │   ├── Course/               # Course operations
│   │   ├── Department/           # Department operations
│   │   └── Query.cs              # Root query type
│   ├── Program.cs                # Application startup
│   └── firebase-config.json      # Firebase configuration
├── SchoolGraphQL.Entities/        # Domain Layer
│   ├── Models/                   # Entity models
│   ├── Dtos/                     # Data transfer objects
│   └── Interfaces/               # Repository interfaces
└── SchoolGraphQL.DataAccess/      # Data Access Layer
    ├── Data/                     # DbContext and configurations
    ├── Repositories/             # Repository implementations
    ├── Configurations/           # Entity configurations
    └── Migrations/               # EF Core migrations
```

## 🔧 Configuration

### Database Configuration
Configure your database connection in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "your-connection-string-here"
  }
}
```

### Firebase Configuration
Ensure your `firebase-config.json` contains valid Firebase Admin SDK credentials.

## 🚦 Development

### Adding New Entities
1. Create the entity model in `SchoolGraphQL.Entities/Models/`
2. Add repository interface and implementation
3. Create GraphQL types in the Schema folder
4. Register services in `Program.cs`
5. Run migrations

### Testing GraphQL Operations
Use the built-in GraphQL Playground at `/ui/playground` to test queries, mutations, and subscriptions interactively.

## 🐛 Known Issues

Based on the current codebase, there are some areas for improvement:
- Namespace inconsistency (`ShcoolGraphQL` vs `SchoolGraphQL`)
- Manual DTO mapping could be optimized with AutoMapper
- Consider implementing LINQ projections for better performance

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For questions or issues, please create an issue in the GitHub repository or contact the development team.

---

**Built with ❤️ using ASP.NET Core and GraphQL**
