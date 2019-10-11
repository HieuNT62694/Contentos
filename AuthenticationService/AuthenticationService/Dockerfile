FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["AuthenticationService/AuthenticationService.csproj", "AuthenticationService/"]

RUN dotnet restore "AuthenticationService/AuthenticationService.csproj"
COPY . .
WORKDIR "/src/AuthenticationService"
RUN dotnet build "AuthenticationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenticationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]