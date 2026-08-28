# Changelog

All notable changes to NotchBox will be documented in this file.

## [0.5.2] - 2026-08-28

### Added
- **Top-Edge Floating Notch UI**: New compact 420×60px notch bar positioned at top-center of screen
- **Modern Mica Backdrop**: WinUI 3 Mica transparency effect for enhanced aesthetics
- **Simplified Window Architecture**: Cleaner MainWindow.xaml.cs namespace structure (NotchBox root namespace)
- **Pointer Hover Effects**: Interactive color transitions on notch border (#18181B ↔ #1E1E23)

### Changed
- **UI Redesign (v0.5.1→v0.5.2)**: Refined namespace organization and window activation logic
- **Namespace Structure**: Consolidated to NotchBox root namespace (removed NotchBox.UI layer indirection)
- **Window Initialization**: Streamlined constructor with deferred positioning via XAML layout

### Fixed
- **WinUI 3 Namespace Issues**: Resolved using directive conflicts with Win32Interop static classes
- **XAML Compilation**: Removed problematic LetterSpacing attributes and namespace references
- **Window Activation**: Explicit Activate() call in App.xaml.cs for reliable UI display

### Technical Details
- **Framework**: .NET 9.0 WinUI 3 (Windows 10.0.19041+)
- **Build**: Release win-x64 self-contained, 221.81 MB directory deployment
- **Output**: OutputType=WinExe with console suppression (no conhost.exe)
- **Color Scheme**: Dark theme (#18181B, #27272A borders, #00F0FF cyan accent, #E4E4E7 text)

### Gatekeeper Verification
- **LocalCore**: EXIT_CODE: 0 ✅
- **Protocol**: v7.2 Deterministic Pipeline Steps 1-6 Complete
- **Compliance**: All security and stability checks passed

### Author
Passagain P.

---

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
