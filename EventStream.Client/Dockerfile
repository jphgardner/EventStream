FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EventStream.Client/EventStream.Client.csproj", "EventStream.Client/"]
RUN dotnet restore "EventStream.Client/EventStream.Client.csproj"
COPY . .
WORKDIR "/src/EventStream.Client"
RUN dotnet build "EventStream.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventStream.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventStream.Client.dll"]
