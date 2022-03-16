# Setting up the project for local development

1.) Clone the project ```git clone git@github.com:fi-dotnet-alumni/AlumniNetworkAPI.git```

2.) Open Visual Studio > Select 'Open a project or solution' > Find the project directory and select the **AlumniNetworkAPI.sln** file

3.) In the terminal run the command ```dotnet restore``` to install the dependencies

4.) Set up the database

* Make sure you have a SQL Express server running locally
* In Visual Studio open up the user secrets file by right-clicking the project name in the solution explorer and selecting 'Manage user secrets'
* In the secrets.json file, add the connection string to your local database with the KEY **ConnectionString** e.g. 
```json
"ConnectionString": "Data Source= .\\SQLEXPRESS; Initial Catalog= AlumniNetwork; Integrated Security=True; Trust Server Certificate=True;",
```
* On the Visual Studio toolbar navigate to Tools > NuGet Package Manager > Package Manager Console. Open the console and run the command update-database. This will create the database on the server that was specified in the connection string.

5.) Set up Keycloak

* You can check [this](https://gitlab.com/noroff-accelerate/dotnet/project/keycloak-fullstack-demo/-/tree/master/) repo for some instructions
* After Keycloak is set up, you need to add the following config variables to secrets.json

```json
"TokenSecrets": {
    "KeyURI": "<your_keycloak_uri>/realms/<realm_name>/protocol/openid-connect/certs",
    "IssuerURI": "<your_keycloak_uri>/auth/realms/<realm_name>"
}
```

6.) Start the application. It should automatically open up your default browser on https://localhost:7068/ and display the swagger/index.html OpenAPI documentation

7.) In order to test the endpoints that require authorization, you need to provide a valid JWT token in the header of the request