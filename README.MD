This is sample ASP.NET Core project.

**ASP.NET_web_app_example** is an implementation of base Asp.Net Core project which written with **clean architecture** and best practices.
This Repository include one web application project with Asp.Net Core & EF.Core which used aspnetcore components; ***razor pages, middlewares, dependency injection, configuration, logging, web api***. To create websites with minimum implementation of asp.net core based on HTML5, CSS, and JavaScript

## Whats Including In This Repository
Project implements below features:
* Asp.net core built-in dependency injection
* Cookies based authentication and roles based authorization
* Business rule check, domain policy apply and validations
* Web API
* Aspnet core razor tools - View Components, partial Views, Tag Helpers, Model Bindings and Validations, Razor Sections etc..  

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* .Net Core 7.x or later
* EF Core 7.x or later

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running:
```csharp
dotnet restore
```
3. Next, build the solution by running:
```csharp
dotnet build
```
4. Next, within the app directory, launch the back end by running:
```csharp
dotnet run
```
5. Launch http://localhost:7218/ in your browser to view the Web UI.


### Usage
After cloning or downloading the sample you will need to run its Entity Framework Core **migrations** before you will be able to run the app.

1. Ensure your connection strings in ```appsettings.json``` point to a local SQL Server instance.

2. Open Package Manager Console and run below command;

```csharp
update-database
```

These commands will create aspnetrun database which include Product and Category table. You can see from **AppDbContext.cs**.

If you modify-change or add new some of entities to Core project, you should run ef migrate commands in order to update your database as the same way but below commands;
```csharp
add migration YourCustomEntityChanges
update-database
```

### Database Graph
![alt text](https://github.com/Vlad311010/ASP.NET_web_app_example/blob/master/app/DbGraph.png?raw=true)

### Structure of Project

Repository include folders for group implementations;
* wwwroot
* Migrations
    * Generated by scaffolding from ef.core    
* Models
    * Hero
    * HeroInstance
    * Inventory
    * Item
    * User
* Pages
    * Default Razor Web Application Template of Asp.Net Core
* Repositories
    * Repository interfaces (IUserRepository)
    * Repository implementation (UserRepository)
    * AppDbContext
* ViewComponents
    * NavBarViewComponent
* ViewModels
    * HeroInstanceFullInfo
    * OwenedItemView
* Api
* Program.cs
 

#### Migrations
EF add-migration classes.


#### Models Folder
Includes Entity Framework Core Models which creates sql table with **Entity Framework Core Code First Aproach**. Some Aggregate folders holds entity and aggregates.

#### Repository Folder
EF Repository implementation. This class responsible to create queries, includes, where conditions etc..

Implementation of Core interfaces in this project with **Entity Framework Core** and other dependencies.
Most of your application's dependence on external resources should be implemented in classes defined in the Infrastructure project. These classes must implement the interfaces defined in Core. If you have a very large project with many dependencies, it may make sense to have more than one Infrastructure project (eg Infrastructure.Data), but in most projects one Infrastructure project that contains folders works well.
This could be includes, for example, **e-mail providers, file access, web api clients**, etc. For now this repository only dependend sample data access and basic domain actions, by this way there will be no direct links to your Core or UI projects.

#### Pages Folder
Development of UI Logic with implementation. Interfaces drives business requirements and implementations in this layer.
The application's main **starting point** is the ASP.NET Core web project. It currently uses the default **ASP.NET Core project template** which based on **Razor Pages** templates. This includes appsettings.json file plus environment variables in order to stored configuration parameters, and is configured in Startup.cs.

#### wwwroot Folder
The wwwroot folder in the ASP.NET Core project is treated as a web root folder. Static files can be stored in any folder under the web root and accessed with a relative path to that root.

In the standard ASP.NET application, static files can be served from the root folder of an application or any other folder under it. This has been changed in ASP.NET Core. Now, only those files that are in the web root - wwwroot folder can be served over an http request. All other files are blocked and cannot be served by default.


### Used Components:
* Asp.Net Core
* Entity Framework Core
* Razor Pages
    * Scaffolding Razor
    * Tag Helpers
    * Bindings
    * Model Validations
    * Navigation - Page Routes
    * User Interfaces
    * Partial Views
    * View Components
    * Dynamic Layout
* Middlewares
* Minimal API
* Logging
* Cookies based authentication and roles based authorization
* Configuration
* Dependency Injection

#### API Endpoints

## Technologies
* .NET Core 7.0
* ASP.NET Core 7.0
* Entity Framework Core 7.0 
* .NET Core Native DI
* Razor Pages
* AutoMapper

## Main Objects
* Asp.Net Core
* Entity Framework Core
* Razor Pages   
* Repository Design Pattern
* Multiple Page Web Application (MPA)
* Monolitic Deployment Architecture
* SOLID and Clean Code