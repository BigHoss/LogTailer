
[CmdletBinding()]
param(
    [String]$Name
)
if([string]::IsNullOrEmpty($Name))
{
    Write-Host "Enter Migration Name and press enter."

    $Name = Read-Host
}
if(![string]::IsNullOrEmpty($Name))
{
    dotnet ef migrations add $name --output-dir Migrations --context LogTailerContext --project LogTailer.Data --startup-project LogTailer.Ui

    $dbFile = '..\..\bin\Debug\net5.0-windows\logtailer.db'

    If(Test-Path -Path $dbFile)
    {
        Remove-Item -Path $dbFile
    }
}
