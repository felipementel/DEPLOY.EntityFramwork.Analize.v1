# EntityFramework Commands

```

dotnet tool install --global dotnet-ef

```

```
$env:MigrationName  = "InitDatabaseCommit";
```

```

dotnet ef migrations add $env:MigrationName --startup-project ./src/DEPLOY.EntityFramework.v1 --project ./src/DEPLOY.EntityFramework.v1 --context DEPLOY.EntityFramework.v1.Infra.Database.DeployDbContext --output-dir Migrations/EF -v

```

```


dotnet ef database update $env:MigrationName -s ./src/DEPLOY.EntityFramework.v1 -p ./src/DEPLOY.EntityFramework.v1 -c DEPLOY.EntityFramework.v1.Infra.Database.DeployDbContext --verbose

```

```
dotnet ef migrations script --project .\src\DEPLOY.EntityFramework.v1\DEPLOY.EntityFramework.v1.csproj -o .\src\DEPLOY.EntityFramework.v1\Migrations\SQL\$env:MigrationName.sql
```

Connection String

```

Server=tcp:sql-canal-deploy.database.windows.net,1433;Initial Catalog=daploy-ef-analizer;Persist Security Info=False;User ID=felipementel;Password=Abcd1234%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```
