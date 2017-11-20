@echo off

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
