# CommandCentral API
API for the CommandCentral App

To run the API and database as a backend, start downloading the docker images.
Next use docker compose to build and run the application.

- Run the following:
  - `docker compose build`
- Then to run the images:
  - `docker compose up`
- verify everything is running either in the browser using swagger or in postman, with the following url:
  - `http://localhost:8080/swagger/index.html`


# Docker images
## API image
[API image](https://hub.docker.com/repository/docker/kristians93/command_central_api/general)
## Postgres image
[Postgres image](https://hub.docker.com/repository/docker/kristians93/command_central_postgres/general)

# Databse Schema
## Household
The household is what connects users to data, in this case the household will be a wrapper for things that should be common for the household.
- Scheme
  - id, serial integer, primary key   
## Grocery list
- Grocery list Scheme
  - id, serial primary key
  - household, foreign key reference household.id unique constraint
- Grocery item Scheme
  - id, serial primary key
  - grocery_list_id, foreign key reference grocery_list.id
