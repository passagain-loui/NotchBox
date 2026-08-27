# CLAUDE.md — NotchBox Project Development & Verification Protocol

## TRI-AGENT WORKFLOW PROTOCOL (v4.6 — COMPLETE MASTER SPECIFICATION)

This document outlines the collaborative workflow between three AI agents and the gatekeeper system for ensuring code quality and automated verification with ZERO-TOLERANCE anti-simulation enforcement and mandatory Log traceability.

### 1. AI ROLES & EXPLICIT RESPONSIBILITIES

- **Master Architect (Gemini):** ออกแบบสถาปัตยกรรมระดับสูง วิเคราะห์ภาพรวม และออกคำสั่งแบบ Structured Task (ห้ามลงมือแก้ไขโค้ดในโปรเจกต์โดยตรง)

- **Execution Engine (Claude Code / OpenCode):** รับคำสั่ง เขียนโค้ด คิดวิเคราะห์อย่างรอบด้านเพื่อให้แก้ไขจบได้ในครั้งเดียวและประหยัดโทเค็น ทำ Auto-Fix และรันคำสั่งตรวจสอบตามโปรโตคอล (ห้ามแอบอ้างผลลัพธ์หรือข้ามขั้นตอน)

- **Gatekeeper Auditor (LocalCore CLI):** สแกนตรวจสอบโค้ดแบบ Read-Only และพ่นค่า EXIT_CODE พร้อมบันทึก Log ลงระบบ (ห้ามแก้ไขโค้ดเอง)

---

### 2. PROJECT ROOT & MARKER VALIDATION RULE

- ก่อนสั่งรัน LocalCore ทุกครั้ง ต้องตรวจสอบและเปลี่ยนไดเรกทอรี (`cd`) เข้าไปในโฟลเดอร์หลักของโปรเจกต์ (Project Root) ที่มีไฟล์มาร์กเกอร์ (เช่น `pyproject.toml`, `package.json`, `NotchBox.csproj`) เรียบร้อยแล้ว
- ห้ามรันจากโฟลเดอร์แม่เด็ดขาดเพื่อป้องกันข้อผิดพลาด `no markers`
- หากพบปัญหานี้ให้ค้นหาโฟลเดอร์ Root และย้าย Working Directory ทันที

---

### 3. MANDATORY GATEKEEPER EXECUTION RULE (SILENT BACKGROUND MODE)

- เพื่อป้องกันไม่ให้หน้าต่าง LocalCore เด้งซ้อนทับหน้าต่าง Log หลัก Execution Engine ต้องรันคำสั่งผ่าน PowerShell แบบซ่อนหน้าต่างทุกครั้ง:
  ```powershell
  powershell -Command "$p = Start-Process -FilePath 'C:\Program Files\LocalCore\localcore.exe' -ArgumentList '--verify', '--model', 'Qwen-2.5-Coder-14B' -NoNewWindow -PassThru; $p.WaitForExit(); exit $p.ExitCode"
  ```

---

### 4. STRICT ANTI-SIMULATION & LOG TRACEABILITY (ZERO-TOLERANCE)

- ห้ามใช้คำสั่งเทสภายใน (เช่น `pytest`) แล้วนำ Exit Code มาอ้างอิงแทน Gatekeeper เด็ดขาด
- ข้อมูลต้องวิ่งผ่าน LocalCore CLI จริงเท่านั้น
- หากแสดง `EXIT_CODE: 0` แต่ไม่มีร่องรอย Log จะถือว่าเป็นโมฆะทันที

---

### 5. AUTOMATED RE-VERIFICATION LOOP

- รันคำสั่งผ่าน Gatekeeper หากได้ `EXIT_CODE: 0` ให้ไปขั้นตอน Release ทันที
- หาก `EXIT_CODE != 0` (FAIL): Execution Engine ห้ามหยุดหรือถามผู้ใช้
  - อ่าน Error Trace จาก Log
  - คิดวิเคราะห์รอบด้านและทำ Auto-Fix
  - รันคำสั่งซ้ำใน Terminal ทันที
  - ทำซ้ำจนกว่าจะได้ `EXIT_CODE: 0` เท่านั้น

---

### 6. STRICT VERSION BUMP & RELEASE PROTOCOL

- ห้ามทำ Version Bump, Build Binaries, สร้าง Git Tag หรือ Commit/Push เด็ดขาด จนกว่าจะมีหลักฐาน `EXIT_CODE: 0` จากการรัน LocalCore จริงยืนยัน

**Version Increment (SemVer):**
- **MAJOR (X.0.0):** เปลี่ยนแปลงสถาปัตยกรรมครั้งใหญ่ หรือมี Breaking Changes
- **MINOR (0.X.0):** เพิ่มฟีเจอร์ใหม่หรือฟังก์ชันหลักที่ผ่านการตรวจแล้ว
- **PATCH (0.0.X):** แก้ไขบั๊ก ปรับปรุงโค้ดภายใน หรือทำ Auto-Fix

**Mandatory Documentation & Audit Trail Sync:**
- ก่อน Build หรือ Commit ต้องอัปเดตเอกสารครบถ้วน:
  - `CHANGELOG.md` — บันทึกรายการเปลี่ยนแปลงและฟีเจอร์ใหม่
  - `HISTORY.md` — บันทึกประวัติและ Timestamp จากการรัน LocalCore
  - Version Variable ในโค้ด — ให้ตรงกันทุกจุด (เช่น `AppInfo.Version` ใน NotchBox.Core)

**Deployment Gateway:**
- Build ไฟล์ Binaries ต่อได้ทันที
- ทำ Git Commit ระบุเลขเวอร์ชัน (เช่น `"chore: release v0.3.10"`)
- Push ขึ้นรีโมทรีโปเป็นขั้นตอนสุดท้าย

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
| 4.6 (COMPLETE) | Tri-Agent + Master Specification | Active | Integrated: Roles, Markers, Gatekeeper, Anti-Simulation, Re-Verification, Version Bump Rules |
| 4.5 | Tri-Agent + Mandated Roles | Deprecated | Replaced by v4.6 |
| 4.4 | Tri-Agent + Strict Anti-Simulation | Deprecated | Replaced by v4.5 |

**Compliance:** All releases ≥0.1.0 must follow Protocol v4.6 (COMPLETE MASTER SPECIFICATION)

### Protocol v4.6 Key Enhancements:
✅ **Execution Engine Directive:** คิดวิเคราะห์อย่างรอบด้านเพื่อให้แก้ไขจบในครั้งเดียวและประหยัดโทเค็น
✅ **Project Root Validation:** บังคับตรวจสอบไฟล์มาร์กเกอร์ก่อนรัน LocalCore
✅ **Unified Workflow:** บรรจุ Version Bump & Release Protocol เข้าเป็นส่วนหนึ่งของ Workflow
✅ **Documentation Sync:** CHANGELOG.md + HISTORY.md + Version Variables ต้องอัปเดตครบถ้วน

---

**Last Updated:** 2026-08-27 (Protocol v4.6 — COMPLETE MASTER SPECIFICATION)
**Project Owner:** Passagain P.
**Protocol Status:** Active & Enforced
**Revision:** v4.6 (Final)
