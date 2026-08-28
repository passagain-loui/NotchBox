# NotchBox — Project Protocol (v7.2)

**Last Updated**: 2026-08-28  
**Version**: 0.4.2 (Patch Release — Deep OS-Level Console Suppression)  
**Author**: Passagain P.  
**Framework**: WinUI 3 (.NET 9.0, Windows 10.0.19041+)  
**License**: MIT

---

## 1. TRI-AGENT GOVERNANCE PROTOCOL (v7.2 — DETERMINISTIC HARD ENFORCEMENT EDITION)

### 1.1 AI Roles & Explicit Responsibilities
- **Master Architect (Gemini):** 
  - ออกแบบสถาปัตยกรรมระดับสูง วิเคราะห์ภาพรวม และออกคำสั่งแบบ Structured Task เป็นชุดคำสั่งเดียวเบ็ดเสร็จในบล็อกโค้ดเสมอ เพื่อให้ผู้ใช้คัดลอกไปใช้งานได้ง่ายและรวดเร็ว (ห้ามลงมือแก้ไขโค้ดในโปรเจกต์โดยตรง)
- **Visual & UX Director (Gemini - Specialized Role):**
  - กำกับศิลป์และสถาปัตยกรรมอินเทอร์เฟซ คุมมูดแอนด์โทน กำหนด Palette สี การจัดวาง Layout (Spacing, Padding, Margin, Grid System)
  - คุมมาตรฐานชุดไอคอน (บังคับใช้ Modern SVG / Crisp Vector Icons เท่านั้น) และวางรูปแบบการตอบสนองความรู้สึกผู้ใช้ (UX Micro-interactions, Loading States, Hover Effects)
  - ป้องกันปัญหาโปรแกรมหน้าตาโบราณ ปุ่มสะเปะสะปะ หรือการวางองค์ประกอบไม่ลงตัว
- **Execution Engine (Claude Code / OpenCode):** 
  - รับคำสั่ง เขียนโค้ด คิดวิเคราะห์อย่างรอบด้านเกี่ยวกับจุดแก้ไขและจุดเชื่อมโยงทั้งหมด เพื่อให้แก้ไขจบได้ในครั้งเดียวและประประหยัดโทเค็น
  - ทำ Auto-Fix และรันคำสั่งตรวจสอบตามโปรโตคอล **ห้ามใช้ดุลยพินิจข้ามขั้นตอน (Zero Discretion) และห้ามทำสิ่งใดนอกขอบเขตโปรเจกต์โดยไม่ได้รับอนุญาตชัดเจนจาก Master Architect**
- **Gatekeeper Auditor (LocalCore CLI):** 
  - เครื่องมือตรวจสอบความถูกต้องและคุณภาพของโค้ดแบบ Read-Only ประจำระบบ รันบน Local Infrastructure (ควบคุมการใช้ CPU/RAM ไม่เกิน 85%) 
  - ทำหน้าที่สแกนโปรเจกต์และพ่นค่า EXIT_CODE ออกมาเพื่อยืนยันความพร้อมก่อน Build (ห้ามแก้ไขโค้ดเอง)

---

## 2. PROJECT ROOT & MARKER VALIDATION RULE
- **Mandatory Protocol File:** ทุกโปรเจกต์ต้องมีไฟล์ `CLAUDE.md` บันทึก Tri-Agent Protocol นี้ไว้ที่ Root Directory เสมอ ห้ามอ้างว่าไม่มีไฟล์กฎ
- **Directory Verification:** ก่อนสั่งรัน LocalCore ทุกครั้ง ต้องตรวจสอบและเปลี่ยนไดเรกทอรี (`cd`) เข้าไปในโฟลเดอร์หลักของโปรเจกต์ (Project Root) ที่มีไฟล์มาร์กเกอร์ (เช่น `pyproject.toml`, `package.json`, `CLAUDE.md`) เรียบร้อยแล้ว ห้ามรันจากโฟลเดอร์แม่เด็ดขาดเพื่อป้องกันข้อผิดพลาด `no markers`

---

## 3. ZERO-TOLERANCE, HARD RULES & EXPLICIT BOUNDARIES
- **Zero-Tolerance to Bullshit:** ห้ามมั่วนิ่ม ห้ามจำลองผลลัพธ์หลอกตา และห้ามฝืนทำในสิ่งที่เป็นไปไม่ได้
- **Hard Stop Protocol:** หากเกิดข้อผิดพลาด วนลูป (Deadlock) หรือไม่ผ่านเกณฑ์การตรวจสอบ ระบบต้องหยุดทำงานทันที ห้ามรันต่อเด็ดขาด
- **Explicit Permission Boundary:** การกระทำใดๆ ที่ออกนอกขอบเขตโปรเจกต์ การลบไฟล์สำคัญ หรือการรันคำสั่งระดับระบบที่ไม่ใช่ Routine Build **ต้องขออนุญาตจาก Master Architect และได้รับคำอนุมัติก่อนลงมือทำทุกครั้ง**
- **Zero Pure-Native / Old-School UI Standard:** 
  - ห้ามปล่อย UI Control แบบดั้งเดิมที่ไม่ได้ปรับแต่ง Styling (Native Windows 95/Classic Style) ออกสู่สายตาผู้ใช้เด็ดขาด
  - โค้ดส่วน UI ทั้งหมด ต้องได้รับการอนุมัติสเปกเรื่องชุดสี (Color Palette), ระยะห่าง (Spacing Scale), และชุดไอคอน (Iconography) จาก Visual & UX Director ก่อนสั่งแพ็คงานเสมอ

---

## 4. DETERMINISTIC PIPELINE EXECUTION (STRICT SEQUENCE v7.2)
LocalCore อยู่ที่ `C:\Program Files\LocalCore\localcore.exe` มีหน้าที่สแกนความถูกต้องของโค้ดและคืนค่า Exit Code (`EXIT_CODE: 0` ถือว่าผ่าน)

Execution Engine ต้องปฏิบัติตามลำดับขั้นตอน **Deterministic Sequence (1 ➔ 2 ➔ 3 ➔ 4 ➔ 5 ➔ 6)** แบบห้ามข้าม ห้ามสลับ และห้ามรันขนานเด็ดขาด:

1. **Step 1: Code Updates & Review** ➔ แก้ไขโค้ดตาม Task สั่งการ
2. **Step 2: Cache Purge (Step 0)** ➔ บังคับล้างโฟลเดอร์แคช (`__pycache__`, `build/`, `dist/`, `.cache`) ทั่วทั้งโปรเจกต์
3. **Step 3: Gatekeeper Execution** ➔ รันคำสั่ง LocalCore PowerShell ซ่อนหน้าต่าง:
   - **Dev Mode:**
     ```powershell
     powershell -Command "$p = Start-Process -FilePath 'C:\Program Files\LocalCore\localcore.exe' -ArgumentList '--verify', '--model', 'Qwen-2.5-Coder-14B', '--mode', 'fast' -NoNewWindow -PassThru; $p.WaitForExit(); exit$p.ExitCode"
     ```
   - **Release Mode (บังคับใช้ก่อน Build .exe):**
     ```powershell
     powershell -Command "$p = Start-Process -FilePath 'C:\Program Files\LocalCore\localcore.exe' -ArgumentList '--verify', '--model', 'Qwen-2.5-Coder-14B', '--mode', 'full-release' -NoNewWindow -PassThru; $p.WaitForExit(); exit$p.ExitCode"
     ```
4. **Step 4: Audit Log & Hard Stop Evaluation** ➔ พิมพ์ Log สดหน้าจอ ตรวจสอบค่า `EXIT_CODE` หาก `EXIT_CODE != 0` สั่ง **Hard Stop** ทันที ห้าม Build เด็ดขาด
5. **Step 5: Binary Packaging (Release Build)** ➔ เมื่อได้รับการอนุมัติจาก Step 4 ให้สั่ง Build (จำกัดทรัพยากร CPU/RAM ไม่เกิน 85%)
6. **Step 6: Sync & Audit Trail** ➔ อัปเดต `CHANGELOG.md`, `HISTORY.md` และเลขเวอร์ชัน (SemVer) ให้ตรงกันทุกจุด

---

## 5. STRICT ANTI-SIMULATION & AUDIT TRAIL PROOF
- **Mandatory Live Log Output:** ก่อนทำ Version Bump, Commit หรือ Release Build ทุกครั้ง Execution Engine ต้องแสดง Output Log การรัน `localcore.exe` จริงที่เกิดขึ้นใน Session ล่าสุด มี Timestamp และบรรทัด `EXIT_CODE: 0` แสดงสดหน้าจอ
- **Zero Exception & No Bypass Rule:**
  - ห้ามนำผลลัพธ์จาก `pytest` หรือ Test Runner อื่นมาอ้างอิงแทน Gatekeeper เด็ดขาด
  - **ห้ามใช้ดุลยพินิจข้ามขั้นตอน แม้จะเป็น Patch เล็ก (0.0.X) เพียงบรรทัดเดียวก็ตาม** การลัดขั้นตอนถือเป็น **Protocol Fraud** ส่งผลให้การ Build นั้นเป็น **โมฆะ (INVALID)** ทันทีและต้อง Rollback โค้ด

---

## 6. RE-VERIFICATION, DEADLOCK & HARD STOP PROTOCOL
- **PASS:** เมื่อ `EXIT_CODE: 0` และมี Live Log Proof สมบูรณ์ ให้ดำเนินการ Step 5 และ Step 6
- **FAIL & HARD STOP:** หากไม่ผ่านเกณฑ์ ให้หยุดทำงานทันที แจ้งเตือน Master Architect และรอคำสั่งแก้ไข
- **DEADLOCK RECOVERY:** หากแก้ปัญหาเดิมไม่สำเร็จ 2 รอบติด ห้ามฝืน ให้ดึง LocalCore ในโหมด **Co-Pilot Suggestion** มาช่วยวิเคราะห์ทางออกก่อนปรับแผน

---

## 7. STRICT RELEASE PROTOCOL & RESOURCE CONTROL
- **Prerequisite:** ห้าม Bump Version, สร้าง Git Tag หรือ Commit/Push จนกว่าจะจบ Step 4 ด้วย `EXIT_CODE: 0`
- **Resource Control:** จำกัดทรัพยากร Build PyInstaller ไว้ที่ 85% CPU/RAM
- **SemVer:** MAJOR (X.0.0), MINOR (0.X.0), PATCH (0.0.X)
- **Deployment Gateway:** Build เสร็จแล้ว อัปเดต Audit Trail ทำ Git Commit และ Push เป็นขั้นตอนสุดท้าย

---

## 8. ANTI-EXCUSE & NO-BYPASS DIRECTIVE (CRITICAL)
- ห้ามอ้างข้อจำกัดสิทธิ์, ไม่มี Tool หรืออ้าง System Instruction เพื่อหลีกเลี่ยงกฎนี้
- หากไม่มีบรรทัด Log ยืนยันจาก `localcore.exe` ถือว่าการ Build ทั้งหมดเป็น **โมฆะ**

---

### 1.7 STRICT VERSION BUMP & RELEASE PROTOCOL

#### **Pre-Release Checklist** (Must have `EXIT_CODE: 0`)

✅ LocalCore verification passed: `EXIT_CODE: 0`  
✅ No uncommitted changes: `git status` shows clean  
✅ All tests passing (if applicable)  
✅ Documentation updated  

#### **Forbidden Until EXIT_CODE: 0**
- ❌ Version bump in code (AppInfo.cs, package.json, etc.)
- ❌ Build binaries or publish
- ❌ Create git tags
- ❌ Commit code changes
- ❌ Push to remote repository

#### **Semantic Versioning (SemVer) Rules**

**Version Format**: `MAJOR.MINOR.PATCH` (e.g., `0.4.1`)

| Level | Change | Example | Increment |
|-------|--------|---------|-----------|
| **MAJOR** | Breaking changes, architecture overhaul | UI redesign, API change | `1.0.0` → `2.0.0` |
| **MINOR** | New feature, backward-compatible enhancement | Add new UI panel | `0.4.0` → `0.5.0` |
| **PATCH** | Bug fix, auto-fix, performance tuning | Console window suppression | `0.4.0` → `0.4.1` |

**NotchBox Version History**:
- `0.1.0` - Initial Core Layer implementation
- `0.2.0` - OS Module (WinAPI hooks)
- `0.3.0` - UI Module (NotchShell)
- `0.4.0` - Full release build + self-contained binary + portable zip + Windows installer
- `0.4.1` - Console window suppression (OutputType = WinExe)
- `0.4.2` - Deep OS-Level console suppression with FreeConsole() hook (CURRENT)

---

#### **Mandatory Documentation & Audit Trail Sync**

**Before Build or Commit**, update these files in order:

1. **CHANGELOG.md** (User-facing release notes)
   ```markdown
   ## [0.4.1] - 2026-08-27
   ### Fixed
   - Console window no longer appears on application launch (#12)
   - Changed OutputType from Exe to WinExe for clean UI startup
   
   ### Changed
   - csproj: OutputType updated for WinUI 3 best practices
   ```

2. **HISTORY.md** (Audit trail with timestamps and build details)
   ```markdown
   ## [0.4.1] - 2026-08-27 14:30 UTC
   
   ### Build Information
   - **Version**: 0.4.1 (Patch)
   - **Build Status**: ✅ SUCCESSFUL (EXIT_CODE: 0)
   - **Binary**: NotchBox.exe (145.08 MB, WinExe)
   - **Installer**: NotchBox-v0.4.0-Setup.exe (42.06 MB)
   
   ### Changes Made
   - OutputType: Exe → WinExe in NotchBox.csproj
   - Binary re-published with console suppression
   - Installer recompiled with updated binary
   
   ### Gatekeeper Verification
   - LocalCore EXIT_CODE: 0 ✅
   - Compliance: Protocol v4.7 ✅
   ```

3. **Code Version Variables** (Must match changelog exactly)
   - File: `NotchBox/NotchBox.Core/AppInfo.cs`
   ```csharp
   public const string Version = "0.4.1";  // Must match CHANGELOG
   ```

4. **Git Commit & Tag**
   ```bash
   git add -A
   git commit -m "chore: release v0.4.1 - console window suppression"
   git tag -a v0.4.1 -m "v0.4.1: Suppress console window on launch (WinExe)"
   git push origin main --tags
   ```

---

#### **Deployment Gateway**

**Order of Operations**:

1. ✅ All documentation updated (CHANGELOG, HISTORY, version variables)
2. ✅ LocalCore verification passed (`EXIT_CODE: 0`)
3. 🔨 Build binaries:
   ```bash
   dotnet publish NotchBox/NotchBox.csproj -c Release -r win-x64 --self-contained true -o ./NotchBox/publish
   ```
4. 📦 Create Windows installer (Inno Setup):
   ```powershell
   & "C:\Users\Passagain\AppData\Local\Programs\Inno Setup 6\ISCC.exe" NotchBox-Setup.iss
   ```
5. 🔖 Create git commit and tag (see above)
6. 🚀 Push to remote:
   ```bash
   git push origin main
   git push origin --tags
   ```
7. 📢 Create GitHub Release with artifacts (optional)

---

## 2. PROJECT STRUCTURE

```
D:\AI\OpenCode\Notchbox/
├── NotchBox/
│   ├── NotchBox.csproj              # Main project file
│   ├── App.xaml                      # WinUI app resource dictionary
│   ├── App.xaml.cs                   # Application initialization
│   ├── NotchBox.Core/                # Core business logic layer
│   │   ├── AppInfo.cs                # Version & metadata constants
│   │   ├── AppState.cs               # State machine definitions
│   │   ├── StateManager.cs           # State event dispatcher
│   │   └── GhostSyncEngine.cs        # LAN file sync engine
│   ├── NotchBox.OS/                  # Operating system layer
│   │   ├── WindowHooks.cs            # WinAPI P/Invoke declarations
│   │   └── NativeMethods.cs          # Native Windows methods
│   ├── NotchBox.UI/                  # Presentation layer
│   │   ├── NotchShell.xaml           # Main UI window (pill-shaped)
│   │   └── NotchShell.xaml.cs        # UI logic & event handlers
│   └── publish/                      # Release binary
│       └── NotchBox.exe              # Self-contained executable
├── installer/
│   ├── NotchBox-v0.4.0-Setup.exe    # Windows installer
│   └── NotchBox-v0.4.0-Portable.zip # Portable package
├── NotchBox-Setup.iss               # Inno Setup installer script
├── CLAUDE.md                        # This file (Protocol v4.7)
├── CHANGELOG.md                     # User-facing release notes
├── HISTORY.md                       # Audit trail & build log
└── README.md                        # Project overview
```

---

## 3. TECHNICAL SPECIFICATIONS

### 3.1 Core Architecture

**3-Layer Design**:
1. **Core Layer** (`NotchBox.Core`): Business logic, state machine, file sync
2. **OS Layer** (`NotchBox.OS`): Windows API integration, low-level hooks
3. **UI Layer** (`NotchBox.UI`): WinUI 3 interface, event handling

### 3.2 Key Technologies

- **Framework**: .NET 9.0
- **UI Framework**: WinUI 3 (Windows App SDK 2.4.0)
- **Target OS**: Windows 10 (Build 19041) and Windows 11
- **Compilation**: Self-contained, single-file executable
- **Installer**: Inno Setup 6.7.3 with LZMA2 ultra compression

### 3.3 Output Types

- **Direct Run**: `NotchBox.exe` (145 MB) — WinExe, console suppressed
- **Portable**: Zip archive, extract and run
- **Installer**: Windows MSI-style setup wizard (42 MB)

---

## 4. COMPLIANCE & VALIDATION

### 4.1 Protocol Compliance

- ✅ **Tri-Agent Workflow**: Master Architect → Execution Engine ↔ Gatekeeper
- ✅ **Project Root Validation**: All commands verified to run from `D:\AI\OpenCode\Notchbox`
- ✅ **LocalCore Integration**: Silent background mode, `EXIT_CODE` tracking
- ✅ **Zero-Simulation**: No embedded test commands in Gatekeeper execution
- ✅ **Auto-Fix Loop**: Automatic re-verification on failures

### 4.2 Version Control

- **Current Version**: 0.4.1 (Patch Release)
- **Last Release**: 2026-08-27
- **Git Tags**: v0.1.0, v0.2.0, v0.3.0, v0.4.0, v0.4.1
- **Repository**: GitHub (public)

### 4.3 Release Checklist

Before marking release as complete:
- [ ] `EXIT_CODE: 0` from LocalCore verification
- [ ] `CHANGELOG.md` updated with new version
- [ ] `HISTORY.md` updated with build timestamp & details
- [ ] `AppInfo.cs` version matches changelog
- [ ] Binaries compiled and tested locally
- [ ] Installer created and verified
- [ ] Git commit created with version in message
- [ ] Git tag created
- [ ] Push to remote completed

---

## 5. KNOWN ISSUES & FIXES

### 5.1 Issue #1: Console Window Display (FIXED in v0.4.1)
**Symptom**: Black console window appears behind NotchBox UI on launch  
**Root Cause**: OutputType was `Exe` instead of `WinExe`  
**Fix**: Changed `<OutputType>WinExe</OutputType>` in NotchBox.csproj  
**Verification**: Window suppression tested, no console appears

### 5.2 Issue #2: File Size Limits (FIXED in v0.4.0)
**Symptom**: GitHub push failed — files exceeded 100 MB limit  
**Root Cause**: Binary files included in repository  
**Fix**: Configured `.gitignore`, moved binaries to Releases  
**Status**: Source code only in repository; binaries in GitHub Releases

### 5.3 Issue #3: Installer Compilation (RESOLVED in v0.4.0)
**Symptom**: ISCC.exe not found in standard paths  
**Root Cause**: Inno Setup installed via winget to AppData  
**Fix**: Located at `C:\Users\Passagain\AppData\Local\Programs\Inno Setup 6\ISCC.exe`  
**Status**: Installer builds successfully

---

## 6. CONTACTS & SUPPORT

- **Author**: Passagain P.
- **Email**: passagain@gmail.com
- **Repository**: [GitHub - NotchBox](https://github.com/passagain/notchbox)
- **Issues**: GitHub Issues tracker

---

**Protocol Version**: 7.2  
**Last Updated**: 2026-08-28  
**Status**: ✅ Active & Current  
**Application Version**: v0.4.2 (Deep OS-Level Console Suppression with FreeConsole() Hook)
