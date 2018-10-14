FROM microsoft/dotnet:2.1-sdk-alpine AS build
COPY . /src
WORKDIR /src
RUN dotnet publish Alpha.Bootstrap.WebApi/Alpha.Bootstrap.WebApi.csproj -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS final
WORKDIR /app
COPY --from=build /app /app
ENTRYPOINT [ "dotnet", "Alpha.Bootstrap.WebApi.dll" ]
EXPOSE 80/TCP
EXPOSE 443/TCP