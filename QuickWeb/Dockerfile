FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["QuickWeb/QuickWeb.csproj", "QuickWeb/"]
COPY ["Quick.Service/Quick.Service.csproj", "Quick.Service/"]
COPY ["Quick.IService/Quick.IService.csproj", "Quick.IService/"]
COPY ["Quick.Models/Quick.Models.csproj", "Quick.Models/"]
COPY ["Quick.Common/Quick.Common.csproj", "Quick.Common/"]
RUN dotnet restore "QuickWeb/QuickWeb.csproj"
COPY . .
WORKDIR "/src/QuickWeb"
RUN dotnet build "QuickWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QuickWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QuickWeb.dll"]