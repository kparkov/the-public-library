$paths = (dotnet run --project ./NoobSoft.PublicLibrary.DataFaker/NoobSoft.PublicLibrary.DataFaker.csproj)

foreach ($path in $paths) {
    Move-Item -Force -Path $path -Destination $HOME
    $fname = (Split-Path $path -Leaf)
    Write-Host ([System.IO.Path]::Combine($HOME, $fname))
}
