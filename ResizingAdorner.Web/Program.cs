using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Web;

[assembly:SupportedOSPlatform("browser")]

namespace ResizingAdorner.Web;

internal partial class Program
{
    private static void Main(string[] args)
        => BuildAvaloniaApp().SetupBrowserApp("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}