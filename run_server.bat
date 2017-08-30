@echo off

echo building MineCase...
pushd src
dotnet restore
dotnet build -c debug
popd

echo start MineCase.Server...
pushd src\MineCase.Server
start dotnet run
echo run "run_gateway.bat" when server is ready.
popd
