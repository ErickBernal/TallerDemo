# Descripcion  de implementacion de SqlServer en Docker

- [docker hub, imagen de SQL server](https://hub.docker.com/_/microsoft-mssql-server)

## Comandos implementados (uso de consola)

### Correr imagen
Descargar imagen

```bash
    docker pull mcr.microsoft.com/mssql/server
```

Ejecucion de imagen

```bash
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong#Password" --name SQLserver -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```


## Ejecucion desde contenedor
**Entrar al contenedor**

```bash
    # Linux
        docker exec -it SQLserver bash
    # Windows
        winpty docker exec -it SQLserver bash
```

- **Ingresar, ejecutar comandos desde SQLserver**
  
```bash
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P yourStrong#Password
```
- **Comprobar funcionamiento de Sql Server**
```Sql
    1> select 1 + 1
    2> go

    -----------
            2

    (1 rows affected)
    1>
```
## Utilizando docker compose
Crear ***[docker-compose.yaml](./docker-compose.yml)***:

Ejecutar:
```bash
    docker-comnpose up -d
```


## Credenciales para conexion desde DBeaver
- Host: ***localhost***
- database / Schema: ***master***
- Nombre de usuario:   ***sa***
- Contraseña: ***yourStrong#Password***

- ![img](../miscelaneos/img/dbeaver.PNG)


