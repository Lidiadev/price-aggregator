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


## Testing
The microservice has integration tests which are run by CircleCI every time a commit is 
pushed to the main branch.

The integration tests are using a standalone WireMock stub server orchestrated by docker-compose to mock the external APIs. 

### How to run the integration tests
From a terminal run:

```
docker-compose up
```
After the container is up and running, run the integration tests:
```
dotnet test PriceAggregator.IntegrationTests/PriceAggregator.IntegrationTests.csproj
```