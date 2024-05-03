using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;

[assembly:SupportedOSPlatform("browser")]

namespace ResizingAdorner.Web;

internal partial class Program
{
    private static async Task Main(string[] args)
        => await BuildAvaloniaApp().SetupBrowserAppAsync();

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}
