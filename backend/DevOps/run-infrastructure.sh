#!/bin/sh
LC_ALL=C

# Folder Context Check
CURRENT_FOLDER=$(pwd | xargs basename)

if [[ ! "$CURRENT_FOLDER" = "DevOps" ]]
then
    echo "This script is support to be invoked from the DevOps folder context. Current context: '${CURRENT_FOLDER}'."
    exit 1;
fi

# Actual Script
docker-compose -f infrastructure.yml down
docker-compose -f infrastructure.yml up --build -d
