# KaseyWebApi

- Having fun with dotnet and C# making a WebApi service that implements Entity Framework for database transactions,
  Swagger OpenApi, and JWT Bearer Token Authentication. A great place to learn how to
  use [Dependency Injection (DI)](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) to
  achieve [Inversion of Control (IoC)](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion)
  in ASP.NET.

Built on `ASP.NET 6.0`

## To run this project

- This project makes use of the ASP.NET
  Core [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux)
  . First, you will need to enable the `Secrets Manager Tool` to create a `userSecretId` for this project on your
  machine.

```shell
dotnet user-secrets init
```

- Next, you will need to update the [newSecrets.json](./newSecrets.json) file with your credentials. We will use this to
  set all necessary secrets at the same time with the following command.

```shell
cat ./newSecrets.json | dotnet user-secrets set
```

- You can use `dotnet user-secrets list` to verify that all secrets were set properly before running the project with
  either of the next commands.

```shell
dotnet run
```

OR

```shell
dotnet watch
```

- Running `dotnet watch` will automatically open `Swagger UI` in your browser and watch the codebase for changes.

## Authentication

- This Web Api Service requires you to authenticate yourself with a `JWT Bearer Token`, you can provision one by making
  a `POST` request to http://localhost:5227/api/v1/Token

Via `Curl`:

```shell
curl --request POST \
  --url http://localhost:5227/api/v1/Token \
  --header 'Content-Type: application/json' \
  --data '{
	"email": "<YOUR_USER_EMAIL>",
	"password": "<YOU_PASSWORD>"
}'
```

Via `HTTP`:

```http request
POST /api/v1/Token HTTP/1.1
Content-Type: application/json
Host: localhost:5227
Content-Length: 68

{
	"email": "<YOUR_USER_EMAIL>",
	"password": "<YOU_PASSWORD>"
}
```

### The response will contain your `JWT Bearer Token`:

```json5
{
	"encryptedApiKey": "eyJh12345678IsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIwOTZjYWVhYS00N2QwLTRhNmMtOGM2YS0xODliNzEwYTRjYzciLCJpYXQiOiI3LzEvMjAyMiA2OjQ5OjIwIFBNIiwiVXNlcklkIjoiNyIsIkRpc3BsYXlOYW1lIjoiS1BDIiwiVXNlck5hbWUiOiJLYXNleVBDYW50dSIsIkVtYWlsIjoia2FzZXlwYXVsY2FudHVAZ21haWwuY29tIiwiZXhwIjoxNjcyNTk4OTYwLCJpc3MiOiJKV1RBdXRoZW50aWNhdGlvblNlcnZlciIsImF1ZCI6IkpXVFNlcnZpY2VQb3N0bWFuQ2xpZW50In0.EewdI5ot2S1Su-LUq0-OEmr-fdTvcpmHu22aprsPGo8"
}
```

> ATTN: You will need to add an `Authorization` header whose value is `Bearer <YOUR_JWT_TOKEN>` when making requests to
> any of the following endpoints in this service.

### Services available at http://localhost:5227/api/v1

- `/Employees`
- `/GitHub`
- `/Links`
- `/Token`
- `/Users`
- `/Values`
- `/WeatherForecast`

## Swagger UI

- Visit http://localhost:5227/swagger/index.html to access Swagger UI. This will automatically open when
  running `dotnet watch`.
