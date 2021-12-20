#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["modules/Conch.Member/Conch.Member.csproj", "modules/Conch.Member/"]
RUN dotnet restore "modules/Conch.Member/Conch.Member.csproj"
COPY . .
WORKDIR "/src/modules/Conch.Member"
RUN dotnet build "Conch.Member.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Conch.Member.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Conch.Member.dll"]
