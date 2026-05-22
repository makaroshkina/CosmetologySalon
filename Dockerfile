FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CosmetologySalon.csproj", "./"]
RUN dotnet restore "CosmetologySalon.csproj"
COPY . .

RUN dotnet publish "CosmetologySalon.csproj" -c Release -o /app/publish \
    -p:CopyRazorGenerateFilesToPublishDirectory=true

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CosmetologySalon.dll"]