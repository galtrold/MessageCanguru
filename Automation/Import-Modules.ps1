if (Get-Module MessageBuild) {
    Write-Host "Unloading MessageBuild module"
    Remove-Module MessageBuild -Force
}
$modilePath = Join-Path $psscriptroot MessageBuild.psm1
Write-Host "Importing MessageBuild module containing:"
Write-Host "1) Invoke-Publish [webroot] - Publish dll and config to sitecore web folder"
Write-Host "2) Invoke-NugetPack - Creates a nuget package for nuget.com"
Import-Module $modilePath