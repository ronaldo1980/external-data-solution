FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ExternalDataSolution.sln", "./"]
COPY ["src/ExternalData.Api/ExternalData.Api.csproj", "src/ExternalData.Api/"]
COPY ["src/ExternalData.Worker/ExternalData.Worker.csproj", "src/ExternalData.Worker/"]
COPY ["src/ExternalData.Domain/ExternalData.Domain.csproj", "src/ExternalData.Domain/"]
COPY ["src/ExternalData.Infrastructure/ExternalData.Infrastructure.csproj", "src/ExternalData.Infrastructure/"]

RUN dotnet restore "src/ExternalData.Api/ExternalData.Api.csproj"

COPY . .
WORKDIR "/src/src/ExternalData.Api"
RUN dotnet publish "ExternalData.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExternalData.Api.dll"]