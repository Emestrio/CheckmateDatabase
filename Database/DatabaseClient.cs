using System.Data.SQLite;

internal class DatabaseClient : IDisposable
{
    private static readonly SQLiteConnection connection = new("Data Source=C:\\Users\\Erik\\source\\repos\\CheckmateDatabase\\database.db");
    public DatabaseClient()
    {
        connection.Open();

        var sql =
        """
        CREATE TABLE IF NOT EXISTS playersToTest
            (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT UNIQUE ON CONFLICT IGNORE
            )
        """;

        var sql2 =
        """
        CREATE TABLE IF NOT EXISTS players
            (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT UNIQUE ON CONFLICT IGNORE
            )
        """;

        var sql3 =
        """
        CREATE TABLE IF NOT EXISTS checkmates
            (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            fen TEXT UNIQUE ON CONFLICT IGNORE,
            firstTimeUnixTime INTEGER,
            url TEXT UNIQUE ON CONFLICT IGNORE
            occurences INTEGER DEFAULT 0
            )
        """;

        using var cmd = new SQLiteCommand(sql, connection);
        using var cmd2 = new SQLiteCommand(sql2, connection);
        using var cmd3 = new SQLiteCommand(sql3, connection);
        cmd.ExecuteNonQuery();
        cmd2.ExecuteNonQuery();
        cmd3.ExecuteNonQuery();
    }

    public string? GetNextPlayerToTest()
    {
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM playersToTest LIMIT 1";

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return (string)reader["username"];
        };

        return null;
    }

    internal void Add(Game game)
    {
        using var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM checkmates WHERE fen MATCH '{game.Fen}'";
    }

    public void Dispose()
    {
        connection.Close();
    }
}

public readonly record struct PlayerRow
{
    public readonly int Id { get; init; }
    public readonly string Username { get; init; }
}