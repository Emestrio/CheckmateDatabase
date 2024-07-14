

await foreach (var url in ChessDotComHttpClient.GetArchivePeriods("Emestrio"))
{
    Console.WriteLine(url);
}