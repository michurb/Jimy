# Jimy Application

The Jimy Application is a comprehensive fitness and workout management platform designed to help users plan workout routines, log exercises, and track their activity over time. The project includes multiple components, such as an API, Blazor-based frontend, and SQL Server database.

## Table of Contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Clone the Repository](#clone-the-repository)
- [Running the Application](#running-the-application)
  - [Running the Database Only](#running-the-database-only)
  - [Running the Entire Application](#running-the-entire-application)
  - [Stopping the Containers](#stopping-the-containers)
- [Technologies Used](#technologies-used)

## Overview

The Jimy Application allows users to:
- Create and manage workout plans.
- Log workout sessions, including sets and weights.
- Track user activities with duration and descriptions.
- View dashboards and activity summaries.

## Prerequisites

To run this project locally, you need to have the following installed:

- **Docker**: For containerization.
- **Docker Compose**: To manage multi-container applications.
- **.NET SDK**: For local development.

## Getting Started
### Clone the Repository
```bash
git clone https://github.com/michurb/Jimy.git
cd Jimy
```

### Running the Database Only
To run the database container only, you can use the following command:
```bash
docker-compose up db
```

### Running the Entire Application

To run the entire application, you can use Docker Compose. The `docker-compose.yml` file contains the configuration for the application services.
```bash
docker-compose up
```

After the services start, you can access them at the following addresses:
- API: `http://localhost:5080`
- Frontend: `http://localhost:5284`

### Stopping the Containers
```bash
docker-compose down
```

## Technologies Used
* ASP.NET Core: For building the RESTful API.
* Blazor: A frontend framework for building client-side web applications.
* SQL Server: As the relational database.
* Entity Framework Core: For data access and ORM.
* Docker: For containerization.


## Project Structure

The repository is structured as follows:

```plaintext
├── src/
│   ├── Jimy.Api/                # Backend API service (ASP.NET Core)
│   ├── Jimy.Blazor/             # Frontend service (Blazor)
│   ├── Jimy.Core/               # Domain models and business logic
│   ├── Jimy.Application/        # Application layer (CQRS implementation)
│   ├── Jimy.Infrastructure/     # Infrastructure and data access
├── docker-compose.yml            # Docker Compose configuration for the application
├── Jimy.sln                      # Solution file
├── README.md                     # Project documentation
```
