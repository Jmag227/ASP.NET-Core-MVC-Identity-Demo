# ASP.NET-Core-Identity-Demo
An ASP.NET Core MVC web app that uses the IDentity DB for authentication and Dapper as the ORM.

- Fork and clone the project
- Then in your **Package Manager Console** window:
  - Run the `dir` command
  - Then `cd` into your Project that the **Startup.cs** file is located in
- Now run the **EF Core Migration Commands**:
  - `dotnet ef migrations add MyCommand1`
  - `dotnet ef database update`
- Once those commands complete, finish setting up your **appsettings.json** file with your **connection string**
- Then run the application
