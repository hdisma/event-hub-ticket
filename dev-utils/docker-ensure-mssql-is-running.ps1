
$mssqlImage = "mcr.microsoft.com/mssql/server:2022-latest"
$containerName = "local-dev-mssql"
$containerHostName = $containerName

$DockerImageName = (docker images -q $mssqlImage) | Out-String

#pull the image if doesn't exists
if (!$DockerImageName) {
    Write-Warning "image $DockerImageName not found!!"
    Write-Host "Pulling image $DockerImageName ...."
    docker pull $mssqlImage
}


Write-Host "searching container $containerName..."
# check if container exists
$DockerContainer = (docker ps -a -q -f name=$containerName) | Out-String
    
if($DockerContainer){

    $DockerContainerExited = (docker ps -a -q -f status=exited -f name=$containerName) | Out-String

    if($DockerContainerExited) {    
        Write-Warning "container $containerName is exited!!"
        Write-Host "starting container $containerName ...."
        docker start $containerName
    } else {
        Write-Host "container $containerName is running."
        exit 0
    }
} else {

    Write-Warning "container $containerName not found!!"
    Write-Host "creating container $containerName...."

    docker create `
    -e "ACCEPT_EULA=Y" `
    -e "MSSQL_SA_PASSWORD=zvNigVNXaRRoy@cAGTwGsV6" `
    -p 1433:1433 `
    --name $containerName `
    --hostname $containerHostName `
    $mssqlImage

    Write-Host "starting container $containerName...."
    docker start $containerName
}
