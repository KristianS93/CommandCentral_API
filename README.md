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


Household and grocerylist schema idea:
-- Create Household table
CREATE TABLE Household (
    HouseholdID INT PRIMARY KEY,
    Name VARCHAR(255)
);

-- Create Grocerylist table
CREATE TABLE Grocerylist (
    GrocerylistID INT PRIMARY KEY,
    HouseholdID INT,
    CreationDate DATE,
    FOREIGN KEY (HouseholdID) REFERENCES Household(HouseholdID) ON DELETE CASCADE
);

-- Create GrocerylistItem table
CREATE TABLE GrocerylistItem (
    GrocerylistItemID INT PRIMARY KEY,
    GrocerylistID INT,
    ItemName VARCHAR(255),
    FOREIGN KEY (GrocerylistID) REFERENCES Grocerylist(GrocerylistID) ON DELETE CASCADE
);
