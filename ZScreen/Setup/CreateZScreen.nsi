; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "ZScreen"
!define PRODUCT_VERSION "1.3.5.0"
!define PRODUCT_PUBLISHER "Brandon Zimmerman"
!define PRODUCT_WEB_SITE "http://brandonz.net"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\ZScreen.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"
!define RELPATH "..\ZScreen\bin\Release\"

; MUI 1.67 compatible ------
/*!include "MUI.nsh"*/
!include "${NSISDIR}\Contrib\Modern UI 2\MUI2.nsh"


!include "DotNET.nsh"
!include LogicLib.nsh
!define DOTNET_VERSION "2"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "license.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "ZScreen"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\ZScreen.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English" ;first language is the default language

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "ZScreenSetup.exe"
InstallDir "$PROGRAMFILES\ZScreen"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01

  Call CloseApp
  
  !insertmacro CheckDotNET ${DOTNET_VERSION}

  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "${RELPATH}ZScreen.exe"
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "${RELPATH}ImageUploader.dll"
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "license.txt"
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "disclaimers.txt"
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "docs.chm"
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  
;  CreateDirectory "$INSTDIR\el"
;  SetOutPath "$INSTDIR\el"
;  SetOverwrite ifnewer
;  File "${RELPATH}el\ZScreen.resources.dll"

; Puts output path back to the right place
SetOutPath "$INSTDIR"

; Shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\ZScreen.lnk" "$INSTDIR\ZScreen.exe"
  CreateShortCut "$DESKTOP\ZScreen.lnk" "$INSTDIR\ZScreen.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -AdditionalIcons
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk" "$INSTDIR\uninst.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\ZScreen.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\ZScreen.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Section -startup
  MessageBox MB_YESNO "Do you wish to have the program start automatically with windows?" IDYES DoWrite IDNO End
  DoWrite: WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Run" "ZScreen" "$INSTDIR\ZScreen.exe"
  End:
SectionEnd

Function .onInit

FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  
  Call un.CloseApp
  
  !insertmacro MUI_STARTMENU_GETFOLDER "Application" $ICONS_GROUP
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\ZScreen.exe"
  Delete "$INSTDIR\license.txt"
  Delete "$INSTDIR\docs.chm"

  Delete "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk"
  Delete "$DESKTOP\ZScreen.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\ZScreen.lnk"

  RMDir "$SMPROGRAMS\$ICONS_GROUP"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  DeleteRegValue HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Run" "ZScreen"
  SetAutoClose true
SectionEnd

Function CloseApp
Push $5

push "zscreen.exe"
  processwork::existsprocess
  pop $5
  IntCmp $5 0 done
  
  push "zscreen.exe"
  processwork::KillProcess

done:
Pop $5
FunctionEnd

Function un.CloseApp
Push $5

push "zscreen.exe"
  processwork::existsprocess
  pop $5
  IntCmp $5 0 done

  push "zscreen.exe"
  processwork::KillProcess

done:
Pop $5
FunctionEnd