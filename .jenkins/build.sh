#!/bin/bash
set -e

dockerRepo=192.168.68.104:1111
dockerImg=dotnet-alpine
dockerTag=0.3

export $dockerRepo
docker pull "$dockerRepo/$dockerImg:$dockerTag"

agentNum=$(cat /proc/sys/kernel/random/uuid | sed 's/[-]//g' | head -c 20)
docker run -v $WORKSPACE:/home/agent/work -n agent-$agentNum "$dockerRepo/$dockerImg:$dockerTag"

