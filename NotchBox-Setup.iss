[Setup]
AppName=NotchBox
AppVersion=0.6.1.2
AppPublisher=Passagain P.
DefaultDirName={pf}\NotchBox
DefaultGroupName=NotchBox
OutputDir=installer
OutputBaseFilename=NotchBox-v0.6.1.2-Setup
Compression=lzma2/ultra
ArchitecturesInstallIn64BitMode=x64
MinVersion=10.0.19041
WizardStyle=modern

[Files]
Source: "publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\NotchBox"; Filename: "{app}\NotchBox.exe"
Name: "{commondesktop}\NotchBox"; Filename: "{app}\NotchBox.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"

[Run]
Filename: "{app}\NotchBox.exe"; Flags: nowait postinstall skipifsilent
