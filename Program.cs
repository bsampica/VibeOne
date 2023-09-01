﻿using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.LinuxFramebuffer;
using Avalonia.LinuxFramebuffer.Input.NullInput;
using Avalonia.ReactiveUI;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace VibeOne;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static int Main(string[] args)
    {
        var builder = BuildAvaloniaApp();

        if (args.Contains("--fbdev"))
        {
            SilenceConsole();
            return builder.StartLinuxFbDev(args);
        }
        else if (args.Contains("--direct"))
        {
            SilenceConsole();
            return builder.StartLinuxDirect(args, new FbdevOutput(), new NullInputBackend());
        }


        return builder.StartWithClassicDesktopLifetime(args);

    }

    private static void SilenceConsole()
    {
        // new Thread(() =>
        // {
        //     Console.CursorVisible = false;
        //     while (true)
        //         Console.ReadKey(true);
        // }) { IsBackground = true }.Start();
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            //With(new X11PlatformOptions() { EnableMultiTouch = true, UseDBusMenu = true, EnableIme = true })
            .LogToTrace()
            .UseReactiveUI();
}
