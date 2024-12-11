cd Data
dotnet add package Microsoft.EntityFrameworkCore.Design

cd Data
dotnet ef migrations add InitialCreate --startup-project ../API

dotnet ef database update --startup-project ../API