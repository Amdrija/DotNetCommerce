#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Logeecom.DemoProjekat.API/Logeecom.DemoProjekat.PL.csproj", "Logeecom.DemoProjekat.API/"]
COPY ["Logeecom.DemoProjekat.BL/Logeecom.DemoProjekat.BL.csproj", "Logeecom.DemoProjekat.BL/"]
COPY ["Logeecom.DemoProjekat.DAL/Logeecom.DemoProjekat.DAL.csproj", "Logeecom.DemoProjekat.DAL/"]
COPY ["Logeecom.DemoProjekat.Exceptions/Logeecom.DemoProjekat.Exceptions.csproj", "Logeecom.DemoProjekat.Exceptions/"]
COPY ["Logeecom.FileManager/Logeecom.FileManager.csproj", "Logeecom.FileManager/"]
RUN dotnet restore "Logeecom.DemoProjekat.API/Logeecom.DemoProjekat.PL.csproj"
COPY . .
WORKDIR "/src/Logeecom.DemoProjekat.API"
RUN dotnet build "Logeecom.DemoProjekat.PL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logeecom.DemoProjekat.PL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logeecom.DemoProjekat.PL.dll"]
