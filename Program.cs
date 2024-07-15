await foreach (var url in ChessDotComHttpClient.GetArchivePeriods("Emestrio"))
{
    Console.WriteLine(url);
    await foreach (var game in ChessDotComHttpClient.GetAllNormalGames(url))
    {
        Console.WriteLine($"{game.White.Username} vs {game.Black.Username}: {(game.White.Result == Result.Win ? game.White.Username : game.Black.Username)} win");
    }
}