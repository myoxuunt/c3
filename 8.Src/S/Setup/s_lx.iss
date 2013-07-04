; �ű��� Inno Setup �ű��� ���ɣ�
; �йش��� Inno Setup �ű��ļ�����ϸ��������İ����ĵ���

#define MyAppName               "�������ݷ���"
#define MyAppVersion            "1.0.0.0"
#define MyAppPublisher          "LY-TECH"
#define MyAppURL                ""
#define MyAppExeName            "S.exe"
#define MyAppDir                "S"
#define CompanyName             "LY-TECH"

#define SDir                    "..\bin\Debug\"
#define BCDir                   "..\bin\Debug\bc\"
#define ConfigDir               "..\bin\Debug\Config\"

#define SpuDir                  "..\..\DBSPU\bin\Debug\"

#define OutputBaseName          "s_lx"

#define SetupBin                "bin"

// 
// 2.0 3.5 4.0
//
#define DotNetFx20              "2.0"
#define DotNetFx35              "3.5"
#define DotNetFx40              "4.0"

#define DotNetFxNeed            "2.0"

#define MsgNeedDotNetFx20       "�˰�װ������Ҫ .NET Framework �汾 2.0, �밲װ .NET Framework �汾, Ȼ���������д˰�װ����."
#define MsgNeedDotNetFx35       "�˰�װ������Ҫ .NET Framework �汾 3.5, �밲װ .NET Framework �汾, Ȼ���������д˰�װ����."
#define MsgNeedDotNetFx40       "�˰�װ������Ҫ .NET Framework �汾 4.0, �밲װ .NET Framework �汾, Ȼ���������д˰�װ����."

[Setup]
; ע: AppId��ֵΪ������ʶ��Ӧ�ó���
; ��ҪΪ������װ����ʹ����ͬ��AppIdֵ��
; (�����µ�GUID����� ����|��IDE������GUID��)
AppId               ={{1C0AD426-F86B-4257-86C1-3D1F6CF469FE}
AppName             ={#MyAppName}
AppVersion          ={#MyAppVersion}
;AppVerName         ={#MyAppName} {#MyAppVersion}
AppPublisher        ={#MyAppPublisher}
AppPublisherURL     ={#MyAppURL}
AppSupportURL       ={#MyAppURL}
AppUpdatesURL       ={#MyAppURL}
DefaultDirName      ={pf}\{#CompanyName}\{#MyAppDir}
DefaultGroupName    ={#MyAppName}
OutputDir           =.         
OutputBaseFilename  ={#SetupBin}\{#OutputBaseName}_{#MyAppVersion}
Compression         =lzma
SolidCompression    =yes

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#SDir}\S.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SDir}\S.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SDir}\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SDir}\Nlog.Config"; DestDir: "{app}"; Flags: ignoreversion


; config
;
Source: "{#ConfigDir}\*.*"; DestDir: "{app}\Config"; Flags: ignoreversion recursesubdirs

; bc
;
Source: "{#SDir}\bc\*.dll"; DestDir: "{app}\bc"; Flags: ignoreversion

; ע��: ��Ҫ���κι���ϵͳ�ļ���ʹ�á�Flags: ignoreversion��

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent unchecked


[code]
//
//
function IsDotNET20Detected(): boolean;
var
    success: boolean;
    install: cardinal;
begin
    success := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727', 'Install', install);
    Result := success and (install = 1);
end;


//
//
function IsDotNET35Detected(): boolean;
var
    success: boolean;
    install: cardinal;
begin
    success := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5', 'Install', install);
    Result := success and (install = 1);
end;


//
//
function IsDotNET40Detected(): boolean;
var
    success: boolean;
    install: cardinal;
begin
    success := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', install);
    Result := success and (install = 1);
end;

//
//
//
function CheckDotNetFx(): boolean;
var 
    success: boolean;
    message: string;
begin
    case {#DotNetFxNeed} of
        {#DotNetFx20}: begin success := IsDotNET20Detected(); message := '{#MsgNeedDotNetFx20}'; end;
        {#DotNetFx35}: begin success := IsDotNET35Detected(); message := '{#MsgNeedDotNetFx35}'; end;
        {#DotNetFx40}: begin success := IsDotNET40Detected(); message := '{#MsgNeedDotNetFx40}'; end;
    else
         MsgBox('.Net Framework version error: {#DotNetFxNeed}', mbCriticalError, MB_OK);
    end;
    
    if not success then 
    begin
        MsgBox( message, mbInformation, MB_OK);
    end;

    Result := success;
end;


//
//
//
function InitializeSetup(): Boolean;
begin
    Result := CheckDotNetFx();
end;
