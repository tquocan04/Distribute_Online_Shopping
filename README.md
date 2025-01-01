# Distributed Customer and Employee Data API

A backend project to manage and distribute customer and employee data based on geographic regions (North, Central, South) and global data using ASP.NET Core Web API.

## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Running Project](#running-project)

## Features
- Distribute customer and employee data across regions: **North**, **Central**, **South**, and a **global server**.
- Authentication and authorization using **JWT**.
- Multimedia storage using **Cloudinary** for images and videos.
- Metadata storage with **MongoDB** for customers and products.
- Relational data storage with **SQL Server**.
- Utilizes **Automapper** for object mapping and **Entity Framework** for database interactions.

## Tech Stack

**Database:** SQL Server, MongoDB

**Tools:** Entity Framework, JWT, AutoMapper

**Framework:** ASP.NET Core Web API 8.0

**Multimedia Storage:** Cloudinary

## Installation

### Prerequisites
- **Windows**: Visual Studio 2017 or later with necessary extensions.
- **macOS**: Visual Studio Code with C# and .NET extensions.
- **SQL Server**: Version `2017-ssei-eval` or later.

### Steps
1. **Clone the repository**:
   ```bash
   git clone https://github.com/tquocan04/Distribute_Online_Shopping
   cd your-repo-name
   ```

2. **Open the project**:
   - On **Windows**:
     - Open the project in **Visual Studio 2017 or later**.
   - On **macOS**:
     - Open the project folder in **Visual Studio Code**.

3. **Install dependencies**:
   - Ensure all required NuGet packages are installed. Visual Studio should automatically restore them when the project is opened.

4. **Set up SQL Server**:
   - Install **SQL Server 2017-ssei-eval** or later.
   - Create the following databases:
     - Global server: `Online_Shopping_Global`
     - North server: `Online_Shopping_North`
     - South server: `Online_Shopping_South`
     - Central server: `Online_Shopping_Central`

5. **Configure SQL server**:
   Configure North, South, Region server in applications.json file of each project to correspond to the connection string and database name in server.

5. **Configure MongoDB**:
   - Create a database name *Online_Shopping_Metadata* with two collections: *customer* and *product*.
   - Ensure MongoDB is running and accessible.
   - Configure MongoDB in applications.json file of Online_Shopping project with your connection string and database name in MongoDB.

6. **Configure environment variables**:
   Create a `.env` file in the root directory (Online_Shopping project) with the following:
     ```env
     DB_CONNECTION_STRING=your_sql_server_connection_string


## Running Project

Press F5 to run project.

