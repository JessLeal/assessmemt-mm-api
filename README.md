# MM Backend API Service

## Link

FrontEnd Repository - https://github.com/JessLeal/assessment-mm-ui


## About

This is the backend service for MM assessment


## How to run

Download the repository

1. Using the terminal
  
    Open the terminal on the solution directory
  
    run $ dotnet run

2. Using Visual Sudio 2022
  
    Open the solution using visual studio
  
    Click on "Start Debugging" button
  
This will automatically open a swagger documentation page that contains all the endpoints
  
  
## How to generate redirect URL
1. Call the /api/loan endpoint with the loan details. 
 
  Sample request JSON:
 
    {
      "amount": 1500,
      "term": 40,
      "title": "Mr.",
      "firstName": "First",
      "lastName": "Last",
      "dateOfBirth": "2002-11-11T02:52:24.480Z",
      "mobile": "09123123123",
      "email": "string@string.com"
    }

2. Open the redirect url in a new browser tab. The redirect url uses the frontend endpoint so change it in the appsettings.json file if you have a different url

## Features
Generate a redirect URL based on the loan details

Calculate ammortization based on product type

Validate input values based on criteria set by the assessment

Provide error message to the user

Save all data to a sqlite database


## Endpoints

GET: api/loan/{accountId} - Get the loan based on the associated account id. This is used in the Calculator page of the front end

GET: api/repayments/{accountId} - Get the repayment details. This is used in the success page of the front end

POST: api/loan - Save the loan details to the database and generate the redirect URL. 
      This will also update the database if the same set of Fristname, lastname and date of birth are inputted

POST: api/repayments/{accountId} - Calculate the repayments based on the loan details. This wiil save the details if 'isSaveRepayment' is true


## Architecture

The application uses a typical Frontend-Backend pattern where the frontend communicates with the backend using API endpoints

The backend uses repository pattern to abstract repository implementation. Repositories (though controvertial) are easy to mock.

It also makes it easier to navigate to database related concerns since I can just look at one folder for all of them.


## Folder structure

- Constants - Holds the default values and enums used through out the application

- Controllers - API Controllers that define the http logic used by the application

- Data - Contains migration details and repository implementation

- DTOs - Contians both DTO definitions as well as DTO validation for API inputs

- Errors - Contains definition for Error objects

- Extensions - Provides the classes for auto mapper profiles and classes that extends the program.cs file

- Interface - Service and repository interfaces. Needed to implement depenency injection and makes unit testing easier

- Middlewares - Currently has the exception middleware for any unhandled errors but could contain other middleware (i.e. auth or logging)

- Model - Classes that define the structure of the entities directly related to the database

- Services - Implementation of business logic used by the application. This contains the Repayment Calculator and URL Generator services


## Other implementation features

### API input validation
  - The application uses FluentValidation to implement validation of the API input values

![image](https://user-images.githubusercontent.com/77286387/201254988-cb160c8e-d994-4cb0-bbe5-a7f9edc71897.png)

### Global error handling
  - The application uses an exception middleware to handle any unhandled exceptions and logs it in the terminal during development
  
 ![image](https://user-images.githubusercontent.com/77286387/201255576-8d9dcd2a-aa0b-4278-8654-926bcbcc5cf3.png)

### Unit Testing
  - Unit test framework is created for the application
  
 ![image](https://user-images.githubusercontent.com/77286387/201256114-3c3e6d8f-1dcb-4260-987e-c29ffe613f9d.png)


## Improvements
- Add a file logging middleware for development

- Use a better database

- Add more unit tests. Currently the application sits at around 30% code coverage which is not ideal for production level application

![image](https://user-images.githubusercontent.com/77286387/201256296-baca781e-6266-4d9a-82d5-a020fd4ed7a1.png)

- Introduce an integration testing framework

- Remove appsettings.json in github repository. This appsettings.json file contains sensitive secret and is only in github repository since this is an assessment
