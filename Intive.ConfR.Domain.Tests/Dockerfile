FROM microsoft/dotnet:2.2-sdk
WORKDIR /

# Restore dotnet before build to allow for caching
COPY Intive.ConfR.Common/Intive.ConfR.Common.csproj /src/Intive.ConfR.Common/
COPY Intive.ConfR.Domain/Intive.ConfR.Domain.csproj /src/Intive.ConfR.Domain/
COPY Intive.ConfR.Domain.Tests/Intive.ConfR.Domain.Tests.csproj /src/Intive.ConfR.Domain.Tests/

RUN dotnet restore /src/Intive.ConfR.Domain.Tests/Intive.ConfR.Domain.Tests.csproj

# Copy source files and test
COPY . /src
WORKDIR /src/Intive.ConfR.Domain.Tests

RUN dotnet test