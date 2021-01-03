#!/bin/sh
LC_ALL=C

docker-compose -f DevOps/infrastructure.yml down
docker-compose -f DevOps/infrastructure.yml up --build -d
