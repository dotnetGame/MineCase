#!/bin/bash

echo building MineCase...
cd src
dotnet restore
dotnet build -c debug

echo start MineCase.Server...
cd ../src/MineCase.Server
dotnet run

echo start MineCase.Gateway...
cd ../src/MineCase.Gateway
dotnet run
