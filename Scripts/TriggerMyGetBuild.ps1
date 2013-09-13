$ErrorActionPreference = "Stop"
$project = "OpenMagic"

function GetWebHook()
{
    $userFolder = (get-item env:UserProfile).Value
    $configFile = [System.IO.Path]::Combine($userFolder, "MyGet.config")

    if (!(Test-Path $configFile))
    {
        throw "Cannot find config file $configFile. It is simple hash table for values that should not be stored in source code repository."
    }

    # Strongly typing $config ensures an exception will be thrown if the format of config file is incorrect.
    [System.Collections.Hashtable]$config = (Get-Content $configFile) -join "`n" | ConvertFrom-StringData

    $configKey = "$project.BuildServices.WebHook"

    if (!$config.ContainsKey($configKey))
    {
        throw "Cannot find $configKey in config file $configFile."
    }
    
    $url = $config.Item($configKey)

    return $url
}

function PostWebHook([string] $url)
{
    $webRequest = [System.Net.WebRequest]::Create($url);
    $webRequest.ServicePoint.Expect100Continue = $false;
    $webRequest.Method = "POST";
    $webRequest.ContentLength = 0;

    $response = $webRequest.GetResponse();
    $responseStream = $response.GetResponseStream();

    $streamReader = New-Object System.IO.StreamReader -argumentList $responseStream;
    $streamReader.ReadToEnd();
}

try
{
    $url = GetWebHook
    PostWebHook $url
}
catch
{
    # todo: use Write-Error
    Write-Host
    write-Host $error[0] -Foreground Red
}

Write-Host
Write-Host
Write-Host "Press any key to continue ..."

$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")