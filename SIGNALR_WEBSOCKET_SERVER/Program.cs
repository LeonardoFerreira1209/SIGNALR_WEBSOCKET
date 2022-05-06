using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Build.
var app = builder.Build();

// Aplicando o WebSocket.
app.UseWebSockets();

// Aplica��o async.
app.Map("/", async context =>
{
    // Verifica se a requisi��o � async se n�o for volta um Bad Request.
    if (!context.WebSockets.IsWebSocketRequest) context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

    // Aceita a requisi��o de um webSocket.
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

    while(true)
    {

        // Declara um array de bytes.
        var data = Encoding.ASCII.GetBytes($" .NET Rocks -> {DateTime.Now}");

        // Envia mensagem utilizando o webSocket.
        await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);

        await Task.Delay(1000);
    }
});

// Rodando aplica��o async.
await app.RunAsync();