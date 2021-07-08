FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EventStream.Server/EventStream.Server.csproj", "EventStream.Server/"]
RUN dotnet restore "EventStream.Server/EventStream.Server.csproj"
COPY . .
WORKDIR "/src/EventStream.Server"
RUN dotnet build "EventStream.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventStream.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventStream.Server.dll"]
