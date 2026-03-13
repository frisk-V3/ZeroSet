using System.Diagnostics;
using System.Security.Principal;

// 1. 管理者権限チェック（レジストリやシステム設定に必須）
if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
{
    Console.WriteLine("❌ 管理者権限で実行してください。");
    return;
}

Console.WriteLine("🚀 --- My Ultimate PC Setup Start --- 🚀");

// 2. インストールしたいアプリ（winget ID）
var apps = new[] {
    "Google.Chrome",
    "Microsoft.VisualStudioCode",
    "Git.Git",
    "Discord.Discord",
    "Docker.DockerDesktop" // 重いのも入れちゃう
};

foreach (var app in apps)
{
    Console.Write($"📦 Installing {app}... ");
    var process = Process.Start(new ProcessStartInfo
    {
        FileName = "winget",
        Arguments = $"install --id {app} --silent --accept-source-agreements --accept-package-agreements",
        CreateNoWindow = true,
        RedirectStandardOutput = true
    });
    process?.WaitForExit();
    Console.WriteLine(process?.ExitCode == 0 ? "✅ Success!" : "⚠️ Skip/Error");
}

// 3. Windowsの「こだわり設定」をレジストリで一括変更
Console.WriteLine("🛠️  Applying Windows tweaks...");
// 例：エクスプローラーで拡張子を常に表示
Process.Start("reg", @"add HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced /v HideFileExt /t REG_DWORD /d 0 /f");

// 4. 開発用フォルダの作成
string devPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Source", "Repos");
if (!Directory.Exists(devPath)) Directory.CreateDirectory(devPath);

Console.WriteLine("\n✨ --- 全てのセットアップが完了しました！ --- ✨");
Console.ReadKey();
