import { useEffect, useState } from "react";
import api from "../api";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
} from "recharts";

// This component shows simple dashboard stats
// I calculate highest and lowest average prices to give quick insight
// This component shows simple dashboard stats
// I calculate highest and lowest average prices to give quick insight

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
    <div>
      <div className="stats-grid">
        <div className="stat-card">
          <span>📈 Highest Average</span>
          <h3>{highest ? highest.symbol : "-"}</h3>
          <p>{highest ? `$${highest.averagePrice}` : "-"}</p>
        </div>

        <div className="stat-card">
          <span>📉 Lowest Average</span>
          <h3>{lowest ? lowest.symbol : "-"}</h3>
          <p>{lowest ? `$${lowest.averagePrice}` : "-"}</p>
        </div>
      </div>

      <div className="chart-card">
        <h3>Average Price Chart</h3>

        {averages.length === 0 ? (
          <p>No average price data found.</p>
        ) : (
          // responsive chart for better UI
          <ResponsiveContainer width="100%" height={260}>
            <BarChart data={averages}>
                {/* X axis shows stock symbols */}
              <XAxis dataKey="symbol" />
               {/* Y axis shows price values */}
              <YAxis />
              <Tooltip />
              <Bar dataKey="averagePrice" fill="#3b82f6" />
            </BarChart>
          </ResponsiveContainer>
        )}
      </div>
    </div>
  );
}

export default DashboardStats;