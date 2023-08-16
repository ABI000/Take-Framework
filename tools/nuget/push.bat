
set ProjectName=%~1
set ProjectDir=%~2
set ApiKey=123456
set PublishMode=Release
set SourceUrl=http://localhost:5000/v3/index.json

del %ProjectDir%bin\%PublishMode%\%ProjectName%.*.nupkg /F /Q
dotnet pack %ProjectDir%%ProjectName%.csproj -c %PublishMode%
dotnet nuget push -s http://localhost:5000/v3/index.json %ProjectDir%bin\%PublishMode%\%ProjectName%.*.nupkg -k %ApiKey% --skip-duplicate
del %ProjectDir%bin\%PublishMode%\%ProjectName%.*.nupkg /F /Q
