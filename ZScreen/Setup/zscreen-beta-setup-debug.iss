#include "scripts\products.iss"

#include "scripts\products\winversion.iss"
#include "scripts\products\fileversion.iss"

//#include "scripts\products\iis.iss"

//#include "scripts\products\kb835732.iss"
//#include "scripts\products\kb886903.iss"
//#include "scripts\products\kb928366.iss"

#include "scripts\products\msi20.iss"
#include "scripts\products\msi31.iss"
//#include "scripts\products\ie6.iss"

//#include "scripts\products\dotnetfx11.iss"
//#include "scripts\products\dotnetfx11lp.iss"
//#include "scripts\products\dotnetfx11sp1.iss"

//#include "scripts\products\dotnetfx20.iss"
//#include "scripts\products\dotnetfx20lp.iss"
//#include "scripts\products\dotnetfx20sp1.iss"
//#include "scripts\products\dotnetfx20sp1lp.iss"
//#include "scripts\products\dotnetfx20sp2.iss"
//#include "scripts\products\dotnetfx20sp2lp.iss"

#include "scripts\products\dotnetfx35.iss"
//#include "scripts\products\dotnetfx35lp.iss"
#include "scripts\products\dotnetfx35sp1.iss"
//#include "scripts\products\dotnetfx35sp1lp.iss"

//#include "scripts\products\mdac28.iss"
//#include "scripts\products\jet4sp8.iss"
//#include "scripts\products\sql2005express.iss"

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

[CustomMessages]
win2000sp3_title=Windows 2000 Service Pack 3
winxpsp2_title=Windows XP Service Pack 2


[Setup]
AllowNoIcons=true
AppMutex=Global\0167D1A0-6054-42f5-BA2A-243648899A6B
AppName={#ExeName}
AppPublisher=ZScreen
AppPublisherURL=http://code.google.com/p/zscreen
AppSupportURL=http://code.google.com/p/zscreen/issues/list
AppUpdatesURL=http://code.google.com/p/zscreen/downloads/list
AppVerName={#ExeName} {#MyAppVersion}
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x86 x64 ia64
ArchitecturesInstallIn64BitMode=x64 ia64
Compression=lzma/ultra64
CreateAppDir=true
DefaultDirName={pf}\ZScreen
DefaultGroupName={#ExeName}
DirExistsWarning=no
InfoAfterFile=..\..\ZScreenLib\Documents\license.txt
InfoBeforeFile=..\..\ZScreenLib\Documents\VersionHistory.txt
InternalCompressLevel=ultra64
LanguageDetectionMethod=uilanguage
OutputBaseFilename={#ExeName}-{#MyAppVersion}-debug-setup
OutputDir=..\..\..\Output\
;SetupIconFile=..\Resources\zss-main.ico
ShowUndisplayableLanguages=false
SignedUninstaller=false
SolidCompression=true
Uninstallable=true
UninstallDisplayIcon={app}\ZScreen.exe
UsePreviousAppDir=true
UsePreviousGroup=true
VersionInfoCompany={#ExeName}
VersionInfoDescription={#ExeName}
VersionInfoTextVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}

;required by products
MinVersion=4.90.3000,5.0.2195sp3
PrivilegesRequired=admin
AppID=ZScreen

[Languages]
Name: en; MessagesFile: compiler:Default.isl
Name: de; MessagesFile: compiler:Languages\German.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
Source: ..\bin\Debug\*.exe; Excludes: *.vshost.exe; DestDir: {app}; Flags: ignoreversion
Source: ..\bin\Debug\*.dll; DestDir: {app}; Flags: ignoreversion recursesubdirs

[Icons]
Name: {group}\{#ExeName}; Filename: {app}\ZScreen.exe; AppUserModelID: ZScreen
;Name: "{group}\ZScreen Manual"; Filename: "{app}\ZScreen-manual.pdf"
Name: {userdesktop}\{#ExeName}; Filename: {app}\ZScreen.exe; Tasks: desktopicon
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\ZScreen; Filename: {app}\ZScreen.exe.; Tasks: quicklaunchicon
;Name: "{group}\Uninstall {#ExeName}"; Filename: "{uninstallexe}"

[Run]
Filename: {app}\ZScreen.exe.; Description: {cm:LaunchProgram,ZScreen}; Flags: nowait postinstall skipifsilent
;Filename: "{app}\ZScreen-manual.pdf"; Description: "{cm:LaunchProgram,ZScreen Manual}"; Flags: nowait unchecked postinstall shellexec skipifsilent

[InstallDelete]
Type: filesandordirs; Name: {app}\ru
Type: filesandordirs; Name: {app}\en
Type: filesandordirs; Name: {app}\nl
Type: files; Name: {app}\ZSS.ImageUploader.dll
Type: files; Name: {app}\ZSS.TextUploader.dll
Type: files; Name: {app}\ImageUploader.dll
Type: files; Name: {app}\ZSS.Colors.dll

[Code]
function InitializeSetup(): Boolean;
var
          ResultCode: Integer;
begin

	//Simple hack to remove old NSIS-installed ZScreen (v1.3.3.0 or earlier)
	if FileExists(ExpandConstant('c:\program files\zscreen\uninst.exe')) then
  begin

        begin
          if Exec(ExpandConstant('c:\program files\zscreen\uninst.exe'), '', '', SW_SHOW,
            ewWaitUntilTerminated, ResultCode) then
          begin
            // success
          end
          else begin
            // failure
          end;
        end;

  end;

  initwinversion();

	//check if dotnetfx20 can be installed on this OS
	if not minwinspversion(5, 0, 3) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('win2000sp3_title')]), mbError, MB_OK);
		exit;
	end;
	if not minwinspversion(5, 1, 2) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('winxpsp2_title')]), mbError, MB_OK);
		exit;
	end;

	//if (not iis()) then exit;

	msi20('2.0');
	msi31('3.1');
//	ie6('5.0.2919');

	//dotnetfx11();
	//dotnetfx11lp();
	//dotnetfx11sp1();

//	kb835732();

//	if (minwinversion(5, 0) and minspversion(5, 0, 4)) then begin
//		dotnetfx20sp1();
//		dotnetfx20sp1lp();
//	end else begin
//		dotnetfx20();
//		dotnetfx20lp();
//	end;

dotnetfx35();
	//dotnetfx35lp();
dotnetfx35sp1();
	//dotnetfx35sp1lp();

//	mdac28('2.7');
//	jet4sp8('4.0.8015');

	Result := true;
end;




