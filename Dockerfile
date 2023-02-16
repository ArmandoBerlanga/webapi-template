FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app
COPY . .

WORKDIR /app/webapi

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/webapi/out ./
RUN find . -name "*.pdb" -type f -delete

ENTRYPOINT ["dotnet", "webapi.dll"]

# docker build -t pplus-webapi .
# docker run -d -p 5001:80 --name pp-webapi pplus-webapi
