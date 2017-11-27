FROM microsoft/dotnet:2.0-runtime
ARG source
WORKDIR /app
EXPOSE 30000
COPY ${source:-obj/Docker/publish} .
COPY ${source:-obj/Docker/publish}/OrleansConfiguration.docker.xml OrleansConfiguration.dev.xml
COPY ${source:-obj/Docker/publish}/config.docker.json config.json
ENTRYPOINT ["dotnet", "MineCase.Server.dll"]
