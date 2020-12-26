#!/bin/sh
LC_ALL=C

# This script should be invoked from DevOps folder context
ORIGINAL_PATH=$(pwd)

[ -z "$1" ] && TAG=latest || TAG="$1"

cd ../
docker image build -f DevOps/Dockerfile -t hitmeapp-backend:"$TAG" .
cd "$ORIGINAL_PATH"