# syntax=docker/dockerfile:1
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src
COPY ["RedditClone.API/RedditClone.API.csproj", "RedditClone.API/"]
COPY ["RedditClone.Domain/RedditClone.Domain.csproj", "RedditClone.Domain/"]
COPY ["RedditClone.Infrastructure/RedditClone.Infrastructure.csproj", "RedditClone.Infrastructure/"]
COPY ["RedditClone.Application/RedditClone.Application.csproj", "RedditClone.Application/"]
COPY ["RedditClone.Tests/RedditClone.Tests.csproj", "RedditClone.Tests/"]

RUN dotnet restore "RedditClone.API/RedditClone.API.csproj"
RUN dotnet restore "RedditClone.Infrastructure/RedditClone.Infrastructure.csproj"

COPY . .

RUN dotnet tool install --global dotnet-ef

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet ef migrations add "InitialCreate" -p /src/RedditClone.Infrastructure -s /src/RedditClone.API

WORKDIR /src/RedditClone.API

ARG TARGETARCH

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080
EXPOSE 443

COPY --from=build /app .

ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser
USER appuser

ENTRYPOINT ["dotnet", "RedditClone.API.dll"]
