FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
COPY . /src
WORKDIR /src
RUN dotnet publish Alpha.Bootstrap.WebApi/Alpha.Bootstrap.WebApi.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR /app
COPY --from=build /app /app
ENTRYPOINT [ "dotnet", "Alpha.Bootstrap.WebApi.dll" ]
EXPOSE 80/TCP
EXPOSE 443/TCP