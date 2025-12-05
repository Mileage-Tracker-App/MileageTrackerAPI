# Mileage Tracker API

A RESTful API for tracking vehicle mileage and expenses with a hierarchical structure: Sessions → Logs → Log Items.

## Database Structure

```
Session (Root container)
├── Log (Vehicle tracking period)
│   └── LogItem (Individual mileage/expense entry)
```

### Models

**Session**
- `Id` - Unique identifier
- `Logs` - Collection of logs within this session

**Log**
- `Id` - Unique identifier
- `SessionId` - Foreign key to parent session
- `Vehicle` - Vehicle name/identifier
- `Description` - Log description
- `StartDate` - When this log started
- `LogItems` - Collection of log items
- `NumberOfLogItems` - Computed count of items
- `TotalMiles` - Computed sum of all miles

**LogItem**
- `Id` - Unique identifier
- `LogId` - Foreign key to parent log
- `Date` - Entry date
- `Miles` - Miles driven
- `Description` - Entry description
- `IsGas` - Whether this is a gas purchase
- `Gallons` - (Optional) Gallons purchased
- `PricePerGallon` - (Optional) Price per gallon
- `TotalCost` - Computed cost (Gallons × PricePerGallon)

## API Endpoints

### Sessions

#### Get All Sessions
```http
GET /api/sessions
```
Returns all sessions with their logs and log items.

#### Get Session by ID
```http
GET /api/sessions/{sessionId}
```
Returns a specific session with all nested data.

#### Create Session
```http
POST /api/sessions
```
Creates a new empty session.

**Response:** `201 Created` with session object

#### Delete Session
```http
DELETE /api/sessions/{sessionId}
```
Deletes a session and all its logs and log items (cascade delete).

**Response:** `200 OK` with success message

#### Get Logs for Session
```http
GET /api/sessions/{sessionId}/logs
```
Returns all logs within a specific session.

---

### Logs

Base route: `/api/sessions/{sessionId}/logs`

#### Get All Logs in Session
```http
GET /api/sessions/{sessionId}/logs
```
Returns all logs for the specified session with their log items.

#### Get Log by ID
```http
GET /api/sessions/{sessionId}/logs/{logId}
```
Returns a specific log with its log items.

#### Create Log
```http
POST /api/sessions/{sessionId}/logs
Content-Type: application/json

{
  "vehicle": "Honda Civic",
  "description": "Monthly tracking",
  "startDate": "2025-01-01T00:00:00Z"
}
```
Creates a new log within the specified session.

**Response:** `201 Created` with log object

#### Update Log
```http
PUT /api/sessions/{sessionId}/logs/{logId}
Content-Type: application/json

{
  "vehicle": "Honda Civic",
  "description": "Updated description",
  "startDate": "2025-01-01T00:00:00Z"
}
```
Updates an existing log's properties.

**Response:** `200 OK` with updated log

#### Delete Log
```http
DELETE /api/sessions/{sessionId}/logs/{logId}
```
Deletes a log and all its log items (cascade delete).

**Response:** `200 OK` with success message

---

### Log Items

Base route: `/api/sessions/{sessionId}/logs/{logId}/items`

#### Get All Log Items
```http
GET /api/sessions/{sessionId}/logs/{logId}/items
```
Returns all log items for the specified log.

#### Get Log Item by ID
```http
GET /api/sessions/{sessionId}/logs/{logId}/items/{itemId}
```
Returns a specific log item.

#### Create Log Item
```http
POST /api/sessions/{sessionId}/logs/{logId}/items
Content-Type: application/json

{
  "date": "2025-01-15T00:00:00Z",
  "miles": 125.5,
  "description": "Trip to work",
  "isGas": false
}
```

Or for a gas purchase:
```http
POST /api/sessions/{sessionId}/logs/{logId}/items
Content-Type: application/json

{
  "date": "2025-01-15T00:00:00Z",
  "miles": 0,
  "description": "Gas fill-up",
  "isGas": true,
  "gallons": 12.5,
  "pricePerGallon": 3.45
}
```

**Response:** `201 Created` with log item object (includes computed `totalCost` if gas)

#### Update Log Item
```http
PUT /api/sessions/{sessionId}/logs/{logId}/items/{itemId}
Content-Type: application/json

{
  "date": "2025-01-15T00:00:00Z",
  "miles": 130.0,
  "description": "Updated trip",
  "isGas": false,
  "gallons": null,
  "pricePerGallon": null
}
```
Updates an existing log item.

**Response:** `200 OK` with updated log item

#### Delete Log Item
```http
DELETE /api/sessions/{sessionId}/logs/{logId}/items/{itemId}
```
Deletes a specific log item.

**Response:** `200 OK` with success message

---

## Response Status Codes

- `200 OK` - Request succeeded
- `201 Created` - Resource created successfully
- `404 Not Found` - Resource not found (Session, Log, or LogItem)
- `400 Bad Request` - Invalid request data

## Features

- **Hierarchical Structure** - Clear parent-child relationships
- **Cascade Deletes** - Deleting a session removes all logs and items; deleting a log removes all items
- **RESTful Routes** - Intuitive URL structure following REST conventions
- **Computed Properties** - Automatic calculation of total costs, miles, and item counts
- **Data Validation** - Foreign key constraints ensure data integrity
