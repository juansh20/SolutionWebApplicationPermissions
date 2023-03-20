# SolutionWebApplicationPermissions

NET CORE 6.0 with Entity Framework, SQL, ElasticSearch, KafkaProducerService

Prerequisites
.NET Core 6.0 SDK installed on your machine
A text editor or an IDE like Visual Studio Code or Visual Studio 2022 (recommended)
Dockers, wsl 2, kafka install on your virtual Linux server

Before to run the project:
- This project was developed using .NET Core 6.0.
- The project contains the necessary files and configurations to be built and run on any platform that supports .NET Core 6.0.
- To build and run the project, open a terminal or command prompt and navigate to the project directory. Then run the following commands:
dotnet restore
dotnet build
dotnet run
- The project includes a database connection string that needs to be updated to match your own database. You can update this in the appsettings.json file located in the root directory of the project.
- The project is configured to use the Entity Framework Core as an Object-Relational Mapping (ORM) tool.
- Excecute the script from /database scripts/Create database and tables.sql in order to create the database.
- Modify: DefaultConnection, ElasticsearchUrl, KafkaBootstrapServers on appsettings.Development.json or appsettings.json
- The project includes Swagger UI for API documentation. To view the API documentation, run the project and navigate to https://localhost:<port_number>/swagger. By default, the port number is 7198.
- The project uses Serilog for registering logs and error exceptions.
- The project uses xUnit for testing Unit.
