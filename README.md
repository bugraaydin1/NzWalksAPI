## Run MSSQL on Docker

```bash
docker pull mcr.microsoft.com/azure-sql-edge

docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=<Password>' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
```

## DB Migration

- Create models
- Create DBContext
- Create migrations and DB:

```bash
dotnet ef migrations add InitialCreate

dotnet ef database update
```
