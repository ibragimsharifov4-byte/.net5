using Npgsql;

namespace Application.Data;

public static class Database
{
    private static string connection = "Host=localhost;Port=5432;Database=NewBase;Username=postgres;Password=1234";
    public static NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(connection);
    }
}
