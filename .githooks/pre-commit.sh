#!/bin/sh
LC_ALL=C


# 1. Formatting .NET staged files

# Select files to format and make the path relative to .NET solution
FILES=$(git diff --cached --name-only --diff-filter=ACM *.cs | sed 's| |\\ |g' | xargs realpath --relative-to="$(pwd)/backend/HitMeApp")
[ -z "$FILES" ] && exit 0

# Go to the backend solution root
cd "$(pwd)/backend/HitMeApp"

# Restore dotnet tools
dotnet tool restore

# Format input for dotnet formatter
echo "$FILES" | cat | xargs | sed -e 's/ / /g' | xargs dotnet format --include

# Add the modified files back to staging
echo "$FILES" | xargs git add

exit 0