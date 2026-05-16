FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dev
WORKDIR /src
COPY ["DigitStarterKit.sln", "./"]
COPY ["DigitStarterKit/DigitStarterKit.csproj", "DigitStarterKit/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
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
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
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
