param(
	[Parameter(Mandatory=$true)]
	[string]$Drive,

	[Parameter(Mandatory=$true)]
	[string]$Dest
)

#Validate Dir
if(-not (Test-Path $Drive)){
	Write-Error "Directory '$Drive' does not exist"
	exit 1
}

if(-not (Test-Path $Dest)){
	Write-Error "Directory '$Dest' does not exist"
	exit 1
}

# Get files
$files = Get-ChildItem -Path $Drive -File

# Apply Commands
try{
	foreach($file in $files){
		Write-Host "Processing: $($file.Name)"

		# Test
		Write-Host "Size: $($file.Length) bytes"
		Write-Host "Modified: $($file.LastWriteTime)"

		#echo "Start-Process ffmpeg '-i $($file.FullName) -c:a pcm_s16le $Dest$($file.Name)' -Wait -NoNewWindow"
		##Write-Host "running rip_cmd"
		Start-Process wmplayer /Task:RipCD
	}
}
catch {
	Write-Error "Failed to rip files from disc..."
	exit 1
}