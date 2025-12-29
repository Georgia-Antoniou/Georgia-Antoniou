param(
[Parameter(Mandatory=$false)]
[string] $RootPath
)

$githubUrl = "https://github.com/Georgia-Antoniou/Georgia-Antoniou/commit/"

if([string]::IsNullOrWhitespace($RootPath))
{
    $RootPath = "C:\Users\Georgia\source\repos\";
}

$commitHash = (& git log -1 --pretty=format:"%H")
$commitMessage = (& git log -1 --pretty=format:"%s")

$tempFolder = "C:\temp\georgia-antoniou\publish";

Remove-Item $tempFolder -Recurse -Force

$webSite = Join-Path $RootPath -ChildPath 'Georgia-Antoniou\MyPortfolio\MyPortfolio.csproj' -Resolve
dotnet publish $webSite -c Release -o $tempFolder

$tempWwwRoot = "$tempFolder\wwwroot\*"

$githubWebSite =  Join-Path $RootPath -ChildPath 'Georgia-Antoniou.github.io' -Resolve

Copy-Item -Path $tempWwwRoot  -Destination $githubWebSite -Recurse -Force


Copy-Item -Path "$githubWebSite\index.html" -Destination "$githubWebSite\404.html" -Force

$originalFolder = $pwd

Set-Location $githubWebSite
git add . 
$deployMessage = "Deploy " + (Get-Date) + [Environment]::NewLine + "Hash: $githubUrl/$commitHash" + [Environment]::NewLine + "Message: $commitMessage";
git commit -m $deployMessage
git push origin

Set-Location $originalFolder