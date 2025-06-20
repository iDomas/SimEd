dotnet publish  ..\..\SimEd\SimEd.csproj -r win-x64 -c Release -o win-x64 -p:PublishDir=.\win-x64
dotnet publish  ..\..\SimEd\SimEd.csproj -r win-x86 -c Release -o win-x86 -p:PublishDir=.\win-x86

rem "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" InstallerWin32.iss
rem "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" InstallerWin64.iss
