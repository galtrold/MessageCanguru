function Invoke-NugetPack {
    param (
        [string] $BuildConfiguration = "Release"
    )
    $modulePath = $psscriptroot;
    $nuspecFile =  Join-Path "$modulePath\..\" "MessageCanguru.nuspec"

    Invoke-Build $BuildConfiguration

    &nuget.exe pack $nuspecFile
}

function Invoke-Publish {
    param (
        [ValidateNotNullOrEmpty()]
        [string] $WebRoot

    )

    Invoke-Build

    $modulePath = $psscriptroot
    $confgiSource = "$modulePath\..\MessageCanguru\App_Config\Include\MessageCanguru"
    $outputPath =  Join-Path $modulePath output
    $configPath = "$WebRoot\App_Config\Include\MessageCanguru\"
    if(-not (Test-Path $configPath)) {
        mkdir $configPath
    }


    Copy-Item "$outputPath\Serilog*.dll" "$WebRoot\bin\"
    Copy-Item "$outputPath\MessageCanguru.*" "$WebRoot\bin\"

    Copy-Item "$confgiSource\*.config" "$WebRoot\App_Config\Include\MessageCanguru\"
    
}

function Invoke-Build {
    param (
        
        [string] $BuildConfiguration = "Debug"

    )
    
    if ( -not (Get-Module -ListAvailable -Name VSSetup)) {
        Write-Host "VSSetup not found. Installing VSSetup"
        Install-Module VSSetup -Force
    }
    
    if (-not (Get-Module VSSetup)) {
        Write-Host "VSSetup not imported. Importing VSSetup"
        Import-Module VSSetup
    }
    $latestVsInstallationInfo = Get-VSSetupInstance -All | Sort-Object -Property InstallationVersion -Descending | Select-Object -First 1
    
    if ($latestVsInstallationInfo.InstallationVersion -like "15.*") {
        $msbuildLocation = "$($latestVsInstallationInfo.InstallationPath)\MSBuild\15.0\Bin\msbuild.exe"
    }
    else {
        $msbuildLocation = "$($latestVsInstallationInfo.InstallationPath)\MSBuild\Current\Bin\msbuild.exe"
    }

    $modulePath = $psscriptroot
    $outputPath =  Join-Path $modulePath output
    $projectFile = Join-Path "$modulePath\..\" "MessageCanguru\MessageCanguru.csproj"
    
    Remove-Item "$outputPath\*.*"

    $buildArgs = "$projectFile /p:OutputPath=$outputPath /p:Configuration=$BuildConfiguration"
    Invoke-Expression "& `"$msbuildLocation`" $buildArgs"
}




