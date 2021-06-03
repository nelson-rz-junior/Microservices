#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BasketAPI.Microservice/BasketAPI.Microservice.csproj", "BasketAPI.Microservice/"]
COPY ["DataAccess.Microservice/DataAccess.Microservice.csproj", "DataAccess.Microservice/"]
RUN dotnet restore "BasketAPI.Microservice/BasketAPI.Microservice.csproj"
COPY . .
WORKDIR "/src/BasketAPI.Microservice"
RUN dotnet build "BasketAPI.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasketAPI.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketAPI.Microservice.dll"]