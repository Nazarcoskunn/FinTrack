import { useEffect, useState } from "react";
import api from "../api";

function DashboardStats() {
  const [averages, setAverages] = useState([]);

  useEffect(() => {
    const fetchAverages = async () => {
      try {
        const res = await api.get("/stocks/average-prices");
        setAverages(res.data);
      } catch (err) {
        console.error(err);
      }
    };

    fetchAverages();
  }, []);

  const highest = averages.length
    ? [...averages].sort((a, b) => b.averagePrice - a.averagePrice)[0]
    : null;

  const lowest = averages.length
    ? [...averages].sort((a, b) => a.averagePrice - b.averagePrice)[0]
    : null;

  return (
    <div className="stats-grid">
      <div className="stat-card">
        <span>Highest Average</span>
        <h3>{highest ? highest.symbol : "-"}</h3>
        <p>{highest ? `$${highest.averagePrice}` : "-"}</p>
      </div>

      <div className="stat-card">
        <span>Lowest Average</span>
        <h3>{lowest ? lowest.symbol : "-"}</h3>
        <p>{lowest ? `$${lowest.averagePrice}` : "-"}</p>
      </div>
    </div>
  );
}

export default DashboardStats;