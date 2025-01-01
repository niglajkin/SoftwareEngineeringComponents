using System.Collections;
using SecondLab;

Console.WriteLine("App is running");

var numbers = new[] { 1, 2, 3, 4, 5};
numbers.AsyncMap(DoubleAsync).ContinueWith(PrintResult); // 2 4 6 8 10

var willThrowErrors = new[] { -1, -1, 2 };
willThrowErrors.AsyncMap(DoubleAsync).ContinueWith(PrintResult);
// 1)Test exception was thrown
// 2)Test exception was thrown

Console.ReadKey();

return;

Task<int> DoubleAsync(int n) => Task.Delay(Random.Shared.Next(2000, 5000)).ContinueWith(_ => {
    if (n == -1) throw new Exception("Test exception was thrown");
    return n * 2;
});

void PrintResult(Task<(int[], List<Exception>)> task) {
    var (newNumbers, exceptions) = task.Result;
    
    var print = (IEnumerable collection, string separator) => {
        foreach (var element in collection) {
            Console.Write($"{element}{separator}");
        }
    };

    if (exceptions.Count != 0) {
        var messages = exceptions.Select((e, i) => $"{i + 1}){e.Message}");
        print(messages, Environment.NewLine);
    }
    else print(newNumbers, " ");
}