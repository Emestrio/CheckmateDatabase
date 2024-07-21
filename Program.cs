using System.Data.Entity;
using Microsoft.VisualBasic;

var database = new DatabaseClient();

while (true)
{
    string? playerToTest = "Emestrio";//database.GetNextPlayerToTest();

    if (playerToTest is null)
    {
        break;
    }

    await foreach (var url in ChessDotComHttpClient.GetArchivePeriods(playerToTest))
    {
        Console.WriteLine(url);
        await foreach (var game in ChessDotComHttpClient.GetAllNormalGames(url))
        {
            if (game.White.Result == Result.Checkmated || game.Black.Result == Result.Checkmated)
            {
                database.Add(game);
            }

            Console.WriteLine($"{game.White.Username} vs {game.Black.Username}: {(game.White.Result == Result.Win ? game.White.Username : game.Black.Username)} win");
        }
    }
}

