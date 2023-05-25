#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HitecService.Api/HitecService.Api.csproj", "HitecService.Api/"]
COPY ["HitecService.Ioc/HitecService.Ioc.csproj", "HitecService.Ioc/"]
COPY ["HitecService.Core/HitecService.Core.csproj", "HitecService.Core/"]
COPY ["HitecService.Data/HitecService.Data.csproj", "HitecService.Data/"]
COPY ["HitecService.Models/HitecService.Models.csproj", "HitecService.Models/"]
COPY ["HitecService.Utility/HitecService.Utility.csproj", "HitecService.Utility/"]
RUN dotnet restore "HitecService.Api/HitecService.Api.csproj"
COPY . .
WORKDIR "/src/HitecService.Api"
RUN dotnet build "HitecService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HitecService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HitecService.Api.dll"]