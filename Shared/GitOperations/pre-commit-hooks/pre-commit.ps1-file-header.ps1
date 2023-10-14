#-------------------------------------------------------------------------------
#  File:           pre-commit.ps1-file-header.ps1 
#  Project:        AlchemicalFlux Utilities
#  Description:    Git hook for pre-commit processing of .ps1 file headers.
#  Copyright:      ©2023 AlchemicalFlux. All rights reserved.
#
#  Last commit by: alchemicalflux 
#  Last commit at: 2023-10-13 19:00:10 
#-------------------------------------------------------------------------------

# Requires -Version 3.0
$ErrorActionPreference = "Stop"

# Add function modules as necessary
$scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
$functionsPath = Join-Path -Path $scriptPath -ChildPath 'functions.ps1'
. $functionsPath


# Constants
$headerStart = "#-------------------------------------------------------------------------------"
$headerEnd =   "#-------------------------------------------------------------------------------"
$currentYear = Get-Date -Format "yyyy"
$user = & git config user.name
$date = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

$filePrefix =         "File:           "
$projectPrefix =      "Project:        "
$descriptionPrefix =  "Description:    "
$copyrightPrefix =    "Copyright:      "
$userPrefix =         "Last commit by: "
$datePrefix =         "Last commit at: "

$projectPostfix =     "YourProjectName  # You should replace this with your project name"
$descriptionPostfix = "YourDescription  # You should replace this with your description"
$copyrightPostfix =   "YourName/YourCompany. All rights reserved.  # You should replace this with your copyright details"


# Gather all files to be updated and adjust as necessary
$stagedFiles = Get-Files "ps1"
foreach ($file in $stagedFiles) {

	# Full path to the file
	$filePath = Join-Path -Path (Get-Location) -ChildPath $file
	$fileName = Split-Path -Path $filePath -Leaf
	$content = Get-Content -Path $filePath -Raw

	# Assign values with any modifications if necessary
	$fileValue = "$fileName "
	$copyrightValue = "©$currentYear "
	$userValue = "$user "
	$dateValue = "$date "

	# Add the new header if it is missing
	if (-not ($content -match "$headerStart")) {
		$fileHeader =        "$filePrefix$fileValue"
		$projectHeader =     "$projectPrefix$projectPostfix"
		$descriptionHeader = "$descriptionPrefix$descriptionPostfix"
		$copyrightHeader =   "$copyrightPrefix$copyrightValue$copyrightPostfix"
		$userHeader =        "$userPrefix$userValue"
		$dateHeader =        "$datePrefix$dateValue"

		$newHeader =
@"
$headerStart
#  $fileHeader
#  $projectHeader
#  $descriptionHeader
#  $copyrightHeader
#
#  $userHeader
#  $dateHeader
$headerEnd

"@

		$content = $newHeader + $content
	}

	# Gather the header section by pattern
	$headerPattern = [regex]::Escape($headerStart) + "(.*?)" + [regex]::Escape($headerEnd)
	$matchResults  = [regex]::Matches($content, $headerPattern, 'Singleline')

	# Check if we have a match
	if($matchResults.Count -eq 0) {
		Write-Error "No header found in file $fileName"
		exit 1
	}

	#Save off the the top header
	$originalHeader = $matchResults[0].Value
	$updatedHeader = $matchResults[0].Value

	# Update the file name to match
	$updatedHeader = $updatedHeader -replace "(?<=$filePrefix).*", $fileValue

	# Update copyright if single year is out of date
	if($updatedHeader -match "©(\d{4}) ") {
		$oldYear = $Matches[1]
		if($oldYear -ne $currentYear) {
			$updatedHeader = $updatedHeader -replace "©$oldYear ", "©$oldYear-$currentYear "
		}
	}

	# Update latest copyright if double-year setup is out of date
	if($updatedHeader -match "©(\d{4})-(\d{4}) ") {
		$oldYear = $Matches[1]
		$newYear = $Matches[2]
		if($newYear -ne $currentYear) {
			$updatedHeader = $updatedHeader -replace "©$oldYear-$newYear ", "©$oldYear-$currentYear "
		}
	}

	# Update the user to match the committor
	$updatedHeader = $updatedHeader -replace "(?<=$userPrefix).*", $userValue

	# Update the commit date and time
	$updatedHeader = $updatedHeader -replace "(?<=$datePrefix).*", $dateValue

	# Replace the original header with the altered data and save
	$finalContent = $content.Replace($originalHeader, $updatedHeader)
	Set-Content -Path $filePath -Value $finalContent -NoNewLine

	# Stage the file for commit
	& git add $filePath
}