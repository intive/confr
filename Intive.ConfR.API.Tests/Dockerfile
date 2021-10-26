FROM microsoft/dotnet:2.2-sdk
WORKDIR /

# Restore dotnet before build to allow for caching
COPY Intive.ConfR.Common/Intive.ConfR.Common.csproj /src/Intive.ConfR.Common/
COPY Intive.ConfR.API/Intive.ConfR.API.csproj /src/Intive.ConfR.API/
COPY Intive.ConfR.Application/Intive.ConfR.Application.csproj /src/Intive.ConfR.Application/
COPY Intive.ConfR.Infrastructure/Intive.ConfR.Infrastructure.csproj /src/Intive.ConfR.Infrastructure/
COPY Intive.ConfR.API.Tests/Intive.ConfR.API.Tests.csproj /src/Intive.ConfR.API.Tests/

RUN dotnet restore /src/Intive.ConfR.API.Tests/Intive.ConfR.API.Tests.csproj

# Copy source files and test
COPY . /src
WORKDIR /src/Intive.ConfR.API.Tests

RUN dotnet test