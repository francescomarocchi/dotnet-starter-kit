FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dev
WORKDIR /src
COPY ["DigitStarterKit.sln", "./"]
COPY ["DigitStarterKit/DigitStarterKit.csproj", "DigitStarterKit/"]
COPY ["BuildingBlocks.Modularity/BuildingBlocks.Modularity.csproj", "BuildingBlocks.Modularity/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Core.Tests/Core.Tests.csproj", "Core.Tests/"]
COPY ["Modules.Authentication/Modules.Authentication.csproj", "Modules.Authentication/"]
COPY ["Modules.Authentication.Application/Modules.Authentication.Application.csproj", "Modules.Authentication.Application/"]
COPY ["Modules.Authentication.Infrastructure/Modules.Authentication.Infrastructure.csproj", "Modules.Authentication.Infrastructure/"]
COPY ["Modules.Authentication.Tests/Modules.Authentication.Tests.csproj", "Modules.Authentication.Tests/"]
COPY ["Modules.Products/Modules.Products.csproj", "Modules.Products/"]
COPY ["Modules.Products.Application/Modules.Products.Application.csproj", "Modules.Products.Application/"]
COPY ["Modules.Products.Infrastructure/Modules.Products.Infrastructure.csproj", "Modules.Products.Infrastructure/"]
COPY ["Modules.Products.Tests/Modules.Products.Tests.csproj", "Modules.Products.Tests/"]
RUN dotnet restore "DigitStarterKit.sln"
COPY . .
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Development \
	ASPNETCORE_URLS=http://+:8080 \
	DOTNET_USE_POLLING_FILE_WATCHER=1
CMD ["dotnet", "watch", "run", "--project", "DigitStarterKit/DigitStarterKit.csproj", "--urls", "http://0.0.0.0:8080"]

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DigitStarterKit.sln", "./"]
COPY ["DigitStarterKit/DigitStarterKit.csproj", "DigitStarterKit/"]
COPY ["BuildingBlocks.Modularity/BuildingBlocks.Modularity.csproj", "BuildingBlocks.Modularity/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Core.Tests/Core.Tests.csproj", "Core.Tests/"]
COPY ["Modules.Authentication/Modules.Authentication.csproj", "Modules.Authentication/"]
COPY ["Modules.Authentication.Application/Modules.Authentication.Application.csproj", "Modules.Authentication.Application/"]
COPY ["Modules.Authentication.Infrastructure/Modules.Authentication.Infrastructure.csproj", "Modules.Authentication.Infrastructure/"]
COPY ["Modules.Authentication.Tests/Modules.Authentication.Tests.csproj", "Modules.Authentication.Tests/"]
COPY ["Modules.Products/Modules.Products.csproj", "Modules.Products/"]
COPY ["Modules.Products.Application/Modules.Products.Application.csproj", "Modules.Products.Application/"]
COPY ["Modules.Products.Infrastructure/Modules.Products.Infrastructure.csproj", "Modules.Products.Infrastructure/"]
COPY ["Modules.Products.Tests/Modules.Products.Tests.csproj", "Modules.Products.Tests/"]
RUN dotnet restore "DigitStarterKit/DigitStarterKit.csproj"
COPY . .
WORKDIR "/src/DigitStarterKit"
RUN dotnet publish "./DigitStarterKit.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DigitStarterKit.dll"]
