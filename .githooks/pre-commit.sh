#!/bin/sh
LC_ALL=C


# 1. Formatting .NET staged files

ROOT_DIR=$(pwd)

# cd "$ROOT_DIR/backend/HitMeApp"

# Restore dotnet tools
dotnet tool restore

# Select files to format
FILES=$(git diff --cached --name-only --diff-filter=ACM *.cs | sed 's| |\\ |g')
[ -z "$FILES" ] && exit 0

# Format input for dotnet-formatter
echo "$FILES" | cat | xargs | sed -e 's/ / /g' | xargs dotnet-format --workspace "$ROOT_DIR/backend/HitMeApp" --include

# Add back the modified files to staging
echo "$FILES" | xargs git add

exit 0