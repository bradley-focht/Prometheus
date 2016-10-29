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
	* [Data Service](#data-service)
  * [Service Portfolio](#service-portfolio)
* [Configuration](#configuration)

## Getting Started
Setup information or link to setup documentation file

### Have a big company
You should start by having a big company to manage to utilize this tool (this is a joke placeholder for formatting example)

## How it Works
 * This thing has a super cool MVC website 
 * backed by some Services for business logic 
 * and EF to connect the whole damn thing to a database. 
 
That list ^^ is some example sections maybe?

### Cool Table
| Ecosystem | Resource      | Field          | Defaulted Value |
|:----------|:--------------|:---------------|:----------------|
| iQmetrix | [Pricing](http://developers.iqmetrix.com/api/pricing/#pricing)      | PricingTerm    | `null`          |
| iQmetrix | [Pricing](http://developers.iqmetrix.com/api/pricing/#pricing)      | OverridePrice  | `null`          |
| iQmetrix | [Pricing](http://developers.iqmetrix.com/api/pricing/#pricing)      | IsDiscountable | `false`         |
| iQmetrix | [Pricing](http://developers.iqmetrix.com/api/pricing/#pricing)      | FloorPrice     | `null`          |
| iQmetrix | [Availability](http://developers.iqmetrix.com/api/availability/#availability) | IsSerialized   | `false`         |
| iQmetrix | [Customer](http://developers.iqmetrix.com/api/crm/#customer)     | DoNotContact   | `true`          |
| iQmetrix | [Address](http://developers.iqmetrix.com/api/crm/#address)     | DoNotContact   | `true`          |

## Limitations
This project is done by some serious noobs with some serious time restrictions

## Project Structure
The components in this project are organized in the following structure:

* **DataService** - iQmetrix Resources and their callsClasses that deal specifically with sending API requests
* **ServicePortfolio** - Classes with business logic for the app

### Data Service
The Data Service is responsible for all calls to the Database and managing its entities or `Models`.

Models:
* **Service** - Class that makes requests related to the [Asset](http://developers.iqmetrix.com/api/assets/) resource.
* **Service Bundle** - Allows you to create inventory availability, see [Inventory Availability](http://developers.iqmetrix.com/api/availability/)


### Service Portfolio 
The Service Portfolio handles all logic between the website and database for all things related to the Service Portfolio. This is done through a number of `Controllers`

Controllers:
 * **First Controller**

## Configuration
Here is an explanation of the config file.

Here are some sample values for the config file
