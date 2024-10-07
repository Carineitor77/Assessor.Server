FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY Assessor.Server.Api/Assessor.Server.Api.csproj ./Assessor.Server.Api/
COPY Assessor.Server.Infrastructure/Assessor.Server.Infrastructure.csproj ./Assessor.Server.Infrastructure/
COPY Assessor.Server.Application/Assessor.Server.Application.csproj ./Assessor.Server.Application/
COPY Assessor.Server.Domain/Assessor.Server.Domain.csproj ./Assessor.Server.Domain/

RUN dotnet restore

COPY . ./

RUN dotnet build "Assessor.Server.Api/Assessor.Server.Api.csproj" -c Release -o /app/build

RUN dotnet publish "Assessor.Server.Api/Assessor.Server.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build-env /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Assessor.Server.Api.dll"]
