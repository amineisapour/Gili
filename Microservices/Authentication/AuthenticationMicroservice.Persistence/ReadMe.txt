Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools

in PM: (AuthenticationMicroservice.Persistence)
Add-Migration InitialMigration -context AuthenticationMicroservice.Persistence.DatabaseContext
Update-Database -context AuthenticationMicroservice.Persistence.DatabaseContext

OR

PM> Add-Migration mig333 -Project WebAutomationSystem.DataModelLayer