FROM microsoft/aspnetcore-build:2.2 as build
WORKDIR /app


COPY . . 

#COPY Solution.sln .
# RUN ls -l
RUN dotnet restore ./BidApp.sln

COPY . .

RUN dotnet publish ./BidApp.WebApi/BidApp.WebApi.dll --output /out/ --configuration Release

# runtime image
FROM microsoft/aspnetcore:2.2
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["BidApp.WebApi.dll"]