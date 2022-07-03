using ComicsShop.Rest.Repositories;
using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ComicsShop.Rest.Database;

public static class Connect
{
    public static void AddDbConnection(this IServiceCollection service)
    {
        service.AddScoped(_ =>
        {
            var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
            var compiler = new MySqlCompiler();
            var db = new QueryFactory(connection, compiler);

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                db.Logger = compiled => { Console.WriteLine(compiled.ToString()); };
            }


            return db;
        });
    }

    public static void AddComicsRepository(this IServiceCollection service)
    {
        service.AddScoped<IComicsRepository, ComicsRepository>();
    }
}