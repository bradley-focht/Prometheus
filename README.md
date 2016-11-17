# Prometheus
Prometheus is an IT Service Management tool. It allows the clients of large IT organizations to request services that have been defined by the organization. It then either executes powershell scripts to fulfill the request or sends the request to the organization's ticketing system. 

This project is being done by Software Systems Engineering students for their capstone project at the University of Regina

Specifically, this project will:
- High Level Requirements

## Table Of Contents
* [Getting Started](#getting-started)
  * [Have a big company](#have-a-big-company)
* [How it Works](#how-it-works)
  * [Cool Table](#cool-table)
* [Limitations](#limitations)
* [Project Structure](#project-structure)
  * [Common](#common)
  * [Data Service](#data-service)
  * [Dependency Resolver](#dependency-resolver)
  * [Service Portfolio Service](#service-portfolio-service)
  * [User Manager](#user-manager)
  * [Web UI](#web-ui)
* [Configuration](#configuration)

## Getting Started
Setup information or link to setup documentation file

### Have a big company
You should start by having a big company to manage to utilize this tool (this is a joke placeholder for formatting example)

## How it Works
 * Use a .NET MVC website to interface with users on all platforms
 * Integrate with the company's Active Directory system to handle access to the application
 * Use a series of microservices to handle the business logic for sections the application
 * Use Entity Framework in the Data Service as a single touchpoint with the database.
 * Provide a REST API for viewing or adjusting Service Requests for the company
 * A separate engine interfaces with this REST API to execute the service requests or handle them in any way the company sees fit

## Limitations
This project is done by some serious noobs with some serious time restrictions

## Project Structure
The components in this project are organized in the following structure:

* **Common** - DTOs, enumerations for entities, and utility classes
* **DataService** - Component responsible for interfacing with the database and the rest of the components
* **DependencyResolver** - Provides Ninject Modules that allow the project to use dependency injection throughout the application
* **ServicePortfolioService** - Service responsible for all business logic for the application related to the service tree structure (Services, Portfolio, Catalog, etc.)
* **UserManager** - Component responsible for interfacing with Active Directory and managing permissions for users
* **WebUI** - Cool MVC stuff

### Common
The Common assembly is used to provide DTOs, `Enums` for entities, and `Utilities` that are to be used across the application

Data Transport Objects (DTOs):
DTOs for all `Models` in the [Data Service](#data-service).

Enumerations: 
Enumeration types for `Models` and `Dto` entities.

Utilities:
* **AutoMapperInitializer** - Utility used to initialize AutoMapper to be able to map `Models` to their corresponding `Dto`s and vice versa.

### Data Service
The Data Service is responsible for all calls to the Database and managing its entities or `Models`.

Models:
* **Lifecycle Status** - 
* **Role** - 
* **Service** - 
* **Service Bundle** - 
* **Service Contract** - 
* **Service Document** - 
* **Service Goal** - 
* **Service Measure** - 
* **Service Request Option** - 
* **Service SWOT** - 
* **Service Work Unit** - 
* **Swot Activity** - 
* **User** - 

### Dependency Resolver
Provides Ninject `Modules` that allow the project to use dependency injection throughout the application.

Modules:
* **ServicePortfolioServiceModule** - Module that Loads bindings to the classes used in the `ServicePortfolioService`
* **UserManagerModule** - Module that Loads bindings to the classes used in `UserManager`

### Service Portfolio Service
The Portfolio Service handles all logic between the website and database for all things related to the Service Portfolio. This is done through a number of `Controllers`

Controllers:
* **Lifecycle Status Controller** - Provides operations related to the `LifecycleStatus` entity
* **Service Controller** - Provides operations related to the `Service` entity
* **Service Bundle Controller** - Provides operations related to the `ServiceBundle` entity

### User Manager

### Web UI

## Configuration
Here is an explanation of the config file.

Here are some sample values for the config file
