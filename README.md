# CommandCentral API
API for the CommandCentral App

# Setup 1

To run the API and database as a backend, start downloading the docker images.
Next use docker compose to build and run the application.
Before starting the containers, the first migrations needs to be addec, cd into the Infrastrcuture/Persistence folder and run:

`dotnet ef migrations add initalmigration --startup-project ../../WebApi/API`

When the migration is created proceed.

- Run the following:
  - `docker compose build`
- Then to run the images:
  - `docker compose up`
  - Or to compose in detached mode use:
  - `docker compose up -d`
- verify everything is running either in the browser using swagger or in postman, with the following url:
  - `http://localhost:8080/swagger/index.html`

# Testing strategy 
- Based on [MSDN: Choosing a testing strategy](https://learn.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy)
- Testing the API will follow 2 strategies
  - Strategy 1 - Moq for repository pattern 
    - [MSDN: Testing without DB](https://learn.microsoft.com/en-us/ef/core/testing/testing-without-the-database#repository-pattern)
    - This strategy involves mocking the repositories (or services in this project), to check whether the service implementation works as expected.
  - Strategy 2 - Testing the same Database system
    - [MSDN: Testing with DB](https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database)
    - This strategy involves full integration testing of the production database system, which is PostgreSQL.
    - Ensures that each controller outputs the correct info from the testing db.
  - The Tests solution has its own Dockerfile to instantiate the testing db locally
    - To use the Database follow these instructions:
      - cd to the directory of the Tests folder
      - Build the image
      - `docker build -t postgres-test .`
      - Run the image
      - `docker run --name postgres-test-env -p 5432:5432 -d postgres-test`

# Routing
will follow the general approach of REST api's

<img width="500" alt="intro-to-restful-routing-rest-routes" src="https://github.com/KristianS93/CommandCentral_API/assets/82061735/a27069f4-8b94-473d-a9fe-cfc34e5ee267">

# Docker images
## API image
[API image](https://hub.docker.com/repository/docker/kristians93/command_central_api/general)
## Postgres image
[Postgres image](https://hub.docker.com/repository/docker/kristians93/command_central_postgres/general)


# Current Db Scheme
<img width="500" alt="db-schema" src="https://github.com/KristianS93/CommandCentral_API/assets/82061735/aed788f5-3feb-4cc8-aeea-3e71b6b4c168">
