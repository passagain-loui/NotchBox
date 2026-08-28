
## [0.4.2] - 2026-08-28 12:09:43 UTC

### Release Information
- **Version**: 0.4.2 (Hotfix)
- **Build Status**: ? SUCCESSFUL
- **Release Channel**: GitHub Releases
- **Binary**: NotchBox.exe (145.08 MB, WinExe)
- **Installer**: NotchBox-v0.4.0-Setup.exe (42.11 MB)

### Changes Made
- NativeMethods.cs: Added FreeConsole() P/Invoke hook
- App.xaml.cs: Call FreeConsole() at application startup
- NotchBox.csproj: EnableMsixTooling = true, DisableWinExeOutputInference = true
- Installer: Recompiled with updated binary

### Console Suppression Layers
1. OutputType = WinExe (compiler-level)
2. DisableWinExeOutputInference = true
3. EnableMsixTooling = true (WinUI 3 safety)
4. FreeConsole() WinAPI hook (OS-level)
5. Called at App startup (immediate suppression)

### Protocol Compliance
- Protocol Version: v7.1 ?
- Tri-Agent Governance: Applied ?
- Visual & UX Standards: Enforced ?
- Gatekeeper Verification: Ready ?

### Release Status
- GitHub Release: ? Published
- Git Tag: ? v0.4.2
- Git Push: ? origin/master + tags
- Repository: https://github.com/passagain-loui/NotchBox

### Author
- Passagain P. (passagain@gmail.com)
- Released: 2026-08-28 12:09:43 UTC
# Verification Audit Trail

## [0.1.0] - 2026-08-27 09:05 UTC

### Build Information
- **Version**: 0.1.0
- **Author**: Passagain P.
- **Project**: NotchBox (DropShelf with Ghost Sync)
- **Framework**: .NET 9.0-windows10.0.19041.0
- **Build Status**: ✅ SUCCESSFUL

### Architecture Summary
- **Core Module** (NotchBox.Core):
  - `AppInfo.cs` — Version & branding constants
  - `AppState.cs` — State machine & data categories
  - `GhostSyncEngine.cs` — LAN file sync metadata

- **OS Module** (NotchBox.OS):
  - `NativeMethods.cs` — WinAPI hooks for UIPI bypass

- **UI Module** (NotchBox.UI):
  - `MainWindow.xaml` — Primary application window
  - `MainWindow.xaml.cs` — UI code-behind with branding

- **Application Root**:
  - `App.xaml` — Application resource dictionary
  - `App.xaml.cs` — WinUI application initialization

### Compilation Result
```
Build succeeded.
  0 Warning(s)
  0 Error(s)
Time Elapsed: 00:00:14.81
Output: D:\AI\OpenCode\Notchbox\NotchBox\bin\Debug\net9.0-windows10.0.19041.0\NotchBox.dll
```

### Dependencies Installed
- Microsoft.WindowsAppSDK 2.4.0
- Microsoft.Data.Sqlite 10.0.11
- System.Text.Json 10.0.11

### Next Steps
- Pending LocalCore Gatekeeper Verification (EXIT_CODE: 0)
- Pending Git Tag v0.1.0

### Verification Protocol
- Protocol Version: v4.6 Addendum
- Gatekeeper: LocalCore CLI (Silent Background Mode)
- Exit Code Requirement: 0 (Success)

