# Skincare APP API

[![Tech stack](https://skillicons.dev/icons?i=cs,dotnet,docker,rabbitmq,git,postman)](https://skillicons.dev)

## Overview
**Skincare APP API** is the backend system for the Skincare Project. This project is built using **ASP.NET Core 8.0**.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [System Requirements](#system-requirements)
- [Installation](#installation)
- [Usage](#usage)

## Introduction

This is introduction section.

## Features

This is Features section

## Technologies Used

- **ASP.NET Core 8.0**: Main framework for building the web API.
- **Entity Framework Core**: ORM for database interactions.
- **SQL Server**: Database for storing application data.
- **JWT (JSON Web Token)**: Authentication and authorization mechanism.
- **RabbitMQ**: Message broker for handling messaging services (in case of microservices).
- **SignalR**: Real-time communication.
- **Docker**: Containerization for easy deployment.
- **YARP (Reverse Proxy)**: Supports reverse proxy and load balancing.
- **Swagger**: API documentation and testing interface.

## System Requirements

- .NET 8.0 SDK or later.
- SQL Server 2019 or later.
- Docker (optional for running in containers).
- RabbitMQ Server, Erlang.

## Installation

### 1. Clone the repository

First, clone the project to your local machine using Git:

```bash
git clone https://github.com/Stoner-Bros/skincare-api
cd skincare-api
```

### 2. Configure the database connection

Open the `appsettings.json` file and set up your connection to the SQL Server database. Replace the placeholders with your actual SQL Server credentials:

```json
"ConnectionStrings": {
    "DBDefault": "Data Source=YOUR_DS;Initial Catalog=YOUR_IC;User ID=YOUR_USER_ID;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

If you're running a local instance of SQL Server, make sure the `Server` and `Database` values are correct.

### 3. Install dependencies

Ensure you have the necessary packages installed by running the following command:

```bash
dotnet restore
```

This command will restore all the required NuGet packages for the project.

### 4. Apply database migrations

To set up the database schema and apply any migrations, run:

```bash
dotnet ef database update
```

This command will create the necessary tables in the SQL Server database defined in your connection string.

### 5. Run the application

To start the backend API server, execute:

```bash
dotnet run
```

The application will start. The port configured in `launchSettings.json`.

### 6. Running with Docker

Alternatively, you can run the application using Docker. First, make sure Docker is installed on your machine. Then, run:

```bash
docker-compose up --build
```

This will build and run the application in Docker containers. The API will be accessible via `http://localhost` (depending on the port mapping in `docker-compose.yml`).

### 7. Access API Documentation

Once the application is running, you can explore the API documentation using Swagger. Navigate to the following URL in your browser:

```
http://localhost:<port>/swagger
```

This will give you access to the list of available endpoints and allow you to interact with them directly through the Swagger UI.

## Usage

Once the backend is running, you can access the APIs through various endpoints.

You can refer to the Swagger documentation for detailed API information.
