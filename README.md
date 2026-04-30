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

I kept this layer simple and did not include business logic here.

---

### Application

- Contains interfaces  
- Contains DTOs  

This helps reduce dependency between layers.

---

### Domain

- Stock  
- StockPrice  

This is where the core data structure is defined.

---

### Infrastructure

This is where the main operations are implemented.

#### Repository

- Handles database operations  
- Contains CRUD logic  

This layer communicates directly with the database.

---

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

I used SQL Server.

- Connected using Entity Framework Core  
- Created Stock and StockPrice tables  
- Stored price history  

---

## External API

I used Finnhub API.

- Fetching stock prices  
- Parsing the response  
- Saving data to database  

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

Average price calculation.

- Takes historical prices  
- Calculates average  

This shows that I can process data, not just store it.

---

## Error Handling

- Used try-catch in controllers  
- Did not expose raw exceptions  
- Returned clean responses  

---

## Security

API key is not stored in the project.

Used environment variable:

setx FINNHUB_API_KEY "YOUR_API_KEY"

---

## Running the Project

- Update connection string  
- Set API key  

Run:

dotnet run --project FinTrack.API

Swagger:

http://localhost:5025/swagger

---

## Frontend

In this project, I developed a simple user interface using React.

My goal was to consume the backend API I built and present the data to the user, while also allowing basic interactions through the UI.

On the frontend side, I implemented:

- Displaying the stock list
- Adding new stocks
- Deleting stocks
- Refreshing stock prices
- Showing last price and last updated time of each stock
- Displaying average price data
- Showing highest and lowest average values in a dashboard
- Adding a simple chart to visualize average prices

I used Axios to connect the React application to the backend API.

The frontend project is located in a separate folder:

### Running the frontend:

cd fintrack-ui
<br/>
npm install
<br/>
npm run dev

The frontend runs by default at:

http://localhost:5173

If the backend is not running, the frontend will not be able to fetch data.

---

## Summary

In this project I:

- Built a .NET Web API  
- Used Onion Architecture  
- Used SQL Server + EF Core  
- Integrated external API  
- Stored and processed data  
- Implemented analytics  
- Applied clean architecture principles  
