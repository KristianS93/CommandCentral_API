# CommandCentral API
API for the CommandCentral App

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


# Docker images
## API image
[API image](https://hub.docker.com/repository/docker/kristians93/command_central_api/general)
## Postgres image
[Postgres image](https://hub.docker.com/repository/docker/kristians93/command_central_postgres/general)


# Current Db Scheme
![dbScheme](https://github.com/KristianS93/CommandCentral_API/assets/82061735/aed788f5-3feb-4cc8-aeea-3e71b6b4c168)
