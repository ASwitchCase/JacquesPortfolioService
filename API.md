# Jacques Portfolio Service API

This document describes the HTTP API exposed by `JacquesPortfolioService.Api`.

## Run locally

From the repository root:

```bash
dotnet run --project src/JacquesPortfolioService.Api --launch-profile https
```

The configured development URLs are:

- HTTPS: `https://localhost:7148`
- HTTP: `http://localhost:5169`

Use HTTPS for normal requests. The API redirects HTTP requests to HTTPS. The application applies pending Entity Framework Core migrations during startup, so the configured database must be available before the API starts.

In Development, the generated OpenAPI document is available at:

```text
https://localhost:7148/openapi/v1.json
```

## Authentication

Authentication uses JWT bearer tokens. Register a user, log in with that user's credentials, then send the returned `accessToken` in the `Authorization` header:

```http
Authorization: Bearer <access-token>
```

`GET /api/auth/me`, the skill endpoints, the portfolio project endpoints, and the work experience endpoints all require authentication.

## Endpoints

### Register

Creates a user account.

```http
POST /api/auth/register
Content-Type: application/json
```

Request body:

```json
{
  "email": "jane@example.com",
  "password": "correct-horse-battery-staple",
  "displayName": "Jane Doe"
}
```

Requirements:

- `email` is required and must be a valid email address.
- `password` is required and must contain at least 8 characters.
- `displayName` is required.

Successful response: `201 Created`

```json
{
  "id": "11111111-1111-1111-1111-111111111111",
  "email": "jane@example.com"
}
```

If the email is already registered, the response is `409 Conflict`.

### Log in

Authenticates a user and returns a JWT.

```http
POST /api/auth/login
Content-Type: application/json
```

Request body:

```json
{
  "email": "jane@example.com",
  "password": "correct-horse-battery-staple"
}
```

Successful response: `200 OK`

```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAtUtc": "2026-09-10T12:00:00+00:00"
}
```

Invalid credentials return `401 Unauthorized`. The configured token lifetime is 60 minutes by default.

### Get current user claims

Returns the claims from the authenticated user's JWT.

```http
GET /api/auth/me
Authorization: Bearer <access-token>
```

Successful response: `200 OK`

```json
[
  { "type": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "value": "..." },
  { "type": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "value": "jane@example.com" },
  { "type": "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "value": "User" }
]
```

### Create a skill

Creates a skill.

```http
POST /api/skills
Content-Type: application/json
Authorization: Bearer <access-token>
```

Request body:

```json
{
  "skillName": "C#"
}
```

`skillName` is required.

Successful response: `201 Created`

```json
{
  "id": "22222222-2222-2222-2222-222222222222",
  "name": "C#",
  "createdAt": "2026-09-10T10:30:00Z"
}
```

The `Location` header points to the new skill's `GET /api/skills/{id}` URL.

### Get a skill

Returns a skill by its GUID.

```http
GET /api/skills/{id}
```

Example:

```bash
curl https://localhost:7148/api/skills/22222222-2222-2222-2222-222222222222
```

Successful response: `200 OK` with the skill representation shown above. If the skill does not exist, the response is `404 Not Found`.

### Create a work experience

Creates a work experience entry.

```http
POST /api/workexperiences
Content-Type: application/json
Authorization: Bearer <access-token>
```

Request body:

```json
{
  "company": "Acme Corp",
  "title": "Software Engineer",
  "startDate": "2022-01-01T00:00:00Z",
  "endDate": null,
  "description": "Built and maintained backend services.",
  "location": "Remote"
}
```

Requirements:

- `company` is required.
- `title` is required.
- `startDate` is required.
- `endDate` is optional; omit or set to `null` for a current role.
- `description` is required.
- `location` is required.

Successful response: `201 Created`

```json
{
  "id": "33333333-3333-3333-3333-333333333333",
  "company": "Acme Corp",
  "title": "Software Engineer",
  "startDate": "2022-01-01T00:00:00Z",
  "endDate": null,
  "description": "Built and maintained backend services.",
  "location": "Remote",
  "createdAt": "2026-09-11T17:32:00Z"
}
```

The `Location` header points to the new work experience's `GET /api/workexperiences/{id}` URL.

### Get a work experience

Returns a work experience entry by its GUID.

```http
GET /api/workexperiences/{id}
Authorization: Bearer <access-token>
```

Successful response: `200 OK` with the representation shown above. If the work experience does not exist, the response is `404 Not Found`.

### List work experiences

Returns all work experience entries.

```http
GET /api/workexperiences
Authorization: Bearer <access-token>
```

Successful response: `200 OK` with a JSON array of work experience representations.

### Update a work experience

Replaces a work experience entry's details.

```http
PUT /api/workexperiences/{id}
Content-Type: application/json
Authorization: Bearer <access-token>
```

Request body: same shape as create. The `id` in the URL is authoritative; any id in the body is ignored.

Successful response: `200 OK` with the updated representation. If the work experience does not exist, the response is `404 Not Found`.

### Delete a work experience

Deletes a work experience entry.

```http
DELETE /api/workexperiences/{id}
Authorization: Bearer <access-token>
```

Successful response: `204 No Content`. If the work experience does not exist, the response is `404 Not Found`.

## End-to-end example

```bash
curl -k -X POST https://localhost:7148/api/auth/register \
  -H 'Content-Type: application/json' \
  -d '{"email":"jane@example.com","password":"correct-horse-battery-staple","displayName":"Jane Doe"}'

curl -k -X POST https://localhost:7148/api/auth/login \
  -H 'Content-Type: application/json' \
  -d '{"email":"jane@example.com","password":"correct-horse-battery-staple"}'

curl -k https://localhost:7148/api/auth/me \
  -H 'Authorization: Bearer <access-token>'

curl -k -X POST https://localhost:7148/api/skills \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer <access-token>' \
  -d '{"skillName":"C#"}'

curl -k -X POST https://localhost:7148/api/workexperiences \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer <access-token>' \
  -d '{"company":"Acme Corp","title":"Software Engineer","startDate":"2022-01-01T00:00:00Z","endDate":null,"description":"Built and maintained backend services.","location":"Remote"}'
```

The `-k` option allows curl to use the local development HTTPS certificate. Do not use it for production requests.

## Error handling

Unhandled exceptions are returned using ASP.NET Problem Details. The API maps missing resources to `404`, forbidden operations to `403`, and unexpected exceptions to `500`.

Request validation rules are defined for the auth and skill commands. Clients should validate those rules before sending requests. At present, validation failures are handled by the application's exception pipeline and may be returned as `500`; this should be treated as current implementation behavior rather than a stable client contract.

## Configuration

The API requires JWT configuration, including a signing secret that is not committed to the repository. Configure `Jwt:Secret` using user secrets or an environment-specific configuration source. The default issuer and audience are both `JacquesPortfolioService`, and the default expiry is 60 minutes.
