#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["modules/Conch.Order/Conch.Order.csproj", "modules/Conch.Order/"]
RUN dotnet restore "modules/Conch.Order/Conch.Order.csproj"
COPY . .
WORKDIR "/src/modules/Conch.Order"
RUN dotnet build "Conch.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Conch.Order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Conch.Order.dll"]
