# Changelog

All notable changes to NotchBox will be documented in this file.

## [0.2.0] - 2026-08-27

### Added - Core Layer 1 (Top-Pill Shell & OS Hooks)
- **WindowHooks.cs**: Native WinAPI integration for window styling
  - `GetWindowLong` & `SetWindowLong` for extended window styles
  - `WS_EX_TOPMOST` (0x00000008) — Always-on-top window rendering
  - `WS_EX_TOOLWINDOW` (0x00000080) — Tool window classification
  - `ChangeWindowMessageFilterEx` — UIPI message filter for WM_COPYGLOBALDATA
- **NotchShell.xaml(.cs)**: Top-center pill-shaped UI shell
  - Branding integration with AppInfo.FullTitle
  - Native window hook application via WindowNative.GetWindowHandle
- **StateManager.cs**: State machine with event-driven transitions
  - AppState lifecycle management (Idle ↔ Expanded ↔ HoldingItems ↔ GhostPending ↔ Downloading)
  - OnStateChanged event for UI animation binding

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
