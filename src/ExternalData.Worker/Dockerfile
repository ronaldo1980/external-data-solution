FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ExternalDataSolution.sln", "./"]
COPY ["src/ExternalData.Api/ExternalData.Api.csproj", "src/ExternalData.Api/"]
COPY ["src/ExternalData.Worker/ExternalData.Worker.csproj", "src/ExternalData.Worker/"]
COPY ["src/ExternalData.Domain/ExternalData.Domain.csproj", "src/ExternalData.Domain/"]
COPY ["src/ExternalData.Infrastructure/ExternalData.Infrastructure.csproj", "src/ExternalData.Infrastructure/"]

RUN dotnet restore "src/ExternalData.Worker/ExternalData.Worker.csproj"

COPY . .
WORKDIR "/src/src/ExternalData.Worker"
RUN dotnet publish "ExternalData.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExternalData.Worker.dll"]