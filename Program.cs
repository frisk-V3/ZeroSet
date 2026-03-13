using System.Runtime.InteropServices;
using System.Diagnostics;

Console.WriteLine($"🚀 Starting setup on {RuntimeInformation.OSDescription}...");

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    // Windows: winget を使用
    RunInstall("winget", "install --id Google.Chrome --silent");
    RunInstall("winget", "install --id Microsoft.VisualStudioCode --silent");
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    // Mac: Homebrew を使用
    RunInstall("brew", "install --cask google-chrome");
    RunInstall("brew", "install --cask visual-studio-code");
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    // Linux: apt を使用 (Ubuntu/Debian系)
    RunInstall("sudo", "apt update");
    RunInstall("sudo", "apt install -y curl git");
}

void RunInstall(string command, string args)
{
    Console.WriteLine($"📦 Executing: {command} {args}");
    var process = Process.Start(new ProcessStartInfo
    {
        FileName = command,
        Arguments = args,
        UseShellExecute = false
    });
    process?.WaitForExit();
}

Console.WriteLine("\n✨ Setup complete!");
