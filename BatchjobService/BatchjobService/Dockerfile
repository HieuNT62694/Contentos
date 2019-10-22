FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["BatchjobService/BatchjobService.csproj", "BatchjobService/"]

RUN dotnet restore "BatchjobService/BatchjobService.csproj"
COPY . .
WORKDIR "/src/BatchjobService"
RUN dotnet build "BatchjobService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BatchjobService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BatchjobService.dll"]