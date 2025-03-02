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
  - Fix docker
  - Update Customer information in the database by job from inbox
  - Integrate polly for retry policy

### EventPublisher project
  - Refactor service names in the docker compose file
  _ Add unit tests for the event publisher

Sample CustomerAdded event:
{
  "Id": "d290f1ee-6c54-4b01-90e6-d701748f0851",
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "john.doe@example.com",
  "Phone": "555-1234",
  "CreatedAt": "2023-10-01T12:34:56Z",
  "UpdatedAt": "2023-10-01T12:34:56Z"
}