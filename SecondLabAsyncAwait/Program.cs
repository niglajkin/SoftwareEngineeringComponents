using SecondLabAsyncAwait;

Console.WriteLine("App is running");

var numbers = new[] { -1, -1, 3, 4, 5};

try {
    var newNumbers = await numbers.AsyncMap(DoubleAsync);
    Console.WriteLine(string.Join(", ", newNumbers));
}
catch (Exception e) {
    Console.WriteLine("One or more ex was thrown. The first was:");
    Console.WriteLine(e);
}

Console.ReadKey();

return;

Task<int> DoubleAsync(int n) => Task.Delay(Random.Shared.Next(2000, 3000)).ContinueWith(_ => {
    if (n == -1) throw new ArgumentException("Test exception was thrown");
    return n * 2;
});

