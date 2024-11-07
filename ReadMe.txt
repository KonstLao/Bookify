docker pull mcr.microsoft.com/mssql/server

docker run --name bookify-db -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1234512345Aa$" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest