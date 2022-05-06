using System.Net.WebSockets;
using System.Text;

// Instância de um client webSocket.
using var webSocket = new ClientWebSocket();

// Connectando ao servidor WebSocket para consumir.
await webSocket.ConnectAsync(new Uri("ws://localhost:5201/"), CancellationToken.None);

// declara um array de bytes.
var buffer = new byte[256];

// Enquanto a conexão estiver aberta iremos continuar rodando o while.(No caso sempre porque o Server tem um While(true))
while (webSocket.State == WebSocketState.Open)
{
    // recebe as mensagens do server webSocker.
    var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

    // Se o tipo da mensagem for de close fecha a conexão, se não imprimi as mensagens.
    if (result.MessageType == WebSocketMessageType.Close) await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Conexão encerrada", CancellationToken.None); else Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
}