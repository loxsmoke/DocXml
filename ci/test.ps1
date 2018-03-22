function dotnet-test {
  Get-ChildItem -Path "test\**\*.csproj" | ForEach-Object {
    dotnet test $_ -c Release --no-build -l "trx;LogFileName=tests.xml"
    # upload results to AppVeyor
    $wc = New-Object 'System.Net.WebClient'
    $wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", (Join-Path $_.Directory.FullName TestResults\tests.xml))
  }
}

@( "dotnet-test" ) | ForEach-Object {
  echo ""
  echo "***** $_ *****"
  echo ""

  # Invoke function and exit on error
  &$_ 
  if ($LastExitCode -ne 0) { Exit $LastExitCode }
}