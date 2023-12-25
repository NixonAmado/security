ARG VARIANT=8.0.100-bookworm-slim-amd64
FROM mcr.microsoft.com/dotnet/sdk:${VARIANT} AS build
WORKDIR /source
EXPOSE 8080

COPY Api/*.csproj Api/
COPY Application/*.csproj Application/
COPY Domain/*.csproj Domain/
COPY Persistence/*csproj Persistence/
RUN dotnet restore Api/Api.csproj

COPY Api/ Api/
COPY Application/ Application/
COPY Domain/ Domain/
COPY Persistence/ Persistence/

FROM build AS publish
WORKDIR /source/Api
RUN dotnet publish --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0.0-bookworm-slim-amd64
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet"]
CMD ["Api.dll"]
