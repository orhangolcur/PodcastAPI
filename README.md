# PodcastAPI 🎙️

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat&logo=sqlite&logoColor=white)

A robust, scalable, and fully-featured RESTful API developed for a Podcast application. Built entirely with **C# and .NET Core**, this project was designed focusing on maintainability and enterprise-level software architecture principles.

## 🏗️ Architecture & Design Patterns

This project moves beyond standard CRUD operations by implementing modern backend architectural patterns:
* **Clean Architecture:** Separation of concerns across `Core`, `Infrastructure`, and `Presentation` layers.
* **Strict CQRS (Command Query Responsibility Segregation):** Implemented using **MediatR** to decouple read and write operations, especially in the Auth, Subscription, and Podcast modules.
* **Vertical Slice Architecture:** Applied for specific features like server-side search to encapsulate feature-specific logic.
* **Repository Pattern:** To abstract database operations and ensure a clean data access layer.

## ✨ Key Features

* **Advanced Authentication:** Secure JWT-based authentication system with **Refresh Token rotation** for enhanced session security.
* **Automated RSS Integration:** Implementation of `.NET BackgroundServices` to automatically fetch, parse, and store new podcast episodes from external RSS feeds.
* **Smart Search:** Server-side search capabilities supporting filtering by title, description, and categories.
* **Robust Validation Pipeline:** Integrated **FluentValidation** with a global exception handling middleware to ensure clean and predictable API responses.
* **File Management:** Fully implemented file upload system via CQRS for user profiles.
* **Email Services:** Integrated SMTP service for automated email handling.

## 🛠️ Tech Stack

* **Framework:** .NET 8 / ASP.NET Core Web API
* **Language:** C#
* **ORM & Database:** Entity Framework Core, SQLite
* **Core Libraries:** MediatR, AutoMapper, FluentValidation
* **Documentation:** Swagger / OpenAPI

## 📂 Project Structure

* **`Core/`**: Contains domain entities, interfaces, and DTOs. Represents the heart of the business logic.
* **`Infrastructure/`**: Contains the persistence layer (EF Core DbContext, Repositories), Background Services, and third-party integrations (SMTP).
* **`Presentation/`**: The ASP.NET Core Web API project containing Controllers, Middlewares, and dependency injection setups.

## 🚀 Getting Started

### Prerequisites
* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* A SQLite browser (optional, for inspecting the database)

### Installation & Configuration

1. **Clone the repository:**
   ```bash
   git clone https://github.com/orhangolcur/PodcastAPI.git
   ```
   
2. **Navigate to the API directory:**
   ```bash
   cd PodcastAPI/Presentation/PodcastAPI.API
   ```

3. **Configuration:**
   * Update the `appsettings.json` file with your specific JWT Secret Keys and SMTP configuration details.

4. **Apply database migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```
