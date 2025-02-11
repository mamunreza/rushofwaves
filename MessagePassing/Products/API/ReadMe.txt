cd Data
dotnet add package Microsoft.EntityFrameworkCore.Design

cd Data
dotnet ef migrations add InitialCreate --startup-project ../API

dotnet ef database update --startup-project ../API


# Todo
### API project
- Add MediatR in the 
- Add X-API-Key authentication in the API project
- Add Repository in the API project

### EventConsumer project
- Consume CustomerAdded from RabbitMQ
