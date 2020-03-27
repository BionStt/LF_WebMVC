# Get the base image from microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

#Copy the CSPROJ File and restore any dependenencies (via NUGET)
COPY *.csproj ./
RUN dotnet restore

# Copy the project files and build our release
COPY . ./
RUN dotnet publish -c Release -o out

#Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebMVC.dll"]