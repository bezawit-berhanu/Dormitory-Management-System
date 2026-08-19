import "dotenv/config";
import express from "express";
import cors from "cors";
import { handleDemo } from "./routes/demo";

export function createServer() {
  const app = express();

  // Middleware
  app.use(cors());
  const jsonParser = express.json();
  const urlEncodedParser = express.urlencoded({ extended: true });

  // Vite proxies /api requests to the .NET API. Do not read those request
  // bodies here: once consumed, the proxy forwards an empty body with the
  // original Content-Length and Kestrel eventually returns a 408 timeout.
  app.use((req, res, next) => {
    if (req.path.startsWith("/api")) return next();
    return jsonParser(req, res, next);
  });
  app.use((req, res, next) => {
    if (req.path.startsWith("/api")) return next();
    return urlEncodedParser(req, res, next);
  });

  // Example API routes
  app.get("/api/ping", (_req, res) => {
    const ping = process.env.PING_MESSAGE ?? "ping";
    res.json({ message: ping });
  });

  app.get("/api/demo", handleDemo);

  return app;
}
