# Changelog

All notable changes to NotchBox will be documented in this file.

## [0.4.0] - 2026-08-27

### Added - Core Layer 3 (UI Animations & Drag-Drop)
- **NotchShell.xaml**: Compositional animations & drop target UI
  - Pill-shaped Border container with 0,0,16,16 corner radius
  - RepositionThemeTransition for smooth size/position animations
  - Status indicator Ellipse (8×8) with color-coded states
  - Branding TextBlock with dynamic content binding
  - AllowDrop + DragOver + Drop event handlers for OLE drag-and-drop
  - PointerEntered + PointerExited for hover state detection
  
- **NotchShell.xaml.cs**: Dynamic state animations & shelf management
  - `RootGrid_PointerEntered()` → Expand on hover (Idle → Expanded)
  - `RootGrid_PointerExited()` → Collapse on exit if shelf empty (Expanded → Idle)
  - `RootGrid_DragOver()` → Accept copy operation + expand UI
  - `RootGrid_Drop()` → Add files to shelf + transition to HoldingItems
  - `HandleStateChanged()` — Dynamic UI updates per state:
    - **Idle**: 180×32, green dot, "NotchBox"
    - **Expanded**: 320×120, blue dot, "Drop items here..."
    - **HoldingItems**: 360×140, gold dot, "{n} item(s) on shelf"
    - **GhostPending**: 320×100, purple dot, "Shared item available!"
    - **Downloading**: orange dot, "Transferring payload..."
  - `_shelfItems` List<string> to track dropped files
  - `DispatcherQueue.TryEnqueue()` for thread-safe UI updates
  
- **Version Metadata**: Updated to v0.4.0

## [0.3.0] - 2026-08-27

### Added - Core Layer 2 (Ghost Sync & LAN Deferred Transfer)
- **GhostSyncEngine.cs**: LAN file synchronization and deferred transfer engine
  - `FileSystemWatcher` monitoring for incoming JSON metadata (*.json)
  - `OnGhostItemReceived` event — Triggered when remote metadata detected
  - `OnGhostItemRemoved` event — Triggered when metadata file removed
  - `HydratePayloadAsync()` — Async file transfer from shared network path
  - JSON deserialization for GhostMetadata (Id, Sender, FileName, FileSizeBytes, PayloadPath, Status)
  - Smart sender filtering (ignores own machine's messages)
- **NotchShell Integration**: Ghost Sync → StateManager binding
  - OnGhostItemReceived → TransitionTo(GhostPending)
  - OnGhostItemRemoved → TransitionTo(Idle)
  - Automatic state lifecycle management via events
- **Version Metadata**: Updated to v0.3.0

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
