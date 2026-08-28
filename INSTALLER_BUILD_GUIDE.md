# NotchBox v0.4.0 — Installer Compilation Guide

## Overview

The Inno Setup installer script `NotchBox-Setup.iss` is ready to compile. This guide explains how to create the Windows installer executable.

---

## Prerequisites

You need **Inno Setup 6.x** or later installed on your system.

### Installation Methods

#### Method 1: Direct Download (Recommended)
1. Visit: https://www.jrsoftware.org/isdl.php
2. Download "Inno Setup 6.x (unicode)" (latest version)
3. Run the installer and follow the wizard
4. Accept default installation location

#### Method 2: Windows Package Manager
```powershell
winget install jrsoftware.innosetup
```

#### Method 3: Scoop
```powershell
scoop install innosetup
```

#### Method 4: Chocolatey
```powershell
choco install innosetup
```

---

## Compilation Steps

### Step 1: Open Inno Setup
After installation, open "Inno Setup Compiler" from Start menu or:
```
C:\Program Files (x86)\Inno Setup 6\Compil32.exe
```

### Step 2: Open the Script
1. Go to **File** → **Open**
2. Navigate to: `D:\AI\OpenCode\Notchbox\NotchBox-Setup.iss`
3. Click **Open**

### Step 3: Compile
1. Go to **Build** → **Compile**
   - OR press **Ctrl+F9**
   - OR click the **Compile** button in the toolbar

2. The compiler will process and create the installer

### Step 4: Verify Output
The compiled installer will be saved at:
```
D:\AI\OpenCode\Notchbox\installer\NotchBox-v0.4.0-Setup.exe
```

---

## Command-Line Compilation (Advanced)

If you prefer to compile from the command line:

```batch
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" "D:\AI\OpenCode\Notchbox\NotchBox-Setup.iss"
```

Or if in the NotchBox directory:
```batch
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" NotchBox-Setup.iss
```

---

## PowerShell Script (Automated)

```powershell
cd D:\AI\OpenCode\Notchbox

$isccPath = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"

if (Test-Path $isccPath) {
    Write-Host "Compiling NotchBox installer..." -ForegroundColor Cyan
    & $isccPath NotchBox-Setup.iss
    
    if (Test-Path ".\installer\NotchBox-v0.4.0-Setup.exe") {
        $size = (Get-Item ".\installer\NotchBox-v0.4.0-Setup.exe").Length / 1MB
        Write-Host "✅ Installer created: NotchBox-v0.4.0-Setup.exe ($([math]::Round($size, 2)) MB)" -ForegroundColor Green
    }
} else {
    Write-Host "❌ Inno Setup Compiler not found at: $isccPath" -ForegroundColor Red
    Write-Host "Please install Inno Setup first." -ForegroundColor Yellow
}
```

---

## Installer Features

The compiled `NotchBox-v0.4.0-Setup.exe` will include:

✅ **Installation Options:**
- Select installation directory
- Create Start menu shortcuts
- Create desktop icon (optional)
- Run at Windows startup (optional)

✅ **Uninstallation:**
- Full uninstall via Control Panel
- Remove all application files
- Remove shortcuts and registry entries

✅ **Compression:**
- LZMA2 ultra compression
- ~25-35 MB installer size (estimated)

✅ **Compatibility:**
- Windows 10 (Build 19041) and later
- Windows 11
- Both 64-bit systems

---

## Distribution

Once compiled, you can distribute `NotchBox-v0.4.0-Setup.exe` via:

1. **GitHub Releases** - Upload to your repository
2. **Your Website** - Direct download link
3. **Application Stores** - Microsoft Store, SourceForge, etc.
4. **Email/Sharing** - Send directly to users

---

## Troubleshooting

### "ISCC.exe not found"
- Ensure Inno Setup is installed in `C:\Program Files (x86)\Inno Setup 6\`
- If installed elsewhere, adjust the path accordingly
- Restart PowerShell/command prompt after installation

### "Script error" messages
- Check that `NotchBox-Setup.iss` is in the correct location
- Verify paths in the script point to `NotchBox\publish\` directory
- Ensure `NotchBox.exe` exists at `NotchBox\publish\NotchBox.exe`

### Installer file too large
- Normal size is 25-35 MB with LZMA2 ultra compression
- If much larger, verify only necessary files are included

---

## Support

For Inno Setup help:
- Official Documentation: https://jrsoftware.org/isinfo.php
- Script Reference: https://jrsoftware.org/ishelp/index.php
- Community Forum: https://forums.innosetup.org/

---

## Summary

1. Install Inno Setup 6.x
2. Open `NotchBox-Setup.iss`
3. Click **Compile**
4. Installer created at `installer\NotchBox-v0.4.0-Setup.exe`
5. Distribute to users!
