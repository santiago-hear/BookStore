# BookStoreAPI
## Características:
   1. Se usó EntityFramewrok Core 6 para el modelamiento y definición de la base de datos, a través del método "Code first".
   2. Por algunos problemas con mi máquina local, no pude realizar una conexión a una base de datos de Oracle, por lo cual se usó SQL server con base de datos, sin embargo, se generó la conexión y el contexto para conectar a Oracle.
   3. Se implementó el patrón repositorio con inyección de dependencias con la arquitectura MVC.
   4. Se realizó el CRUD completo para ambas entidades (Autores y Libros)
   5. La cantidad de libros permitida se puede configurar desde el appsettings.json
   6. Las candenas de conexión a la base de datos se pueden configurar desde el appsettings.json

## Uso
  1. Primero se debe crear la base de datos local para generar la cadena de conexión
  2. Despues se debe ingresar la cadena de conexión al appsettings.json ("OracleConnection" para oracle o "SqlServerConnection" para sqlserver)
  3. Luego se debe ejecutar desde la linea de comandos de PM (Package Management) las siguientes lineas.
 
   - Para Oracle
```bash
add-migration migrationName -Context BookStoreOracleContext
update-database -Context BookStoreOracleContext
```
   - Para SqlServer
```bash
add-migration migrationName -Context BookStoreDSqlServerContext
update-database -Context BookStoreDSqlServerContext
```
   - Y configurar el el servicio desde el archivo program.cs
 ```c#
// Para oracle
var OracleConnection = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<BookStoreOracleContext>(options => options.UseOracle(OracleConnection));
// Para sql server
var SQLServerConnection = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<BookStoreSqlServerContext>( options => options.UseSqlServer(SQLServerConnection));
```

## Pruebas Funcionales
Las pruebas funcionales se muestran en el siguiente:
[video](https://drive.google.com/file/d/1NDigfr_zmGtXhoHHkR6p8CvuN2hcKNeG/view?usp=sharing)
