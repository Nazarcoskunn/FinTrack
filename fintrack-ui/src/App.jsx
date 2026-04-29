import { useState } from "react";
import StockForm from "./components/StockForm";
import StockList from "./components/StockList";
import AveragePrices from "./components/AveragePrices";


function App() {
  const [refresh, setRefresh] = useState(false);

  const handleStockAdded = () => {
    setRefresh(!refresh);
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>FinTrack</h1>

      <StockForm onStockAdded={handleStockAdded} />

      <StockList key={refresh} />
      <AveragePrices key={refresh} />
    </div>
  );
}

export default App;