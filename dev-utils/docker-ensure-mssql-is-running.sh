#!/bin/bash

green=$(tput setaf 2)
yellow=$(tput setaf 3)
reset=$(tput sgr0)

mssqlImage="mcr.microsoft.com/mssql/server:2022-latest"
containerName="local-dev-mssql"
containerHostName="$containerName"

dockerImageName=$(docker images -q $mssqlImage)

# Pull the image if it doesn't exist
if [ -z "$dockerImageName" ]; then
    echo "${yellow}Image $dockerImageName not found!!${reset}"
    echo "${green}Pulling image $mssqlImage ....${reset}"
    docker pull $mssqlImage
fi

echo "Searching container $containerName..."

# Check if container exists
dockerContainer=$(docker ps -a -q -f name=$containerName)

if [ -n "$dockerContainer" ]; then
    dockerContainerExited=$(docker ps -a -q -f status=exited -f name=$containerName)
    if [ -n "$dockerContainerExited" ]; then
        echo "${yellow}Container $containerName is exited!!${reset}"
        echo "${green}Starting container $containerName ....${reset}"
        docker start $containerName
    else
        echo "${green}Container $containerName is running.${reset}"
        exit 0
    fi
else
    echo "${yellow}Container $containerName not found!!${reset}"
    echo "${green}Creating container $containerName....${reset}"
    docker create \
    -e "ACCEPT_EULA=Y" \
    -e "MSSQL_SA_PASSWORD=zvNigVNXaRRoy@cAGTwGsV6" \
    -p 1433:1433 \
    --name $containerName \
    --hostname $containerHostName \
    $mssqlImage

    echo "${green}Starting container $containerName....${reset}"
    docker start $containerName
fi
