$ProjectName = "ScreenHotKeys"
Copy-Item "src\bin\Release\net462\$ProjectName.dll" -Destination .
Compress-Archive -Path "$ProjectName.dll" -Force -DestinationPath ./$ProjectName.zip
