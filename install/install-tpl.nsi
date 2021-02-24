;NSIS Modern User Interface version 1.70
;Multilingual Example Script
;Written by Joost Verburg

;--------------------------------
;Include Modern UI

  !include "MUI.nsh"
  !include "FileAssociation.nsh"

;--------------------------------
;General

  ;Name and file
  Name "DatAdmin"
  OutFile "datadmin-install.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\DatAdmin#VERTYPE#"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\DatAdmin#VERTYPE#" ""

;--------------------------------
;Variables

  ;Var LANGID
  Var STARTMENU_FOLDER
  Var MUI_TEMP

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING

;--------------------------------
;Language Selection Dialog Settings

  ;Remember the installer language
  !define MUI_LANGDLL_REGISTRY_ROOT "HKCU" 
  !define MUI_LANGDLL_REGISTRY_KEY "Software\DatAdmin#VERTYPE#" 
  !define MUI_LANGDLL_REGISTRY_VALUENAME "Installer Language"

;--------------------------------
;Pages

  LicenseLangString myLicenseData 1033 "license-en.txt"
  LicenseLangString myLicenseData 1029 "license-cz.txt"
  LicenseLangString myLicenseData 1040 "license-en.txt"
  LicenseLangString myLicenseData 2052 "license-en.txt"

  LangString LANGID 1033 "en"
  LangString LANGID 1029 "cz"
  LangString LANGID 1040 "it"
  LangString LANGID 1036 "fr"
  LangString LANGID 2052 "sc"

  !insertmacro MUI_PAGE_LICENSE $(myLicenseData)
  !insertmacro MUI_PAGE_DIRECTORY
  
  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\DatAdmin#VERTYPE#" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"

  !insertmacro MUI_PAGE_STARTMENU Application $STARTMENU_FOLDER
  
  !insertmacro MUI_PAGE_INSTFILES
  
  !define MUI_FINISHPAGE_RUN
  !define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
;Launch function

Function LaunchLink
  ExecShell "" "$INSTDIR\DatAdmin.exe"
FunctionEnd

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"
  !insertmacro MUI_LANGUAGE "Czech"
  !insertmacro MUI_LANGUAGE "Italian"
  !insertmacro MUI_LANGUAGE "French"
  !insertmacro MUI_LANGUAGE "SimpChinese"


;--------------------------------
; .NET Stuff

	!include WordFunc.nsh
	!insertmacro VersionCompare
	!include LogicLib.nsh

	Function GetDotNETVersion
		Push $0
		Push $1
		
		System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1"
		StrCmp $1 "error" 0 +2
		StrCpy $0 "not found"
		
		Pop $1
		Exch $0
	FunctionEnd

	Function InstallDotNET
        MessageBox MB_OK|MB_ICONSTOP "Microsoft .NET Framework 2.0 is not installed. Please install .NET 2.0 first, or go to website http://datadmin.com for more information"
        Quit
		;MessageBox MB_OK|MB_ICONSTOP ".NET runtime library is not installed. The .NET installer will now run. After .NET has been installed, installion of DatAdmin will resume."
		;SetOutPath $TEMP
		;File "dotnetfx.exe"
		;ExecWait '"$TEMP\dotnetfx.exe"'
	FunctionEnd

;--------------------------------
;Reserve Files
  
  ;These files should be inserted before other files in the data block
  ;Keep these lines before any File command
  ;Only for solid compression (by default, solid compression is enabled for BZIP2 and LZMA)
  
  !insertmacro MUI_RESERVEFILE_LANGDLL

;--------------------------------
;Installer Sections

Section "Program" Program

  SimpleSC::ServiceIsRunning "DatAdminService"
  Pop $0
  Pop $0
  IntCmp $0 0 ServiceRunningLabel +1 +1 
    SimpleSC::StopService "DatAdminService"
    Pop $0
    Processes::KillProcess "DatAdminService"
    sleep 20000
  ServiceRunningLabel:

#CREATE_CONTENT#

  nsExec::Exec '"$INSTDIR\DatAdmin.install.exe"'
  Delete "$INSTDIR\DatAdmin.install.exe"
    
  ;Store installation folder
  WriteRegStr HKCU "Software\DatAdmin#VERTYPE#" "" $INSTDIR
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$STARTMENU_FOLDER"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\DatAdmin#SPACEVERTYPE#.lnk" "$INSTDIR\DatAdmin.exe"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Uninstall#SPACEVERTYPE#.lnk" "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END
  
  SimpleSC::StartService "DatAdminService"
  Pop $0
  
  ${RegisterExtension} "$INSTDIR\DatAdmin.exe" ".dca" "DatAdmin configuration archive"
SectionEnd

;--------------------------------
;Installer Functions

Function .onInit

  !insertmacro MUI_LANGDLL_DISPLAY

	Call GetDotNETVersion
	Pop $0
	${If} $0 == "not found"
		Call InstallDotNET
		Return
	${EndIf}
	
	StrCpy $0 $0 "" 1 # skip "v"
	
	${VersionCompare} $0 "2.0" $1
	${If} $1 == 2
		MessageBox MB_OK|MB_ICONSTOP ".NET runtime library v2.0 or newer is required. You have $0."
		Call InstallDotNET
		Return
;		${Else}
;			MessageBox MB_OK|MB_ICONSTOP "The correct version of the .NET runtime library is installed."
	${EndIf}

FunctionEnd

;--------------------------------
;Uninstaller Section

Section "Uninstall"
  ${UnregisterExtension} ".dca" "DatAdmin configuration archive"  

  SimpleSC::ServiceIsRunning "DatAdminService"
  Pop $0
  Pop $0
  IntCmp $0 0 ServiceRunningLabel2 +1 +1 
    SimpleSC::StopService "DatAdminService"
    Pop $0
    Processes::KillProcess "DatAdminService"
    sleep 20000
  ServiceRunningLabel2:
  
  SimpleSC::RemoveService "DatAdminService"
  Pop $0

#DELETE_CONTENT#

  !insertmacro MUI_STARTMENU_GETFOLDER Application $MUI_TEMP
  Delete "$SMPROGRAMS\$MUI_TEMP\DatAdmin#SPACEVERTYPE#.lnk"
  Delete "$SMPROGRAMS\$MUI_TEMP\Uninstall#SPACEVERTYPE#.lnk"

  ;Delete empty start menu parent diretories
  StrCpy $MUI_TEMP "$SMPROGRAMS\$MUI_TEMP"
 
  startMenuDeleteLoop:
	ClearErrors
    RMDir $MUI_TEMP
    GetFullPathName $MUI_TEMP "$MUI_TEMP\.."
    
    IfErrors startMenuDeleteLoopDone
  
    StrCmp $MUI_TEMP $SMPROGRAMS startMenuDeleteLoopDone startMenuDeleteLoop
  startMenuDeleteLoopDone:

  DeleteRegKey HKCU "Software\DatAdmin#VERTYPE#"
SectionEnd

;--------------------------------
;Uninstaller Functions

Function un.onInit

  !insertmacro MUI_UNGETLANGUAGE
  
FunctionEnd
