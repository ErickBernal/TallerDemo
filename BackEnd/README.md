# Fuente de consulta C#.
- Creacion de Api [Informatica DP](https://www.youtube.com/watch?v=Gua0O0Q7I58)

- Uso de ORM en .NET Core y C#
[How YOU can use an ORM in .NET Core and C# to type less SQL -starring Entity Framework](https://softchris.github.io/pages/dotnet-orm.html#create-the-database)


## Glosario
- **DbSet** : repsenta una tabla en la base de datos.


Comando utilizado para conectar la **Capa de datos** con la base de datos **DBEB**
```bash
dotnet ef dbcontext scaffold "Data Source=localhost;Database=DBEB;User ID=sa;Password=yourStrong#Password;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer --context-dir .\Data --output-dir .\Data\Models
```

---
# Comandos 
```bash
# crear un proyecto webapi llamado TallerApi
dotnet new webapi -o TallerApi2
```

# Compilando models

- Abrir **Consola de administracion de paquetes**
```bash
# En consola ejecutar 
# la migracion creara dos clases, se utilizaran para actualizar la base de datos con las modificaciones realizadas 
add-migration initial -o Data/migrations
EntityFrameworkCore\add-migration initiaD -o Data/migrations
# initial: nombre de la migracion
# DataAccess/migrations: destino de los archivos generados

# Para actualizar la base de datos ejecutar:
update-database
EntityFrameworkCore\Update-Database
```


