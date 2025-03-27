# Define the remote server and credentials
$server = "localhost\\SQLEXPRESS"   # Replace with the name or IP address of your server
$username = "dev"
$password = "test1234"
$connectionString = "Server=localhost\\SQLEXPRESS;Database=TeachPortal;Integrated Security=True;TrustServerCertificate=true;"

# Convert the password to a secure string
$securePassword = ConvertTo-SecureString $password -AsPlainText -Force

# Define the PowerShell script to create the user and update appsettings.json
$script = {
    param ($username, $securePassword, $connectionString)
    
    # Step 1: Create the new local user
    New-LocalUser -Name $username -Password $securePassword -FullName "Developer" -Description "Developer account"
    
    # Step 2: Optionally, add the user to a group (e.g., Administrators)
    Add-LocalGroupMember -Group "Administrators" -Member $username
    
    # Step 3: Modify appsettings.json to include the new connection string
    $appSettingsPath = "C:\Users\Satesh\source\repos\TeachPortal\TeachPortal\appsettings.json"  # Replace with actual path to appsettings.json

    if (Test-Path $appSettingsPath) {
        $appSettings = Get-Content -Path $appSettingsPath -Raw | ConvertFrom-Json

        # Check if "ConnectionStrings" section exists, otherwise create it
        if (-not $appSettings.PSObject.Properties['ConnectionStrings']) {
            $appSettings | Add-Member -MemberType NoteProperty -Name 'ConnectionStrings' -Value @{}
        }

        # Update or add the DefaultConnection string
        $appSettings.ConnectionStrings.DefaultConnection = $connectionString

        # Save the updated appsettings.json file
        $appSettings | ConvertTo-Json -Depth 10 | Set-Content -Path $appSettingsPath -Force
    } else {
        Write-Error "appsettings.json not found at path $appSettingsPath"
    }
}

# Run the script on the remote server using Invoke-Command
Invoke-Command -ComputerName $server -ScriptBlock $script -ArgumentList $username, $securePassword, $connectionString -Credential (Get-Credential)
