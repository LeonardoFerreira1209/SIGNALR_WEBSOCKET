using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Build.
var app = builder.Build();

// Aplicando o WebSocket.
app.UseWebSockets();

// Aplicação async.
app.Map("/", async context =>
{
    // Verifica se a requisição é async se não for volta um Bad Request.
    if (!context.WebSockets.IsWebSocketRequest) context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

    // Aceita a requisição de um webSocket.
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

// Rodando aplicação async.
await app.RunAsync();