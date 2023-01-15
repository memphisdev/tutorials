FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy project csproj file and restore it in docker directory
COPY ./*.csproj ./
RUN dotnet restore

# Copy everything into the docker directory and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ProductOffer.WebAPI.dll"]
EXPOSE 4201