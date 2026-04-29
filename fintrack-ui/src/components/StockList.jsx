import { useEffect, useState } from "react";
import api from "../api";

function StockList() {
  const [stocks, setStocks] = useState([]);

  const fetchStocks = async () => {
    try {
      const res = await api.get("/stocks");
      setStocks(res.data);
    } catch (err) {
      console.error(err);
    }
  };

 useEffect(() => {
  const loadStocks = async () => {
    try {
      const res = await api.get("/stocks");
      setStocks(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  loadStocks();
}, []);

  const deleteStock = async (id) => {
    try {
      await api.delete(`/stocks/${id}`);
      fetchStocks();
    } catch (err) {
      console.error(err);
      alert("Silme hatası");
    }
  };

  const refreshPrice = async (symbol) => {
    try {
      await api.post(`/stocks/${symbol}/refresh-price`);
      alert("Fiyat güncellendi");
    } catch (err) {
      console.error(err);
      alert("Fiyat çekme hatası");
    }
  };

  return (
    <div>
      <h2>Stock List</h2>

      <table border="1">
        <thead>
          <tr>
            <th>Symbol</th>
            <th>Name</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {stocks.map((stock) => (
            <tr key={stock.id}>
              <td>{stock.symbol}</td>
              <td>{stock.name}</td>
              <td>
                <button onClick={() => refreshPrice(stock.symbol)}>
                  Refresh Price
                </button>

                <button onClick={() => deleteStock(stock.id)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default StockList;