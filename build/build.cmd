@echo off
rem Parameter: A.B.C the new version number for nuget, assembly and the file
dotnet tool install -g dotnet-property
pushd ..\src\DocXml
dotnet-property DocXml.csproj Version:%1
dotnet-property DocXml.csproj AssemblyVersion:%1.0
dotnet-property DocXml.csproj FileVersion:%1.0
dotnet build -c Release
popd
move ..\src\DocXml\bin\release\LoxSmoke.DocXml.%1.nupkg
