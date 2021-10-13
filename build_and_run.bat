@echo off
where mongo
IF %ERRORLEVEL% NEQ 0 (
    echo "mongo command is not available, please make sure mongodb is installed and added to the system path."
    pause
    exit /b 1
)

mongo --eval "db.stats()"
IF %ERRORLEVEL% NEQ 0 (
    echo "mongodb not running"
    exit /b 1
) else (
    echo "mongodb is online..."
)

echo building MineCase...
pushd src
dotnet restore
dotnet build -c debug
popd

echo start MineCase.Server...
pushd src\MineCase.Server
start dotnet run
popd

echo start MineCase.Gateway...
pushd src\MineCase.Gateway
start dotnet run
popd
