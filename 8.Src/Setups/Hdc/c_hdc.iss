; 脚本由 Inno Setup 脚本向导 生成！
; 有关创建 Inno Setup 脚本文件的详细资料请查阅帮助文档！

#define MyAppName               "数据采集"
#define MyAppVersion            "1.0.0.0"
#define MyAppPublisher          "LY-TECH"
#define MyAppURL                ""
#define MyAppExeName            "C3.exe"
#define MyAppDir                "C3"
#define CompanyName             "LY-TECH"

#define C3Dir                   "..\..\C3\bin\Debug\"
#define BCDir                   "..\..\C3\bin\Debug\bc\"
#define CRCDir                  "..\..\C3\bin\Debug\crc\"
#define ConfigDir               "..\..\C3\bin\Debug\Config\"

#define SpuDir                  "..\..\DBSPU\bin\Debug\"

#define OutputBaseName          "c3_hdc"
#define ContentSourceDir        "..\bin\Debug\"
#define ContentDestDir			"C"

#define SetupBin				"bin"

// 
// 2.0 3.5 4.0
//
#define DotNetFx20              "2.0"
#define DotNetFx35              "3.5"
#define DotNetFx40              "4.0"

#define DotNetFxNeed            "2.0"

#define MsgNeedDotNetFx20       "此安装程序需要 .NET Framework 版本 2.0, 请安装 .NET Framework 版本, 然后重新运行此安装程序."
#define MsgNeedDotNetFx35       "此安装程序需要 .NET Framework 版本 3.5, 请安装 .NET Framework 版本, 然后重新运行此安装程序."
#define MsgNeedDotNetFx40       "此安装程序需要 .NET Framework 版本 4.0, 请安装 .NET Framework 版本, 然后重新运行此安装程序."

[Setup]
; 注: AppId的值为单独标识该应用程序。
; 不要为其他安装程序使用相同的AppId值。
; (生成新的GUID，点击 工具|在IDE中生成GUID。)
AppId               ={{7803D7CA-5BB5-4A53-B110-06513D959DB9}
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
Source: "{#C3Dir}\C3.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#C3Dir}\C3.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#C3Dir}\C3.Communi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#C3Dir}\Xdgk.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#C3Dir}\Nlog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#C3Dir}\Nlog.Config"; DestDir: "{app}"; Flags: ignoreversion


; config
;
Source: "{#ConfigDir}\hp.xml"; DestDir: "{app}\Config"; Flags: ignoreversion
Source: "{#ConfigDir}\ListenPort.xml"; DestDir: "{app}\Config"; Flags: ignoreversion

Source: "{#ConfigDir}\spu_for_hdc.xml"; DestDir: "{app}\Config";  DestName: "spu.xml"; Flags: ignoreversion
Source: "{#ConfigDir}\dpu_for_hdc.xml"; DestDir: "{app}\Config"; DestName: "dpu.xml"; Flags: ignoreversion
Source: "{#ConfigDir}\Source_for_hdc.xml"; DestDir: "{app}\Config"; DestName: "Source.xml"; Flags: ignoreversion

; bc
;
Source: "{#C3Dir}\bc\*.dll"; DestDir: "{app}\bc"; Flags: ignoreversion

; crc
;
Source: "{#C3Dir}\crc\*.dll"; DestDir: "{app}\crc"; Flags: ignoreversion


; spu
;
Source: "{#SpuDir}\DBSpu.dll"; DestDir: "{app}\s"; Flags: ignoreversion

; scl6
;
Source: "..\..\SCL6DPU\bin\Debug\SCL6Dpu.dll"; DestDir: "{app}\d\scl6"; Flags: ignoreversion
Source: "..\..\SCL6DPU\bin\Debug\DeviceDefine\*.xml"; DestDir: "{app}\d\scl6\DeviceDefine"; Flags: ignoreversion
Source: "..\..\SCL6DPU\bin\Debug\Task\*.xml"; DestDir: "{app}\d\scl6\Task"; Flags: ignoreversion

; hd
;
Source: "..\..\HDDPU\bin\Debug\HDDpu.dll"; DestDir: "{app}\d\hd"; Flags: ignoreversion
Source: "..\..\HDDPU\bin\Debug\DeviceDefine\*.xml"; DestDir: "{app}\d\hd\DeviceDefine"; Flags: ignoreversion
Source: "..\..\HDDPU\bin\Debug\Task\*.xml"; DestDir: "{app}\d\hd\Task"; Flags: ignoreversion

; 7203
;
Source: "..\..\PS.Data7203DPU\bin\Debug\PS.DATA7203DPU.dll"; DestDir: "{app}\d\Data7203"; Flags: ignoreversion
Source: "..\..\PS.Data7203DPU\bin\Debug\DeviceDefine\*.xml"; DestDir: "{app}\d\Data7203\DeviceDefine"; Flags: ignoreversion
Source: "..\..\PS.Data7203DPU\bin\Debug\Task\*.xml"; DestDir: "{app}\d\Data7203\Task"; Flags: ignoreversion

; 注意: 不要在任何共享系统文件上使用“Flags: ignoreversion”

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
