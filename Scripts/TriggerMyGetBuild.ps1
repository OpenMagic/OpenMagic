$ErrorActionPreference = "Stop"

function GetWebHook()
{
    $userFolder = (get-item env:UserProfile).Value
    $configFile = [System.IO.Path]::Combine($userFolder, "MyGet.config")

    if (!(Test-Path $configFile))
    {
        throw "Cannot find $configFile config file. It is simple hash table for values that should not be stored in source code repository."
    }

    $config = get-content -Path $configFile | ConvertFrom-StringData
    $url = $config.get_item("OpenMagic.BuildServices.WebHook")
    
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