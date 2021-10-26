FROM microsoft/dotnet:2.2-aspnetcore-runtime AS Base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build

# Restore dotnet before build to allow for caching
WORKDIR /
COPY Intive.ConfR.Application/Intive.ConfR.Application.csproj /src/Intive.ConfR.Application/
COPY Intive.ConfR.Common/Intive.ConfR.Common.csproj /src/Intive.ConfR.Common/
COPY Intive.ConfR.Domain/Intive.ConfR.Domain.csproj /src/Intive.ConfR.Domain/
COPY Intive.ConfR.Infrastructure/Intive.ConfR.Infrastructure.csproj /src/Intive.ConfR.Infrastructure/
COPY Intive.ConfR.Persistence/Intive.ConfR.Persistence.csproj /src/Intive.ConfR.Persistence/
COPY Intive.ConfR.API/Intive.ConfR.API.csproj /src/Intive.ConfR.API/

RUN dotnet restore /src/Intive.ConfR.API/Intive.ConfR.API.csproj

# Copy source files and build
COPY . /src

RUN dotnet build /src/Intive.ConfR.API/Intive.ConfR.API.csproj --no-restore -c Release
RUN dotnet publish /src/Intive.ConfR.API/Intive.ConfR.API.csproj --no-restore -c Release -o /app

# Copy compiled app to runtime container
FROM base AS final
COPY --from=build /app .
CMD ["dotnet", "Intive.ConfR.API.dll"]
