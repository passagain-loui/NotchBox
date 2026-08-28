[Setup]
AppName=NotchBox
AppVersion=0.5.3.1
AppPublisher=Passagain P.
AppPublisherURL=https://github.com/passagain/notchbox
AppSupportURL=https://github.com/passagain/notchbox/issues
AppUpdatesURL=https://github.com/passagain/notchbox/releases
DefaultDirName={pf}\NotchBox
DefaultGroupName=NotchBox
AllowNoIcons=yes
OutputDir=installer
OutputBaseFilename=NotchBox-v0.5.3.1-Setup
Compression=lzma2/ultra
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64
MinVersion=10.0.19041
WizardStyle=modern
UninstallDisplayIcon={app}\NotchBox.exe

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "startup"; Description: "&Run NotchBox at startup"; GroupDescription: "Startup Options"; Flags: unchecked

[Files]
Source: "dependencies\Microsoft.WindowsAppRuntime.Redist.Installer.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall
Source: "NotchBox\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\NotchBox"; Filename: "{app}\NotchBox.exe"; WorkingDir: "{app}"
Name: "{group}\{cm:UninstallProgram,NotchBox}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\NotchBox"; Filename: "{app}\NotchBox.exe"; WorkingDir: "{app}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\NotchBox"; Filename: "{app}\NotchBox.exe"; WorkingDir: "{app}"; Tasks: quicklaunchicon

[Run]
Filename: "{tmp}\Microsoft.WindowsAppRuntime.Redist.Installer.exe"; Parameters: "--quiet --install"; StatusMsg: "Installing Windows App Runtime dependencies..."; Flags: waituntilterminated

Filename: "{app}\NotchBox.exe"; Description: "{cm:LaunchProgram,NotchBox}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueName: "NotchBox"; ValueType: string; ValueData: "{app}\NotchBox.exe"; Flags: uninsdeletevalue; Tasks: startup






