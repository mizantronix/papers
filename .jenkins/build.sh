#!/bin/bash
set -e

dockerRepo=192.168.68.104:1111
dockerImg=dotnet-build
dockerTag=0.6
dockerContainerNameTemplate=dotnet-build

docker pull "$dockerRepo/$dockerImg:$dockerTag"
path=$WORKSPACE/papers-server

docker run \
	-v $path:/home/agent/work \
    -e BUILD_NUMBER=$BUILD_NUMBER \
    -e MAJOR_VERSION=0 \
    -e MINOR_VERSION=2 \
    --name $dockerContainerNameTemplate-$BUILD_NUMBER \
    "$dockerRepo/$dockerImg:$dockerTag"

docker rm $dockerContainerNameTemplate-$BUILD_NUMBER
docker rmi "$dockerRepo/$dockerImg:$dockerTag"