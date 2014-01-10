Write-Host "Running MyGet.ps1..."
Write-Host "args.Count: $($args.Count)."

for ($i = 0; $i -lt $args.Count; $i++) 
{
    Write-Host "args[$i]: $(args[$i])"
}