import { useState } from "react";
import StockForm from "./components/StockForm";
import StockList from "./components/StockList";
import DashboardStats from "./components/DashboardStats";
import AveragePrices from "./components/AveragePrices";
import "./App.css";

// Main application component
// Here I combine all relationships and merge components

function App() {
  const [refresh, setRefresh] = useState(false);

  // Triggered when new stock is added
  // Re-rendering the list by changing the state
  const handleStockAdded = () => {
    setRefresh(!refresh);
  };

  return (
    <div className="app">
      <div className="header">
        <h1>📊 FinTrack</h1>
        <br />
        <p>Financial data tracking and analysis dashboard</p>
      </div>

      {/* Stock addition form */}
      <div className="card">
        <StockForm onStockAdded={handleStockAdded} />
      </div>
      {/* Stock lists */}
      <div className="card">
        <StockList key={refresh} />
      </div>
      {/* Area showing average prices */}
      <div className="card">
        <AveragePrices refresh={refresh} />
      </div>
      {/* Dashboard statistics */}
      <div className="card">
        <DashboardStats />
      </div>
    </div>
  );
}

export default App;
