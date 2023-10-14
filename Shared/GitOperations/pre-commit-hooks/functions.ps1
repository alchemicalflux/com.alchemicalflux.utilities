#-------------------------------------------------------------------------------
#  File:           functions.ps1 
#  Project:        AlchemicalFlux Utilities
#  Description:    Functions that help with pre-commit header processing.
#  Copyright:      ©2023 AlchemicalFlux. All rights reserved.
#
#  Last commit by: alchemicalflux 
#  Last commit at: 2023-10-13 19:00:10 
#-------------------------------------------------------------------------------

function Get-BashFiles {
    $gitStaged = git diff --cached --name-only --diff-filter=AM
    $stagedFiles = $gitStaged -split "`n"

    # Create an empty array to hold our final list of files
    $finalFiles = @()

    foreach ($file in $stagedFiles) {
        $fullPath = Join-Path -Path (Get-Location) -ChildPath $file
        # First we check if the file has an extension. If it doesn't, we assume it might be a bash file
        if ([System.IO.Path]::GetExtension($fullPath) -eq '') {
            # Read the first line of the file
            $firstLine = Get-Content -Path $fullPath -First 1
            # Check if it starts with "#!/bin/sh"
            if ($firstLine -match "^#!/bin/sh") {
                $finalFiles += $file
            }
        } elseif ($file -like "*.sh") {
            # If it does have an extension, we check if it is .sh
            $finalFiles += $file
        }
    }
    return $finalFiles
}

function Get-Files ($extention) {
    $gitStaged = git diff --cached --name-only --diff-filter=AMR
    $stagedFiles = $gitStaged -split "`n"

    # Create an empty array to hold our final list of files
    $finalFiles = @()

    foreach ($file in $stagedFiles) {
        if ($file -like "*.$extention") {
            # If it does have an extension, we check if it is .cs
            $finalFiles += $file
        }
    }
	
    return $finalFiles
}
