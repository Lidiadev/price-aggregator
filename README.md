Welcome to the Price Aggregator project!

## Overview
The Price Aggregator is a microservice designed to aggregate, retrieve and cache prices of financial
instruments. It provides several functionalities through API endpoints.

## Features
1. Aggregated Instrument Price Endpoint:
- Use this API endpoint to request the aggregated price of a specific instrument at a given time point (with hour accuracy).
- The microservice will check its datastore and serve the aggregated price if it's already available.
- If the aggregated price is not available, the microservice will request prices from external sources, aggregate them, 
persist the result, and return it through the API endpoint.
  
2. Persisted Instrument Price Retrieval Endpoint:
- This API endpoint allows you to fetch persisted instrument prices from the datastore based on a user-specified time range.

## Technologies and Tools 
The Price Aggregator project utilizes the following technologies and tools:
- ASP.NET Core 7.0
- Entity Framework Core 7.0 (with In-memory DB)
- XUnit, Fluent Assertions for unit testing
- WireMock for simulating external APIs
- Circle CI for continuous integration

## Architecture
The project follows the principles of Clean Architecture and Domain-Driven Design (DDD). 
This ensures a separation of concerns and maintainability of the codebase.

## Running the API
To run the API, follow the steps below:

1. Open your preferred IDE (Rider or Visual Studio).
2. Run the `PriceAggregator.API` project.
3. The API provides a Swagger UI interface that allows you to interact with the API and explore its endpoints.


### Database Configuration
The solution is configured to use an in-memory database, eliminating the need for additional infrastructure setup.

## Testing
The microservice's unit and integration tests are automatically executed by CircleCI whenever a commit is pushed to the main branch.

The integration tests use a standalone WireMock stub server managed by docker-compose to mimic the behavior of external APIs.

*Please note that due to time limitations, the project does not currently have a high unit tests coverage. 
However, integrating SonarCloud would help generate code coverage reports for each Pull Request. 
This integration would prevent a PR from being merged if it fails to meet the required code coverage percentage.

### Running the Integration Tests
To run the integration tests, perform the following steps:

1. Open a terminal.
2. Run the following command to start the WireMock stub server using docker-compose:
```
docker-compose up
```
3. Once the container is up and running, run the integration tests with the following command:
```
dotnet test PriceAggregator.IntegrationTests/PriceAggregator.IntegrationTests.csproj
```
