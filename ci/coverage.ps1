Param (
  [switch] $generateReport,
  [switch] $uploadCodecov
  )

$currentPath = Split-Path $MyInvocation.MyCommand.Path
$coverageOutputDirectory = Join-Path $currentPath "coverage"
$coverageFile = "coverage-results.xml"

Remove-Item $coverageOutputDirectory -Force -Recurse -ErrorAction SilentlyContinue
Remove-Item $coverageFile -ErrorAction SilentlyContinue
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
$openCoverConsole = "packages\OpenCover.4.6.519\tools\OpenCover.Console.exe"

# OpenCover currently not supporting portable pdbs (https://github.com/OpenCover/opencover/issues/601)

$configuration = "Coverage"
Get-ChildItem -Filter .\test\ |
    ForEach-Object {
      $csprojPath = $_.FullName
      $testProjectName = $_.Name
# Project name being tested is unit test project name with UnitTests removed. "UnitTests" length == 9 
      $projectName = $testProjectName -replace ".{9}$"
        cmd.exe /c $openCoverConsole `
          -target:"c:\Program Files\dotnet\dotnet.exe" `
          -targetargs:"test -c $configuration $csprojPath\$testProjectName.csproj" `
          -mergeoutput `
          -hideskipped:File `
          -output:$coverageFile `
          -oldStyle `
          -filter:"+[$projectName]* -[$testProjectName]*" `
          -searchdirs:"$csprojPath\bin\$configuration\netcoreapp2.0\" `
          -register:user
    }

If ($generateReport) 
{
  nuget install -Verbosity quiet -OutputDirectory packages -Version 2.4.5.0 ReportGenerator
  $reportGenerator = "packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe"
  cmd.exe /c $reportGenerator `
    -reports:$coverageFile `
    -targetdir:$coverageOutputDirectory `
    -verbosity:Error
}

If ($uploadCodeCov) 
{
  nuget install -Verbosity quiet -OutputDirectory packages -Version 1.0.1 Codecov
  $Codecov = "packages\Codecov.1.0.1\tools\Codecov.exe"
  cmd.exe /c $Codecov -f $coverageFile
}
