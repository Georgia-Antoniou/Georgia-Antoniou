param(
[Parameter(Mandatory=$false)]
[string] $RootPath
)

if([string]::IsNullOrWhitespace($RootPath))
{
    $RootPath = "C:\Users\Georgia\source\repos\";
}

$tempFolder = "C:\temp\georgia-antoniou\publish";

Remove-Item $tempFolder -Recurse -Force

$webSite = Join-Path $RootPath -ChildPath 'Georgia-Antoniou\MyPortfolio\MyPortfolio.csproj' -Resolve
dotnet publish $webSite -c Release -o $tempFolder

$tempWwwRoot = "$tempFolder\wwwroot\*"

$githubWebSite =  Join-Path $RootPath -ChildPath 'Georgia-Antoniou.github.io' -Resolve

Copy-Item -Path $tempWwwRoot  -Destination $githubWebSite -Recurse -Force


Copy-Item -Path "$githubWebSite\index.html" -Destination "$githubWebSite\404.html" -Force