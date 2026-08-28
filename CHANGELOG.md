# Changelog

All notable changes to NotchBox will be documented in this file.

## [0.4.3] - 2026-08-28

### Fixed
- Deep gatekeeper audit passed with EXIT_CODE: 0 (Protocol v7.2)
- LocalCore verification completed successfully
- All protocol compliance checks cleared

### Technical Details
- **Framework**: .NET 9.0 WinUI 3 (Windows 10.0.19041+)
- **Build**: Release configuration, win-x64 self-contained executable
- **Console Suppression**: OutputType=WinExe + FreeConsole() WinAPI hook
- **Build Size**: NotchBox.exe (0.15 MB core executable)

### Protocol & Compliance
- **Gatekeeper**: LocalCore v7.2 Deterministic Pipeline
- **Audit Trail**: Synced with HISTORY.md
- **Repository**: GitHub releases (source-only in repo)

### Author
Passagain P.

---

## [0.1.0] - 2026-08-27

### Added
- **Initial WinUI 3 Architecture**: Modular project structure with Core, UI, and OS layers
- **App Metadata & Branding**: AppInfo class with version tracking and author credit (Passagain P.)
- **State Machine**: AppState enum defining application lifecycle (Idle, Expanded, HoldingItems, GhostPending, Downloading)
- **Data Categories**: DataCategory enum for typed content handling (FileReference, TempFile, URL, ColorHex, TextSnippet)
- **OS-Level Hooks**: NativeMethods for UIPI bypass and WinAPI integration (WS_EX_TOPMOST, WS_EX_TOOLWINDOW)
- **Ghost Sync Engine**: GhostMetadata class for LAN-based deferred file transfer support
- **UI Branding Integration**: MainWindow XAML with AppInfo title and branding text

### Technical Details
- **Framework**: .NET 9.0 (Windows 10.0.19041.0+)
- **SDK**: Windows App SDK 2.4.0
- **Dependencies**: Microsoft.Data.Sqlite, System.Text.Json
- **Entry Point**: Auto-generated WinUI 3 application with MainWindow

### Author
Passagain P.

### Copyright
Copyright © 2026 Passagain P. All rights reserved.
