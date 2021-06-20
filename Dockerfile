FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine
COPY . /app
WORKDIR /app
RUN dotnet publish "API_Emprestimos.csproj" -c Release -o /app/publish
WORKDIR /app/publish
ENTRYPOINT ["dotnet", "API_Emprestimos.dll"]