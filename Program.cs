

await foreach (var url in ChessDotComHttpClient.GetArchivePeriods("Emestrio"))
{
    await foreach (var game in ChessDotComHttpClient.GetAllNormalGames(url))
    {
        Console.WriteLine(game);
        break;
    }
    break;
}