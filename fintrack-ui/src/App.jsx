import { useState } from "react";
import StockForm from "./components/StockForm";
import StockList from "./components/StockList";
import AveragePrices from "./components/AveragePrices";
import "./App.css";


function App() {
  const [refresh, setRefresh] = useState(false);

  const handleStockAdded = () => {
    setRefresh(!refresh);
  };

return (
  <div className="app">
    <div className="header">
      <h1>FinTrack</h1>
      <p>Financial data tracking and analysis dashboard</p>
    </div>

    <div className="card">
      <StockForm onStockAdded={handleStockAdded} />
    </div>

    <div className="card">
      <StockList key={refresh} />
    </div>

    <div className="card">
   <AveragePrices refresh={refresh} /> 
    </div>
  </div>
);
}

export default App;