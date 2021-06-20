# API_Emprestimos

Subir a aplicacao/banco de dados
docker-compose up -d --build

para debug, subir uma instancia do sqlserver com o comando abaixo
docker run -d -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Developer" -p 1433:1433 mcr.microsoft.com/mssql/server

ou usar a instancia da docker compose
