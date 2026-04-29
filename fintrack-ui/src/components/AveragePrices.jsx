import { useEffect, useState } from "react";
import api from "../api";

// This component shows average prices of stocks
// I use this to demonstrate that I can process data, not just fetch it

function AveragePrices({ refresh }) {
  const [averagePrices, setAveragePrices] = useState([]);

 useEffect(() => {
  const fetchAveragePrices = async () => {
    try {
      const res = await api.get("/stocks/average-prices");
      setAveragePrices(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  fetchAveragePrices();
}, [refresh]);
  return (
    <div>
      <h2>Average Prices</h2>

      <table border="1">
        <thead>
          <tr>
            <th>Symbol</th>
            <th>Average Price</th>
          </tr>
        </thead>

        <tbody>
          {averagePrices.map((item, index) => (
            <tr key={index}>
              <td>{item.symbol}</td>
              <td>{item.averagePrice}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default AveragePrices;