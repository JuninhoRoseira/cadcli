# cadcli

CRUD cadastro de clientes Asp.Net MVC - jQuery

Para criar o banco, executar os passos abaixo.

```bash
sudo docker pull mcr.microsoft.com/mssql/server:2022-latest

sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Pontepret@01" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
```

Em seguida executar o script dentro da pasta SQL-Scripts
