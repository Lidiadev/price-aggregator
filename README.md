## Overview
Price Aggregator is a a microservice for aggregation, retrieval and caching of prices of financial
instruments.

The microservice is providing the following functionalities:
- an API Endpoint to request the aggregated instrument price at a specific time point
with hour accuracy.
  - The aggregated price will be served from the datastore if it’s already available
  - If not available, the prices will be requested from the external sources,
  aggregated, then persisted and returned by the endpoint
- an API Endpoint that fetches the persisted instrument prices from the datastore
  during a user-specified time range

## Technologies and tools used
- .NET 7.0
- In-memory DB
- RESTful API
- Circle CI

## How to run the API
From an IDE (Rider or Visual Studio), run `PriceAggregator.API`.

The API provides Swagger UI which allows to interact with the API.

## Testing
CircleCI runs integration tests for the microservice whenever a commit is pushed to the 
main branch. 

These tests use a standalone WireMock stub server managed by docker-compose to mimic the behavior of external APIs.


*Due to time limitations, the project does not currently have unit tests. 
However, integrating SonarCloud would help generate code coverage reports for each Pull Request. This integration would prevent a PR from being merged if it fails to meet the required code coverage percentage.

### How to run the integration tests
From a terminal run:

```
docker-compose up
```
After the container is up and running, run the integration tests:
```
dotnet test PriceAggregator.IntegrationTests/PriceAggregator.IntegrationTests.csproj
```
