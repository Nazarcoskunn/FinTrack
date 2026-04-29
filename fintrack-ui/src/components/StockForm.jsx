import { useState } from "react";
import api from "../api";

// This component shows simple dashboard stats
// I calculate highest and lowest average prices to give quick insight


function StockForm({ onStockAdded }) {
  const [symbol, setSymbol] = useState("");
  const [name, setName] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await api.post("/stocks", {
        symbol,
        name,
      });

      setSymbol("");
      setName("");
      onStockAdded();
    } catch (err) {
      console.error(err);
      alert("Stock eklenirken hata oluştu");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>Stock Ekle</h3>

      <input
        type="text"
        placeholder="Symbol (AAPL)"
        value={symbol}
        onChange={(e) => setSymbol(e.target.value)}
      />

      <input
        type="text"
        placeholder="Name (Apple)"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />

      <button type="submit">Ekle</button>
    </form>
  );
}

export default StockForm;