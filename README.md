### Overview
Price Aggregator is a a micro-service for aggregation, retrieval & caching of financial
instruments prices.

The micro-service is providing the following endpoints:
- an API Endpoint to request the aggregated instrument price at a specific time point
with hour accuracy.
  - The aggregated price will be served from the datastore if it’s already available
  - If not available, the prices will be requested from the external sources, then
  aggregated, then persisted & returned by the endpoint
- an API Endpoint that fetches the persisted instrument prices from the datastore
  during a user-specified time range 

### Technical details
- .NET 7.0
- In-memory DB
- RESTful API

### How to run

### Testing
The micro-service has integration tests which are run by CircleCI every time a commit is 
pushed to the main branch.