# 1. docker build -f WheyMenII.Dockerfile -t jhbui1/wheymen:3.0 ../../WheyMenII
#docker run --rm -it -p 8000:80 -e "ConnectionStrings__Default=Server=tcp:2002-training-bui.database.windows.net,1433;Initial Catalog=WheyMen;Persist Security Info=False;User ID=jhbui1;Password=Xaeonwolf1&;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"  wheymen:3.0
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY . ./

RUN dotnet publish WheyMenII -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY --from=build /app/publish ./

CMD [ "dotnet", "WheyMenII.UI.dll" ]