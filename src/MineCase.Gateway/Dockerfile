FROM microsoft/dotnet:2.0-runtime
ARG source
WORKDIR /app
EXPOSE 25565
COPY ${source:-obj/Docker/publish} .
COPY ${source:-obj/Docker/publish}/OrleansConfiguration.docker.xml OrleansConfiguration.dev.xml
ENTRYPOINT ["dotnet", "MineCase.Gateway.dll"]
