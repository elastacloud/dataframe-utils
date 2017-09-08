### AppVeyor specific code

#APPVEYOR_BUILD_VERSION=1.0.21

$Copyright = "Copyright (c) 2017 by Elastacloud Ltd."
$PackageIconUrl = "http://i.isolineltd.com/nuget/parquet.png"
$PackageProjectUrl = "https://github.com/elastacloud/dataframe-utils"
$RepositoryUrl = "https://github.com/elastacloud/dataframe-utils"
$Authors = "Richard Conway (@azurecoder), Ivan Gavryliuk (@aloneguid) and Andy Cross (@andycross)"
$PackageLicenseUrl = "https://github.com/elastacloud/parquet-dotnet/blob/master/LICENSE"
$RepositoryType = "GitHub"

$gv = $env:APPVEYOR_BUILD_VERSION
if($gv -eq $null)
{
   $gv = "1.0.0"
}

function Update-ProjectVersion($File)
{
   Write-Host "processing ${File.FullName}"

   $v = $gv

   $xml = [xml](Get-Content $File.FullName)

   if($xml.Project.PropertyGroup.Count -eq $null)
   {
      $pg = $xml.Project.PropertyGroup
   }
   else
   {
      $pg = $xml.Project.PropertyGroup[0]
   }

   $parts = $v -split "\."
   $bv = $parts[2]
   if($bv.Contains("-")) { $bv = $bv.Substring(0, $bv.IndexOf("-"))}
   $fv = "{0}.{1}.{2}.0" -f $parts[0], $parts[1], $bv
   $av = "{0}.0.0.0" -f $parts[0]
   $pv = $v

   $pg.Version = $pv
   $pg.FileVersion = $fv
   $pg.AssemblyVersion = $av

   Write-Host "$($File.Name) => fv: $fv, av: $av, pkg: $pv"

   $pg.Copyright = $Copyright
   $pg.PackageIconUrl = $PackageIconUrl
   $pg.PackageProjectUrl = $PackageProjectUrl
   $pg.RepositoryUrl = $RepositoryUrl
   $pg.Authors = $Authors
   $pg.PackageLicenseUrl = $PackageLicenseUrl
   $pg.RepositoryType = $RepositoryType

   $xml.Save($File.FullName)
}

function Remove-AllNuGetPackagePreviews($PackageId, $ApiKey, $Keyword, $PreviewsToKeep, $VersionsToMaintain)
{
    $lower = $PackageId.ToLowerInvariant();

    $json = Invoke-WebRequest -Uri "https://api.nuget.org/v3-flatcontainer/$lower/index.json" | ConvertFrom-Json

    $filteredVersions = $json.versions | Where-Object { $_ -like "*$Keyword*"} | Select-Object -Last $VersionsToMaintain
    $count = $filteredVersions.Count
    $removed = 0

    Write-Host "found $count versions"

    foreach($version in $filteredVersions)
    {
      $versionsLeft = $count - $removed

      if($versionsLeft -lt $PreviewsToKeep)
      {
          Write-Host "only $versionsLeft left, need to keep $PreviewsToKeep"
          break;
      }

      Write-Host "Unlisting $PackageId, Ver $version"

      Invoke-Expression "nuget delete $PackageId $version $ApiKey -source https://api.nuget.org/v3/index.json -NonInteractive"

      $removed += 1
    }
}

Invoke-Expression "nuget restore src/DataFrame.sln"

# Update versioning information
Get-ChildItem *.csproj -Recurse | Where-Object {-not(($_.Name -like "*test*")) } | % {
   Update-ProjectVersion $_
}

Remove-AllNuGetPackagePreviews -PackageId "DataFrame.Math" -ApiKey $env:NUGET_KEY -Keyword "alpha" -PreviewsToKeep 3 -VersionsToMaintain 6
