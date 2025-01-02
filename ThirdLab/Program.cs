using ThirdLab;

Console.WriteLine("App is running");

var numbers = new[] { 1, 2, 3, 4, 5};

try {
    var cancellationSource = new CancellationTokenSource();

    _ = Task.Run(async () => {
        await Task.Delay(4000);
        cancellationSource.Cancel();
    });
    
    var newNumbers = await numbers.AsyncMap(DoubleAsync, cancellationSource.Token);
    Console.WriteLine(string.Join(", ", newNumbers));
}
catch (OperationCanceledException e) {
    Console.WriteLine(e.Message);
}

Console.ReadKey();

return;

async Task<int> DoubleAsync(int n, CancellationToken token) {
    token.ThrowIfCancellationRequested();
    if (n == -1) throw new ArgumentException("Test exception was thrown");

    const int delay = 5000;
    await Task.Delay(delay, token);
    
    return n * 2;
}