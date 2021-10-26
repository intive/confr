FROM microsoft/dotnet:2.2-sdk
WORKDIR /

# Restore dotnet before build to allow for caching
COPY Intive.ConfR.Application/Intive.ConfR.Application.csproj /src/Intive.ConfR.Application/
COPY Intive.ConfR.Application.Tests/Intive.ConfR.Application.Tests.csproj /src/Intive.ConfR.Application.Tests/
COPY Intive.ConfR.Common/Intive.ConfR.Common.csproj /src/Intive.ConfR.Common/
COPY Intive.ConfR.Domain/Intive.ConfR.Domain.csproj /src/Intive.ConfR.Domain/
COPY Intive.ConfR.Persistence/Intive.ConfR.Persistence.csproj /src/Intive.ConfR.Persistence/
COPY Intive.ConfR.Infrastructure/Intive.ConfR.Infrastructure.csproj /src/Intive.ConfR.Infrastructure/

RUN dotnet restore /src/Intive.ConfR.Application.Tests/Intive.ConfR.Application.Tests.csproj

# Copy source files
COPY . /src
WORKDIR /src/Intive.ConfR.Application.Tests

# run tests on docker build
RUN dotnet test
