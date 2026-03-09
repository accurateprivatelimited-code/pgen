[Setup]
AppName=PGen
AppVersion=1.0
WizardStyle=modern
DefaultDirName={autopf}\PGen
DefaultGroupName=PGen
Compression=lzma2
SolidCompression=yes
OutputDir=C:\Users\SHAMIM ABBAS\Desktop\PGen
OutputBaseFilename=PGen
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
SetupIconFile=C:\Users\SHAMIM ABBAS\Documents\GitHub\pgen\PGen\bin\Debug\net8.0-windows\icon_image.ico
[Files]
Source: "C:\Users\SHAMIM ABBAS\Documents\GitHub\pgen\PGen\bin\Release\net8.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
; Copy icon file explicitly
Source: "C:\Users\SHAMIM ABBAS\Documents\GitHub\pgen\PGen\bin\Debug\net8.0-windows\icon_image.ico"; DestDir: "{app}"
[Icons]
Name: "{group}\PGen"; Filename: "{app}\PGen.exe"
Name: "{commondesktop}\PGen"; Filename: "{app}\PGen.exe"; IconFilename: "{app}\icon_image.ico"
[Dirs]
Name: "{app}\data"; Permissions: users-full