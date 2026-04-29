import { useEffect, useState } from "react";
import api from "../api";

function StockList() {
  const [stocks, setStocks] = useState([]);

  useEffect(() => {
    const fetchStocks = async () => {
      try {
        const res = await api.get("/stocks");
        setStocks(res.data);
      } catch (err) {
        console.error(err);
      }
    };

    fetchStocks();
  }, []);

  const reloadStocks = async () => {
    try {
      const res = await api.get("/stocks");
      setStocks(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  const deleteStock = async (id) => {
    try {
      await api.delete(`/stocks/${id}`);
      reloadStocks();
    } catch (err) {
      console.error(err);
      alert("Delete error");
    }
  };

  const refreshPrice = async (symbol) => {
    try {
      await api.post(`/stocks/${symbol}/refresh-price`);
      reloadStocks();
    } catch (err) {
      console.error(err);
      alert("Price refresh error");
    }
  };

  return (
    <div>
      <h2>Stocks</h2>

      <table>
        <thead>
          <tr>
            <th>Symbol</th>
            <th>Name</th>
            <th>Last Price</th>
            <th>Last Updated</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {stocks.map((stock) => (
            <tr key={stock.id}>
              <td>{stock.symbol}</td>
              <td>{stock.name}</td>
              <td>{stock.lastPrice ? `$${stock.lastPrice}` : "-"}</td>
              <td>
                {stock.lastUpdated
                  ? new Date(stock.lastUpdated).toLocaleString()
                  : "-"}
              </td>
              <td>
                <button  className="btn btn-success" onClick={() => refreshPrice(stock.symbol)}>
                  Refresh
                </button>

                <button className="btn btn-danger" onClick={() => deleteStock(stock.id)}>
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