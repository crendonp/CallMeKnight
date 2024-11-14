// See https://aka.ms/new-console-template for more information

Console.WriteLine("Starting Call Me Night!");
HowManyHops:
Console.WriteLine("How many hops?");
if(int.TryParse(Console.ReadLine(), out var hops))
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    var dialNumbers = GetDistinctDialNumbers(hops);
    watch.Stop();
    Console.WriteLine($"With {hops} hops we get {dialNumbers.Count()} dial numbers. Calculation time {watch.ElapsedMilliseconds} ms");
    Console.WriteLine("Do you want to print all posible dial numbers? \"Y\" for Yes");
    var response = Console.ReadLine();
    if (!string.IsNullOrEmpty(response) && response?.ToUpper() == "Y") {
        foreach (var dialNumber in dialNumbers)
        {
            Console.WriteLine(dialNumber);
        }
    }
    Console.WriteLine("Do you want to do another calculation? \"Y\" for Yes");
    response = Console.ReadLine();
    if (!string.IsNullOrEmpty(response) && response?.ToUpper() == "Y")
        goto HowManyHops;
}
else
{
    Console.WriteLine("Please enter a valid number");
    goto HowManyHops;
}

static List<string> GetDistinctDialNumbers(int hops, List<int>? startPositions = null, string? path = null)
{
    if (startPositions is null)
    {
        startPositions = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    Dictionary<int, List<int>> startPositionsAndHopOptions = new(){
        { 0, new List<int>(){ 4,6 } },
        { 1, new List<int>(){ 6,8 } },
        { 2, new List<int>(){ 7,9 } },
        { 3, new List<int>(){ 4,8 } },
        { 4, new List<int>(){ 0,3,9 } },
        { 5, new List<int>() },
        { 6, new List<int>(){ 0,1,7 } },
        { 7, new List<int>(){ 2,6 } },
        { 8, new List<int>(){ 1,3 } },
        { 9, new List<int>(){ 2,4 } },
    };

    if (hops <= 0) return new List<string>();

    List<string> dialNumbers = new List<string>();
    
    foreach(int i in startPositions)
    {
        string currentPath = GetCurrentPath(path, i);
        if (hops > 1)
            dialNumbers.AddRange(GetDistinctDialNumbers(hops - 1, startPositionsAndHopOptions[i], currentPath));
        else
            dialNumbers.Add(currentPath);
    }
    return dialNumbers;

    static string GetCurrentPath(string? path, int i)
    {
        return string.IsNullOrEmpty(path) ? $"{i}" : $"{path}-{i}";
    }
}