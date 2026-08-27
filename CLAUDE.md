# CLAUDE.md — NotchBox Project Development & Verification Protocol

## TRI-AGENT WORKFLOW PROTOCOL (v4.6 — SILENT BACKGROUND VERIFICATION)

This document outlines the collaborative workflow between three AI agents and the gatekeeper system for ensuring code quality and automated verification with ZERO-TOLERANCE anti-simulation enforcement and mandatory Log traceability.

### 1. AI ROLES & EXPLICIT RESPONSIBILITIES

**Master Architect (Gemini)**
- หน้าที่: ออกแบบสถาปัตยกรรมระดับสูง วิเคราะห์ภาพรวม และออกคำสั่งแบบ Structured Task
- ข้อจำกัด: เป็นผู้วางแผนและสั่งการ ห้ามลงมือแก้ไขโค้ดในโปรเจกต์โดยตรง

**Execution Engine (Claude Code / OpenCode)**
- หน้าที่: รับคำสั่งจาก Master Architect, เขียนโค้ด, ทำ Auto-Fix, และรันคำสั่งตรวจสอบตามโปรโตคอล
- ข้อจำกัด: ห้ามแอบอ้างผลลัพธ์ (No Simulation) และห้ามข้ามขั้นตอนการตรวจสอบ

**Gatekeeper Auditor (LocalCore CLI)**
- หน้าที่: สแกนตรวจสอบโค้ดแบบ Read-Only และพ่นค่า EXIT_CODE ออกมาพร้อมบันทึก Log ลงระบบ
- ข้อจำกัด: เป็นผู้ตรวจทานความถูกต้องเด็ดขาด ไม่ทำการแก้ไขโค้ดเอง

---

### 2. MANDATORY GATEKEEPER EXECUTION RULE (SILENT BACKGROUND MODE)

เพื่อป้องกันไม่ให้หน้าต่าง LocalCore เด้งซ้อนทับหน้าต่าง Log หลักของผู้ใช้ Execution Engine ต้องใช้คำสั่งรันแบบซ่อนหน้าต่าง (PowerShell Start-Process หรือ WindowStyle Hidden) ทุกครั้ง:

```powershell
$p = Start-Process -FilePath 'C:\Program Files\LocalCore\localcore.exe' `
  -ArgumentList '--verify', '--model', 'Qwen-2.5-Coder-14B' `
  -NoNewWindow -PassThru
$p.WaitForExit()
exit $p.ExitCode
```

---

### 3. STRICT ANTI-SIMULATION & LOG TRACEABILITY (ZERO-TOLERANCE)

**ห้ามใช้คำสั่งเทสภายใน:**
- ❌ **PROHIBITED:** ใช้ pytest, cargo test เดี่ยวๆ แล้วนำ Exit Code มาอ้างอิงแทน LocalCore เด็ดขาด
- ❌ **PROHIBITED:** การจำลอง (Simulate) หรือแอบอ้างผลลัพธ์ LocalCore ออกมาแบบไม่จริง
- ❌ **PROHIBITED:** ข้ามขั้นตอนการตรวจสอบและลงมือทำ Version Bump, Build, หรือ Git Commit

**บังคับให้ใช้ LocalCore จริง:**
- ✅ **REQUIRED:** บังคับให้ข้อมูลและ Request ต้องวิ่งเข้าประมวลผลผ่าน LocalCore CLI จริงเท่านั้น
- ✅ **REQUIRED:** ต้องปรากฏ Log หลักฐานของการทำงาน (Log Trace) ในระบบ LocalCore
- ✅ **REQUIRED:** หากผลลัพธ์แสดง EXIT_CODE: 0 แต่ไม่มีร่องรอยการทำงานหรือ Log วิ่งผ่าน LocalCore จะถือว่า "เป็นโมฆะทันที" (Invalid Execution)

---

### 4. AUTOMATED RE-VERIFICATION LOOP

**INITIAL CHECK:**
- รันคำสั่งผ่าน Gatekeeper หากได้ EXIT_CODE: 0 ให้ไปขั้นตอน Release ทันที

**FAIL LOOP:**
หาก EXIT_CODE != 0 (FAIL):
- Execution Engine ห้ามหยุดทำงานและห้ามถามผู้ใช้
- อ่าน Error Trace จาก Log ของ LocalCore → ทำการแก้ไขโค้ด (Auto-Fix) → สั่งรันคำสั่ง Gatekeeper ซ้ำใน Terminal จริงทันที
- ทำซ้ำจนกว่า LocalCore จะพ่นค่า EXIT_CODE: 0 ออกมาจริงๆ เท่านั้น

---

### 5. STRICT EXIT & DEPLOYMENT CONDITION

งานจะเสร็จสมบูรณ์และอนุญาตให้ทำ Version Bump, Build Binaries, รวมถึง Git Commit/Push ได้ ก็ต่อเมื่อมีหลักฐาน Log และ EXIT_CODE: 0 จากการรัน LocalCore จริงยืนยันเท่านั้น!

---

## STRICT VERSION BUMP & RELEASE PROTOCOL (v4.6 Addendum)

### 1. CONDITIONAL PREREQUISITE

- ❌ ห้ามทำ Version Bump, Build Binaries, สร้าง Git Tag, หรือ Commit/Push เด็ดขาด
- ✅ จนกว่าจะมีหลักฐานค่า EXIT_CODE: 0 จากการรัน LocalCore จริงยืนยันเท่านั้น

### 2. VERSION INCREMENT RULES (SEMVER)

- **MAJOR Version (X.0.0):** อัปเดตเมื่อมีการเปลี่ยนแปลงโครงสร้างสถาปัตยกรรมครั้งใหญ่ หรือมีการแก้โค้ดที่ทำลายความเข้ากันได้ของระบบเดิม (Breaking Changes)
- **MINOR Version (0.X.0):** อัปเดตเมื่อมีการเพิ่มฟีเจอร์ใหม่ (New Features) หรือฟังก์ชันการทำงานหลักที่สมบูรณ์และผ่านการตรวจจาก Gatekeeper แล้ว
- **PATCH Version (0.0.X):** อัปเดตเมื่อมีการแก้ไขบั๊ก (Bug Fixes), ปรับปรุงโค้ดภายใน, หรือทำ Auto-Fix เล็กๆ น้อยๆ

### 3. MANDATORY DOCUMENTATION & AUDIT TRAIL SYNC

ก่อนที่จะดำเนินการคำสั่ง Build หรือ Git Commit ทุกครั้ง Execution Engine ต้องอัปเดตเอกสารประกอบให้ครบถ้วนทุกจุด:

- **CHANGELOG.md:** บันทึกรายการเปลี่ยนแปลง ฟีเจอร์ใหม่ หรือบั๊กที่ถูกแก้ในเวอร์ชันนั้นๆ
- **HISTORY.md:** บันทึกประวัติการรันตรวจสอบ, Timestamp, และผลลัพธ์การยืนยันจาก Gatekeeper
- **Version Variable:** อัปเดตเลขเวอร์ชันในโค้ดหรือไฟล์ตั้งต้นของโปรเจกต์ (เช่น `AppInfo.Version` ใน NotchBox.Core) ให้ตรงกันทุกจุด

### 4. DEPLOYMENT GATEWAY

- หลังจากอัปเดตไฟล์เอกสารและเลขเวอร์ชันเสร็จสิ้น ให้ดำเนินการ Build ตัวติดตั้งหรือไฟล์ Binaries ต่อได้ทันที
- ทำการ Git Commit พร้อมระบุเลขเวอร์ชันที่ชัดเจน (เช่น "chore: release v0.2.0") และ Push ขึ้นรีโมทรีพอเป็นขั้นตอนสุดท้าย

---

## PROJECT ROOT & MARKER VALIDATION RULE (v4.6 Addendum)

### 1. WORKING DIRECTORY ENFORCEMENT

- ก่อนที่ Execution Engine จะสั่งรันคำสั่ง LocalCore ทุกครั้ง ต้องตรวจสอบให้แน่ใจว่าได้เปลี่ยนไดเรกทอรี (cd) เข้าไปในโฟลเดอร์หลักของโปรเจกต์ (Project Root)
- Project Root ต้องมีไฟล์มาร์กเกอร์ (เช่น `NotchBox.csproj`, `package.json`, `pyproject.toml`)
- ห้ามสั่งรัน LocalCore จากโฟลเดอร์กลางหรือโฟลเดอร์แม่ที่ไม่มีไฟล์มาร์กเกอร์โปรเจกต์โดยเด็ดขาด

### 2. AUTOMATED PATH CORRECTION

- หากพบ Log แจ้งเตือนเรื่อง `[AUTO-DETECT] FAILED - no markers` ให้ Execution Engine หยุดและทำการค้นหาโฟลเดอร์ Root ที่แท้จริงของโปรเจกต์ทันที
- ย้าย Working Directory ไปยังโฟลเดอร์นั้นก่อนสั่งรันคำสั่ง Gatekeeper ซ้ำ

---

## PROJECT CONTEXT

**Repository:** NotchBox (Local)
**Current Version:** 0.1.0 (Initial WinUI 3 Architecture)
**.NET Framework:** net9.0-windows10.0.19041.0
**Platform:** Windows 10+
**Build Tools:** dotnet build, LocalCore CLI
**VCS:** Git (initialized)

### Key Files
- `NotchBox/NotchBox.csproj` — Project manifest
- `NotchBox.Core/AppInfo.cs` — Version & branding constants
- `NotchBox.Core/AppState.cs` — State machine definitions
- `NotchBox.Core/GhostSyncEngine.cs` — LAN sync metadata
- `NotchBox.OS/NativeMethods.cs` — WinAPI integration
- `NotchBox.UI/MainWindow.xaml(.cs)` — Primary UI window
- `CHANGELOG.md` — User-facing release notes
- `HISTORY.md` — Verification audit trail
- `CLAUDE.md` — This protocol document

---

## ESCALATION & FAILURE SCENARIOS

**Scenario: LocalCore Cannot Find Project Markers**
1. Verify: Check current working directory
2. Fix: Move to NotchBox project root containing NotchBox.csproj
3. Re-Verify: Run LocalCore again from correct directory

**Scenario: BUILD FAILED (Compilation Errors)**
1. Analyze: Read full compiler error message
2. Fix: Update code to resolve compilation errors
3. Re-Verify: Run `dotnet build` and LocalCore verification loop

**Scenario: EXIT_CODE != 0**
1. Halt: Stop any further actions immediately
2. Analyze: Read LocalCore error output
3. Fix: Auto-correct identified issues
4. Re-Verify: Rerun LocalCore until EXIT_CODE: 0 achieved

**Escalation Trigger:** If loop exceeds 5 iterations → Contact Master Architect for guidance

---

## VERSION HISTORY & COMPLIANCE

| Version | Protocol | Status | Notes |
|---------|----------|--------|-------|
| 4.6 | Tri-Agent + Project Root Validation | Active | Current (This Document) |
| 4.5 | Tri-Agent + Mandated Roles | Deprecated | Replaced by v4.6 |
| 4.4 | Tri-Agent + Strict Anti-Simulation | Deprecated | Replaced by v4.5 |

**Compliance:** All releases ≥0.1.0 must follow Protocol v4.6

---

**Last Updated:** 2026-08-27 (Protocol v4.6)
**Project Owner:** Passagain P.
**Protocol Status:** Active & Enforced
