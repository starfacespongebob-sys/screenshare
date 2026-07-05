const WebSocket = require("ws");

const wss = new WebSocket.Server({ port: 8080 });
let viewer = null;

wss.on("connection", ws => {
  ws.on("message", data => {
    if (viewer) viewer.send(data);
  });

  ws.on("close", () => {
    if (ws === viewer) viewer = null;
  });

  // First client becomes viewer
  if (!viewer) viewer = ws;
});
