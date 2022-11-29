$ProjectName = "ScreenHotKeys"
Copy-Item "..\src\bin\Release\net462\$ProjectName.dll" -Destination .
Copy-Item ..\README.md
Get-ChildItem -Exclude *.zip,*.ps1 | Compress-Archive -Force -DestinationPath ./$ProjectName.zip
