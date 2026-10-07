param([string]$AudioDirectory)
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$audioTarget = Join-Path $projectRoot 'Assets\Audio'
if ($AudioDirectory) {
    New-Item -ItemType Directory -Path $audioTarget -Force | Out-Null
    Get-ChildItem -LiteralPath $AudioDirectory -Filter '*.wav' | ForEach-Object {
        Copy-Item -LiteralPath $_.FullName -Destination $audioTarget -Force
    }
}
[xml]$project = Get-Content -LiteralPath (Join-Path $projectRoot 'ExploreTheHouse.csproj')
foreach ($asset in $project.Project.ItemGroup.Content) {
    if ($asset.Include -like 'Assets\Audio\*.wav' -and !(Test-Path -LiteralPath (Join-Path $projectRoot $asset.Include))) {
        throw "Missing audio asset: $($asset.Include). Supply -AudioDirectory with the original WAV files."
    }
}
$output = Join-Path $projectRoot 'artifacts\release'
dotnet publish (Join-Path $projectRoot 'ExploreTheHouse.csproj') -c Release -r win-x64 --self-contained true -o $output -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -p:DebugType=None -p:DebugSymbols=false -p:PublishTrimmed=false
if ($LASTEXITCODE -ne 0) { throw 'Publish failed.' }
Write-Host "Ready: $(Join-Path $output 'ExploreTheHouse.exe')"
