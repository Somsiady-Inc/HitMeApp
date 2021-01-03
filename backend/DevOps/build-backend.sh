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
ORIGINAL_PATH=$(pwd)

[ -z "$1" ] && TAG=latest || TAG="$1"

cd ../
docker image build -f DevOps/Dockerfile -t hitmeapp-backend:"$TAG" .
cd "$ORIGINAL_PATH"
