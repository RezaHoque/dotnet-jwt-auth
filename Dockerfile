FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["./JwtAuthApi/JwtAuthApi.csproj", "."]
RUN dotnet restore "JwtAuthApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./JwtAuthApi/JwtAuthApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./JwtAuthApi/JwtAuthApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","JwtAuthApi.dll"]