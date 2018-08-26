FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 56048
EXPOSE 44398

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Petfinder/Petfinder.csproj", "Petfinder/"]
RUN dotnet restore "Petfinder/Petfinder.csproj"
COPY . .
WORKDIR "/src/Petfinder"
RUN dotnet build "Petfinder.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Petfinder.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Petfinder.dll"]