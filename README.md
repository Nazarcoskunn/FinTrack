# FinTrack

A fullstack financial data tracking and analysis application built with .NET and React.

---

## Project Purpose

The goal of this project is to fetch financial data from an external API, store it in a database, and perform basic analysis on that data.

I didn’t just want to fetch data, I also wanted to show that I can process it and produce meaningful results.

At the same time, I wanted to demonstrate how a real backend application is structured, including layered architecture, API usage, data storage, and error handling.

---

## Architecture

I used Onion Architecture in this project.

The reason I chose this is to keep the code organized and separate responsibilities between layers.

---

## Layers

### API
- Handles incoming HTTP requests
- Calls the service layer
- Returns responses
- No business logic is implemented here

### Application
- Contains interfaces
- Contains DTOs
- Helps reduce dependency between layers

### Domain
- Stock
- StockPrice
- Defines the core data structure

### Infrastructure

#### Repository
- Handles database operations
- Contains CRUD logic
- Communicates directly with the database

#### Service
- Contains business logic
- Fetches data from external API
- Processes data
- Performs analytics

Controllers do not access the database directly, everything goes through the service layer.

---

## Repository / Service Separation

- Repository → data access
- Service → business logic

This separation makes the code:
- More organized
- Easier to understand
- Easier to maintain

---

## Database

- SQL Server is used
- Connected using Entity Framework Core
- Stock and StockPrice tables created
- Price history is stored

---

## External API

Finnhub API is used.

- Fetching stock prices
- Parsing API responses
- Saving data to the database

---

## Endpoints

GET     /api/stocks  
POST    /api/stocks  
DELETE  /api/stocks/{id}  
POST    /api/stocks/{symbol}/refresh-price  
GET     /api/stocks/top-gainers  
GET     /api/stocks/top-losers  
GET     /api/stocks/average-prices  

---

## Analytics

I added a simple analysis:

- Average price calculation

This shows that I can process data, not just store it.

---

## Error Handling

- Used try-catch in controllers
- Did not expose raw exceptions
- Returned clean responses

---

## Security

The API key is not stored in the project.

It is handled using an environment variable:

$env:FINNHUB_API_KEY="YOUR_API_KEY"

---

## Running the Backend

Make sure SQL Server is installed and running.

### 1. Configure the database connection

Open:

FinTrack.API/appsettings.json

Update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=FinTrackDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

Replace YOUR_SERVER with your own SQL Server name.

---

### 2. Set the API key

$env:FINNHUB_API_KEY="YOUR_API_KEY"

---

### 3. Apply migrations

dotnet ef database update --project FinTrack.Infrastructure --startup-project FinTrack.API

---

### 4. Run backend

dotnet run --project FinTrack.API

---

### 5. Swagger

http://localhost:5025/swagger

---

## Frontend

I developed a simple UI using React.

Features:

- Display stock list
- Add stock
- Delete stock
- Refresh stock price
- Show last price and update time
- Show average prices
- Show highest / lowest values
- Chart visualization

Axios is used for API communication.

---

## Running the Frontend

cd fintrack-ui  
npm install  
npm run dev  

Frontend runs at:

http://localhost:5173

The backend must be running for the frontend to work.

---

## Summary

In this project I:

- Built a .NET Web API
- Used Onion Architecture
- Used SQL Server + EF Core
- Integrated external API
- Stored and processed data
- Implemented analytics
- Built a React frontend
