Sgen.exe /a:"..\ZScreen\bin\x86\Debug\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Debug\ZScreenLib.dll" /f /t:"ZScreenLib.XMLSettings" /t:"ZScreenLib.Workflow" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Debug\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /v

Sgen.exe /a:"..\ZScreen\bin\x86\Release\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Release\ZScreenLib.dll" /f /t:"ZScreenLib.XMLSettings" /t:"ZScreenLib.Workflow" /v
Sgen.exe /a:"..\ZScreen\bin\x86\Release\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /v

Sgen.exe /a:"..\ZUploader\bin\Debug\ZUploader.exe" /f /t:"ZUploader.Settings" /v
Sgen.exe /a:"..\ZUploader\bin\Debug\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZUploader\bin\Debug\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /v

Sgen.exe /a:"..\ZUploader\bin\Debug\ZUploader.exe" /f /t:"ZUploader.Settings" /v
Sgen.exe /a:"..\ZUploader\bin\Release\HelpersLib.dll" /f /t:"HelpersLib.AppSettings" /v
Sgen.exe /a:"..\ZUploader\bin\Release\UploadersLib.dll" /f /t:"UploadersLib.UploadersConfig" /t:"UploadersLib.GoogleTranslatorConfig" /v

pause