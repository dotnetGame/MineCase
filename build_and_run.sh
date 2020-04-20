#!/bin/bash
which mongo
if [ $? -ne 0 ]; then
    echo "mongo command is not available, please make sure mongodb is installed and added to the system path."
    return 1
fi

mongo --eval "db.stats()"
if [ $? -ne 0 ]; then
    echo "mongodb not running"
    return 1
else 
    echo "mongodb is online..."
fi

echo building MineCase...
cd src
dotnet restore
dotnet build -c debug
cd -

echo start MineCase.Server...
cd src/MineCase.Server
dotnet run &
cd -

echo start MineCase.Gateway...
cd src/MineCase.Gateway
dotnet run
cd -