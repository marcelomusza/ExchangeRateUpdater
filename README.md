# Exchange Rate Updater WebAPI Application

## Introduction

This is a Web API application which acts as a system that connects to external APIs to retrieve currency exchange rates for different entities, saving results into a database and then exposing endpoints to show the information in different formats.
The base idea of this exercise is to connect to an exposed public API from the Czech National Bank, but the solution is open for extension, since we can add more external APIs as needed.

## Tech Stack

- .NET 8 Web API with Clean Architecture (with Vertical Slices orientation)
- Serilog for centralized logging
- Global Exception Handling Middleware
- Swagger for API documentation
- MediatR and CQRS Pattern
- Rate Limiting for API endpoints
- AutoMapper for object mapping
- Extension methods for more specific mappings
- FluentValidation integrated into MediatR
- Hangfire for background jobs
- MySQL Database with EF Core (Code First)
- API Key authentication for secure endpoints
- Unit testing with NUnit and Moq
- Docker and Docker Compose

## Architecture

The solution is structured using Clean Architecture principles with MediatR and CQRS patterns to enhance database handling and scalability. This ensures the application follows SOLID principles, decouples components, and provides a scalable, maintainable foundation.

The key part of this design is that it follows Vertical Slices practices with Clean Architecture, in which we can add more components (like additional banks public APIs) easily without affecting other existing features. Each "Slice" would be a separate public API we can introduce to our App.

While this tech stack may seem extensive for this exercise, it showcases a robust setup for scalable applications. The architecture supports extensibility for additional jobs, repository operations, and services, making it easy to expand or modify as needed.

In addition to what was mentioned above, the use of Docker Compose makes it easy to deploy the application in any environment, ensuring consistency and ease of setup.

## Extra Miles

Additional features beyond the requirements of this exercise include:
- Fully Working Production Deployment on Azure Container Instance (DETAILS BELOW)
- Hangfire job that runs on a daily basis to invoke the Czech Bank Api and save data into the database
- Fully working Database approach with MySql and EF Core with Code First migrations and seeds for static data.
- Vertical Slices principles to allow extension for more external Apis
- Public Endpoint to retrieve currency exchange rates by date
- Added Serilog, MediatR, FluentValidation, CQRS (Commands and Queries with proper Validation), Rate Limiting, Docker, and Docker Compose for a comprehensive setup

## The complete Flow of this App

This App has two ways to invoke the Czech Bank public API. One way is a secured by ApiKey Endpoint to invoke the Api, and the second one is a Hangfire Job that runs on a daily basis. Both approaches use the same service internally, which processes the API response, converts the data as entities and save them into the database using a simple yet effective model.

In addition to that, the App exposes two public endpoints. One is used to retrieve a list of currency exchange rates by day, and the other one is for the test requirement, which will provide a list of currency codes to a service, and then process them with the data in the database to elaborate a response in the following format:
```json
{
    "bankName": "Czech National Bank",
    "exchangeRates": [        
        "10/16/2024: CZK/EUR = 25.2950",
        "10/16/2024: CZK/INR = 27.6270",
        "10/16/2024: CZK/USD = 23.2120",
        "10/17/2024: CZK/EUR = 25.2500",
        "10/17/2024: CZK/INR = 27.6570",
        "10/17/2024: CZK/USD = 23.2440"
    ]
}
```
The Source Currency for this case in particular is CZK since it is coming from the Czech National Bank, and the corresponding Target Currency that sets the currency pairs are those that are retrieved from the bank public API. The System is open for extension, so we can add other national banks in the future which will form new currency pairs, like for example `10/14/2024: USD/EUR = 1.0`, meaning that we are using a public API from a bank in United States to form the output.

## The database model

The database has 3 tables:
- ExchangeRates
- Banks 
- Currencies

The Currencies table was seeded with the complete list of currency codes that exists today, according to ISO 4217. This data is static and unlikely to change so it makes sense to have it pre-seeded to the database

The Banks table has the Czech Bank pre-seeded for ease of execution of this test, however it is ready to be used for multiple banks

The ExchangeRates table will hold the currency code pairs and the respective values.

## Running the Application on Azure

Below you will find the exact endpoint urls you can use to invoke and test this application directly. Use Postman for this since you will need to set different parameters to the endpoints, specially the POST one.

- `POST http://exchange-rate-updater-api.westeurope.azurecontainer.io/api/exchange-rate/czech-bank/process`
    - Requires an `x-api-key` header with the API key = MewsTest
    - The json request body is this one: 
    ```
    {
        "bankId": 1,
        "date": "2024-10-17",
        "language": 1
    } 
    ```

- `GET http://exchange-rate-updater-api.westeurope.azurecontainer.io/api/exchange-rate/czech-bank/<BankId>/daily?date=<Date>`: 
    - Retrieves currency exchange rates from the specified BankId, and the Date in YYYY-mm-dd format
    
- `GET http://exchange-rate-updater-api.westeurope.azurecontainer.io/api/exchange-rate/czech-bank/<BankId>/exchange-rates?currencyCodes=<CurrencyCodes>`: 
    - Retrieves and process list of currency codes from the specified BankId, and the CurrencyCodes in "USD,EUR..." format.
    - The Response is the requirement output of this Test.

NOTE: If you find these URLs offline, please get back to me and I'll make sure to bring them back online.


## Running the Application Locally

If you want to run the application locally, follow the steps below.

### Prerequisites

- Ensure Docker and Docker Compose are installed.

### Steps


1. **Navigate to the solution root directory in your terminal**.
2. **Run Docker Compose**:
   ```bash
   docker-compose up -d
   ```
3. **Connect to the API container**:
   ```bash
   docker exec -it exchangerateupdater-api-1 /bin/sh
   ```
4. **Apply database migrations:**
   ```bash
   cd ExchangeRateUpdater.Api
   dotnet ef database update
   ```

Once done, the Application will be ready to be used.

## Configuration Note

For simplicity, connection strings, the API key, and other configurations are stored in `appsettings.json` or `docker-compose.yml`. In a production environment, sensitive data should be managed in secure storage solutions like Azure Key Vault or AWS Secrets Manager.

The WebAPI Dockerfile has a single stage using the dotnet sdk package, which allows the container to have dotnet and ef commands available in order to execute the initial migration. This is not intended for production since the container will have unnecessary packages, but to make it easier to run the migrations it is acceptable in this case. In a real production scenario we would use scripts to update and/or seed the database, and the container should be as light as possible.

## Using the API

Three primary endpoints are available:

- `POST http://localhost:8080/api/exchange-rate/czech-bank/process`
    - Requires an `x-api-key` header with the API key = MewsTest
    - The json request body is this one: 
    ```
    {
        "bankId": 1,
        "date": "2024-10-17",
        "language": 1
    } 
    ```

- `GET http://localhost:8080/api/exchange-rate/czech-bank/<BankId>/daily?date=<Date>`: 
    - Retrieves currency exchange rates from the specified BankId, and the Date in YYYY-mm-dd format
    
- `GET http://localhost:8080/api/exchange-rate/czech-bank/<BankId>/exchange-rates?currencyCodes=<CurrencyCodes>`: 
    - Retrieves and process list of currency codes from the specified BankId, and the CurrencyCodes in "USD,EUR" format.
    - The Response is the requirement output of this Test.

By default, the Web API runs on port `8080` and MySQL on port `3306`.