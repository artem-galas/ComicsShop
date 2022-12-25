using ComicsShop.Rest.Repositories;
using ComicsShop.Rest.Extensions;
using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using Dapper;

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

            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            return db;
        });
    }

    public static void AddComicsRepository(this IServiceCollection service)
    {
        service.AddScoped<IComicsRepository, ComicsRepository>();
    }
}