# E-commerce Clean Architecture

## Overview

This is an e-commerce application designed with **Clean Architecture** principles to ensure modularity, scalability, and maintainability. The project implements the **CQRS (Command Query Responsibility Segregation)** pattern to separate read and write operations, optimizing performance and enabling flexible scaling. This repository, `Hassan-sami/ecommerce-clean-architecture`, serves as a foundation for building a robust e-commerce platform.

## Features

- **User Management**: User registration, authentication, and profile management.
- **Product Catalog**: Browse, search, and filter products by categories or attributes.
- **Shopping Cart**: Add, update, or remove items in the cart.
- **Order Management**: Create and track orders with order history.
- **CQRS Implementation**: Separates commands (write operations) and queries (read operations) for improved performance.
- **Scalable Architecture**: Modular design following Clean Architecture principles.

*(Note: Additional features like payment integration or admin panel may be in development, as the repository appears to be a work in progress based on the lack of releases.)*

## Architecture

The application adheres to **Clean Architecture**, organizing the codebase into distinct layers to ensure separation of concerns:

1. **Domain Layer**: Contains core business entities, models, and rules (e.g., `Product`, `Order`, `User`).
2. **Application Layer**: Defines use cases, business logic, and orchestrates data flow using CQRS (commands and queries).
3. **Infrastructure Layer**: Handles external systems such as databases, APIs, or third-party services.
4. **Presentation Layer**: Manages API endpoints, controllers, or user interfaces.

The **CQRS pattern** ensures that read operations (e.g., fetching product details) are separated from write operations (e.g., creating an order), enabling optimized data handling and scalability.

## Technologies Used

- **Backend**: .NET 8.0 (ASP.NET Core)
- **Database**: Microsoft SQL Server
- **API**: RESTful APIs
- **Containerization**: Docker and Docker Compose
- **Build Tools**: .NET SDK 8.0

## Getting Started

### Prerequisites

- **Docker**: Ensure Docker and Docker Compose are installed on your system.
- **.NET SDK 8.0**: Required if you want to build or run the application without Docker.
- **Git**: For cloning the repository.
- A compatible system with sufficient resources to run Docker containers.

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Hassan-sami/ecommerce-clean-architecture.git
   cd ecommerce-clean-architecture
   ```

2. **Set up environment variables**:
   - The `docker-compose.yml` file includes environment variables for the SQL Server and web application. Ensure the following are configured correctly:
     - SQL Server password (`YourStrong@Passw0rd`) meets Microsoft SQL Server requirements (at least 8 characters, including uppercase, lowercase, numbers, and special characters).
     - If needed, create a `.env` file to override defaults or add additional configurations (e.g., API keys for external services):
       ```env
       ASPNETCORE_ENVIRONMENT=Development
       ConnectionStrings__DefaultConnection=Server=sqlserver;Database=AppDb;User=sa;Password=YourStrong@Passw0rd;Encrypt=False;
       ```

3. **Run the application with Docker Compose**:
   - The project uses Docker to containerize the application and database. The `docker-compose.yml` file sets up two services:
     - `sqlserver`: A Microsoft SQL Server instance.
     - `webapp`: The .NET Core web application.
   - To build and run the services, execute:
     ```bash
     docker-compose up --build
     ```
   - This command:
     - Builds the .NET application using the `Dockerfile`, which restores dependencies, compiles the solution, and publishes the `ecommerce.api` project.
     - Starts the SQL Server container on port `1433`.
     - Starts the web application, accessible on `http://localhost:7198` (HTTP) and `https://localhost:7199` (HTTPS).
   - To stop the services, press `Ctrl+C` or run:
     ```bash
     docker-compose down
     ```

4. **Access the application**:
   - Open a browser and navigate to `http://localhost:7198` or `https://localhost:7199` to interact with the API.
   - Use tools like Postman or curl to test API endpoints (e.g., `/api/products` or `/api/orders`, depending on implementation).

5. **Running without Docker** (Optional):
   - If you prefer to run the application directly:
     - Ensure a local SQL Server instance is running with the database `AppDb` and credentials matching the connection string.
     - Navigate to the project root and restore dependencies:
       ```bash
       dotnet restore
       ```
     - Build and run the application:
       ```bash
       cd src/ecommerce.api
       dotnet run
       ```
## Project Structure

```plaintext
ecommerce-clean-architecture/
├── src/
│   ├── ecommerce.Domain/      # Business entities and rules (e.g., Product, Order)
│   ├── ecommerce.application/ # Use cases, commands, queries, and CQRS handlers
│   ├── ecommerce.infra/       # Database, external services, and repositories
│   ├── ecommerce.api/         # API controllers and endpoints
├── Dockerfile                 # Docker build configuration for the web app
├── docker-compose.yml         # Docker Compose configuration for services
├── .gitignore                 # Ignored files and directories
├── README.md                  # This file
├── ecommerce.sln              # Solution file for .NET projects
```

## Contributing

We welcome contributions to enhance the project! To contribute:

1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your feature description"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Open a pull request on GitHub.

## License

This project is licensed under the [MIT License](LICENSE). See the LICENSE file for details.

## Contact

For questions, suggestions, or issues, please reach out to [Hassan Sami](https://github.com/Hassan-sami) or open an issue on the repository.

---

© 2025 Hassan Sami
