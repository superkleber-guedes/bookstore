## Kleber - Bookstore - API

### Instructions

Kleber Bookstore is an API with a book collection that has 5 endpoints fo: Create, Update, Delete, GetById, and Get.
A CQRS pattern was used and therefore there's a separation on commands and queries.
Some comments were made in the code (as not everything is implemented and a couple of shortcuts were made)

### How to run it locally

You will need CosmosDb Emulator, 
For Windows:
    1. Follow the instructions outlined here - https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21
For Mac:
    1. Get Docker from here - https://docs.docker.com/docker-for-mac/install/
    2. Follow the instructions outlined here - https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21

You then, need to run the emulator.

Once the emulator is running, please change the values on appSettings.json in 2 projects, 
    API 
    and Infrastructure.IntegrationTests:

the settings looks like this (the settings bellow are already there):

```json
 "CosmosDb": {
    "EndpointUri": "https://localhost:8081/",
    "ConnectionString": "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
    "DatabaseName": "kleber-bookstore",
    "ContainerName": "books"
  }
```

Once your settings are in place, you need to create the database and container.

This solution has an integration test project, that does a side job as a database creator and feeder.
(Although this is not best practice, it helps other people setting up their local environment)

To feed the database with this data:

| Id  | Author  | Title  |  Price |
|----|---|---|---|
|  1 | A. A. Milne |  Winnie-the-Pooh | 19.25  |
|  2 | Jane Austen | Pride and Prejudice | 5.49  |
|  3 | William Shakespeare | Romeo and Juliet | 6.95 |

Please, Run the following integration test called "FeedDatabaseSaveAndGetTest".
When the test pass, you are ready to go.

You can then, run the API and have fun.

### Tests

Not all tests have been implemented, to save me some time, but everything works.
There is a integration test implemented that tests the only third party used, CosmosDb.
There are some Component tests that tests the API and mocks the repository (that depends on CosmosDb). Not all controllers have component tests implemented.
There are no unit tests (the projects are there though), to try and make this as simple as possible, and as they add very little business value, I chose to skip them.