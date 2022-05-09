#!/bin/bash
set -e

echo "installing..."
echo "AZP_URL is $AZP_URL"
echo "AZP_TOKEN is $AZP_TOKEN"
echo "AZP_AGENT_NAME is $AZP_AGENT_NAME"
echo "AZP_POOL is $AZP_POOL"

if [ -z "$AZP_URL" ]; then
    echo 1>&2 "error: missing AZP_URL environment variable"
    exit 1
fi

if [ -z "$AZP_TOKEN_FILE" ]; then
    if [ -z "$AZP_TOKEN" ]; then
        echo 1>&2 "error: missing AZP_TOKEN environment variable"
        exit 1
    fi
    AZP_TOKEN_FILE=/home/agent/.token
    echo -n $AZP_TOKEN > "$AZP_TOKEN_FILE"
fi

unset AZP_TOKEN

if [ -n "$AZP_WORK" ]; then
    mkdir -p "$AZP_WORK"
fi

#rm -rf /azp/agent
#mkdir /azp/agent

cd /home/agent/
export AGENT_ALLOW_RUNASROOT="1"
cleanup() {
    if [ -e config.sh ]; then
        print_header "Cleanup. Removing Azure Pipelines agent..."
        ./config.sh remove --unattended \
            --auth PAT \
            --token $(cat "$AZP_TOKEN_FILE")
    fi
}

print_header() {
    lightcyan='\033[1;36m'
    nocolor='\033[0m'
    echo -e "${lightcyan}$1${nocolor}"
}

# Let the agent ignore the token env variables
export VSO_AGENT_IGNORE=AZP_TOKEN,AZP_TOKEN_FILE
source ./env.sh

print_header "1. Configuring Azure Pipelines agent..."
./config.sh --unattended \
    --agent "$AZP_AGENT_NAME" \
    --url "$AZP_URL" \
    --auth PAT \
    --token $(cat "$AZP_TOKEN_FILE") \
    --pool "${AZP_POOL:-Default}" \
    --work "${AZP_WORK:-_work}" \
    --replace \
    --acceptTeeEula & wait $!

print_header "2. Running Azure Pipelines agent..."

trap 'cleanup; exit 130' INT
trap 'cleanup; exit 143' TERM

# To be aware of TERM and INT signals call run.sh
# Running it with the --once flag at the end will shut down the agent after the build is executed
./run.sh & wait $!