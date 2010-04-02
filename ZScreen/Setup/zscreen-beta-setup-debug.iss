; Based on the original AllOrCurrent.iss
; Dynamically change the locations of various installed elements
; Installed latest Microsoft .NET Framework if run as Administrator
; during the installation process based on user type, i.e.:
;
;      Current User or All Users
;
; Author: Dave Silvia; Mike Delpach
; EMail: queries@wxcodex.net; mcored@gmail.com

#include "scripts\products.iss"
#include "scripts\products\winversion.iss"
#include "scripts\products\fileversion.iss"
#include "scripts\products\msi20.iss"
#include "scripts\products\msi31.iss"
#include "scripts\products\dotnetfx35.iss"
#include "scripts\products\dotnetfx35sp1.iss"

#define SimpleVersion(str S);; \
	Local[0] = Pos (".0.0.", S),;; \
	/* (4) and (5) */;; \
	(Local[0] > 0) ? Copy (S, 1, 3) :;; \
	(;; \
		Local[0] = Pos (".0.0", S),;; \
		/* (3) */;; \
		(Local[0] > 0) ? Copy (S, 1, 3) :;; \
		(;; \
			Local[0] = Pos (".0", S),;; \
			/* (2) */;; \
			(Local[0] > 5) ? Copy (S, 1, Local[0] - 1) :;; \
			(;; \
				Local[0] = Pos (".0.", S),;; \
				/* (6) */;; \
				(Local[0] > 0) ? Copy (S, 1, 3) :;; \
				(;; \
					Copy (S, 1, 5);; \
				);; \
			);; \
		);; \
	);

#define ExeName "ZScreen"
#define ExePath "..\bin\Debug\ZScreen.exe"
#define MyAppVersion GetFileVersion(ExePath)

[Code]
var
  yNreply: Integer;
  yNreply2: Integer;
  ErrorCode: Integer;
  defaultDirectory: String;
  defaultMenuFolder: String;
  applicationIdentifier: String;
  userType: String;
  userSystemName: String;
  relativeStartMenuPrograms: String;
  profileDirectory: String;
  whichDesktop: String;
  
function InitializeSetup(): Boolean;
begin
  relativeStartMenuPrograms:='Start Menu\Programs';
  defaultDirectory:=ExpandConstant('{localappdata}');
  profileDirectory:=GetEnv('USERPROFILE');
  defaultMenuFolder:=profileDirectory + '\'+relativeStartMenuPrograms;
  userSystemName:=ExpandConstant('{username}');
  userType:='CurrentUser';
  whichDesktop:=profileDirectory+'\Desktop';
  Result:=true;
  if IsAdminLoggedOn = True then
  begin
    yNreply:=MsgBox('As an administrator, you can install {#ExeName} system wide or only for the user account that you are logged into.'
                     + #13#10#13#10 + 'Is this a system wide installation?',mbConfirmation,MB_YESNO);
    if yNreply = IDYES then
    begin
      userType:='AllUsers';
      defaultDirectory:=ExpandConstant('{pf}');
      profileDirectory:=GetEnv('ALLUSERSPROFILE');
      defaultMenuFolder:=profileDirectory+'\'+relativeStartMenuPrograms;
      whichDesktop:=profileDirectory+'\Desktop';
      userSystemName:=userSystemName+'_Administrator';
      
      initwinversion();
      dotnetfx35();
      dotnetfx35sp1();
    end;
  end
  else
   yNreply2:=MsgBox('As a Standard User, you can only install {#ExeName} for the user account that you are logged into.'
                    + #13#10#13#10 + 'For a system wide installation, please press No, right click the setup and Run As Administrator.'
                    + #13#10#13#10 + 'Do you want to continue?',mbConfirmation,MB_YESNO);
   if yNreply2 = IDNO then
   Result:=false;
end;

// This function passes the 'Script Constants' to the non-code
// sections above.  These are populated in the function
// following this one...
function GetCodeVar(Param: String): String;
var tmpVar: String;
begin
  if CompareText(Param,'DefaultDirName') = 0 then
    Result:=defaultDirectory
  else
  if CompareText(Param,'StartMenu') = 0 then
  begin
    if IsAdminLoggedOn = True then
    begin
    tmpVar:=ExpandConstant('{group}');
    tmpVar:=ExtractFileName(tmpVar);
    Result:=defaultMenuFolder+'\'+tmpVar;
    end
    else
    Result:=ExpandConstant('{group}');
  end
  else
  if CompareText(Param,'Uninstaller') = 0 then
  begin
    tmpVar:=ExpandConstant('{uninstallexe}');
    tmpVar:=ExtractFileName(tmpVar);
    Result:=ExpandConstant('{app}')+'\'+tmpVar;
  end
  else
  if CompareText(Param,'Desktop') = 0 then
    Result:=whichDesktop
  ;
end;

[CustomMessages]
win2000sp3_title=Windows 2000 Service Pack 3
winxpsp2_title=Windows XP Service Pack 2


[Setup]
AllowNoIcons=true
AppMutex=Global\0167D1A0-6054-42f5-BA2A-243648899A6B
AppId={#ExeName}
AppName={#ExeName}
AppPublisher={#ExeName}
AppPublisherURL=http://code.google.com/p/zscreen
AppSupportURL=http://code.google.com/p/zscreen/issues/list
AppUpdatesURL=http://code.google.com/p/zscreen/downloads/list
AppVerName={#ExeName} {#MyAppVersion}
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x86 x64 ia64
ArchitecturesInstallIn64BitMode=x64 ia64
Compression=lzma/ultra64
CreateAppDir=true
DefaultDirName={code:GetCodeVar|DefaultDirName}\{#ExeName}
DefaultGroupName={#ExeName}
DirExistsWarning=no
InfoAfterFile=..\..\ZScreenLib\Documents\license.txt
InfoBeforeFile=..\..\ZScreenLib\Documents\VersionHistory.txt
InternalCompressLevel=ultra64
LanguageDetectionMethod=uilanguage
MinVersion=4.90.3000,5.0.2195sp3
OutputBaseFilename={#ExeName}-{#MyAppVersion}-debug-setup
OutputDir=..\..\..\Output\
PrivilegesRequired=none
;SetupIconFile=..\Resources\zss-main.ico
ShowLanguageDialog=auto
ShowUndisplayableLanguages=false
SignedUninstaller=false
SolidCompression=true
Uninstallable=true
UninstallDisplayIcon={app}\{#ExeName}.exe
UsePreviousAppDir=no
UsePreviousGroup=no
VersionInfoCompany={#ExeName}
VersionInfoTextVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}

[Languages]
Name: en; MessagesFile: compiler:Default.isl
Name: de; MessagesFile: compiler:Languages\German.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
Source: ..\bin\Debug\*.exe; Excludes: *.vshost.exe; DestDir: {app}; Flags: ignoreversion
Source: ..\bin\Debug\*.dll; DestDir: {app}; Flags: ignoreversion recursesubdirs
Source: ..\..\..\branches\ZUploader\ZUploader\bin\Debug\*.exe; Excludes: *.vshost.exe; DestDir: {app}; Flags: ignoreversion

[Icons]
Name: {code:GetCodeVar|StartMenu}\{#ExeName}; Filename: {app}\{#ExeName}.exe; AppUserModelID: {#ExeName}
;Name: "{group}\ZScreen Manual"; Filename: "{app}\ZScreen-manual.pdf"
Name: {code:GetCodeVar|Desktop}\{#ExeName}; Filename: {app}\{#ExeName}.exe; Tasks: desktopicon
;Name: "{group}\Uninstall {#ExeName}"; Filename: "{uninstallexe}"

[Run]
Filename: {app}\{#ExeName}.exe.; Description: {cm:LaunchProgram,ZScreen}; Flags: nowait postinstall skipifsilent
Filename: {app}\ZUploader.exe.; Description: {cm:LaunchProgram,ZUploader}; Flags: nowait unchecked postinstall skipifsilent
;Filename: "{app}\ZScreen-manual.pdf"; Description: "{cm:LaunchProgram,ZScreen Manual}"; Flags: nowait unchecked postinstall shellexec skipifsilent

[InstallDelete]
Type: filesandordirs; Name: {app}\ru
Type: filesandordirs; Name: {app}\en
Type: filesandordirs; Name: {app}\nl
Type: files; Name: {app}\ZSS.ImageUploader.dll
Type: files; Name: {app}\ZSS.TextUploader.dll
Type: files; Name: {app}\ImageUploader.dll
Type: files; Name: {app}\ZSS.Colors.dll
