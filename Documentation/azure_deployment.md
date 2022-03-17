# Setting up the project for Azure deployment

1.) Fork the project on GitHub or have the source code in a repository where you have admin privileges

2.) Next you will need an Azure account and then you'll have to sign in to [Azure Portal](https://portal.azure.com/)

3.) Setting up the database
* Create a new SQL Database by clicking 'Create a resource' > Databases > SQL Database
* During the creation, in the Networking tab, make sure to switch on the option for 'Allow Azure services and resources to access this server'. This will allow the application to connect to the database without having to explicitly whitelist the virtual IP of the app service
* After successfully creating the database instance, navigate to Settings > Connection Strings to find the database connection string

4.) Set up Keycloak

* You can check [this](https://gitlab.com/noroff-accelerate/dotnet/project/keycloak-fullstack-demo/-/tree/master/) repo for some instructions
* Keycloak can be deployed to e.g Azure or Heroku

5.) Create a new Azure App Service instance for the application by clicking 'Create a resource' > Web > Web App. In the Basics tab, set the Publish option to Code and pick .NET 6 for the Runtime stack

6.) After the app service instance has been created, navigate to Settings > Configuration. In the Application Settings tab, click the 'New application setting' button and add the following key-value pairs:

```
Name: ConnectionString
Value: <your_database_connection_string>
```

```
Name: TokenSecrets:KeyURI
Value: <your_keycloak_uri>/realms/<realm_name>/protocol/openid-connect/certs
```

```
Name: TokenSecrets:IssuerURI
Value: <your_keycloak_uri>/auth/realms/<realm_name>
```

Optionally, if you want to see the Swagger OpenAPI documentation when navigating to the swagger/index.html route, make sure to include the following app setting

```
Name: ASPNETCORE_ENVIRONMENT
Value: Development
```

6.) Set up automatic deployment from GitHub
* Navigate to Deployment > Deployment Center and add a new deployment method
* Select GitHub under Continuous deployment, log in with your GitHub account and find the API project repository
* By giving Azure access to the repo, it can auto-generate an appropriate GitHub Actions workflow file that will push the main branch to Azure whenever there are new changes
* The previous workflow can be deleted from the repository
* The deployment process might take a few minutes, but afterwards the app should be up and running

7.) You can start using the API routes normally, but due to the authentication configuration, most endpoints require a valid JWT token issued by the Keycloak instance in the header of the request to work