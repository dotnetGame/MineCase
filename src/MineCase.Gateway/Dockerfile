FROM microsoft/dotnet:2.0-runtime
ARG source
WORKDIR /app
EXPOSE 25565
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "MineCase.Gateway.dll"]
