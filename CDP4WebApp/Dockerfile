FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY Nuget.Config .
COPY CDP4WebApp CDP4WebApp
RUN dotnet restore --configfile Nuget.Config CDP4WebApp
RUN dotnet build CDP4WebApp -c Release -o /app/build

FROM build AS publish
RUN dotnet publish CDP4WebApp -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY CDP4WebApp/nginx.conf /etc/nginx/nginx.conf