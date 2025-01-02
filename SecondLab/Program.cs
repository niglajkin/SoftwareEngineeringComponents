using SecondLab;

Console.WriteLine("App is running");

var numbers = new[] { 1, 2, 3, 4, 5};
numbers.AsyncMap(DoubleAsync).ContinueWith(PrintResult);

Console.ReadKey();

//Results:
//{ 1, 2, 3, 4, 5} => 2, 4, 6, 8, 10
//{-1, 2, 3, -1, 5} => 
// Test exception was thrown
// Test exception was thrown
return;

Task<int> DoubleAsync(int n) => Task.Delay(Random.Shared.Next(2000, 5000)).ContinueWith(_ => {
    if (n == -1) throw new Exception("Test exception was thrown");
    return n * 2;
});

void PrintResult(Task<int[]> task) {
    if (task.IsFaulted) {
        task.Exception.Handle(e => {
            Console.WriteLine(e.Message);
            return true;
        });
    }

    else {
        var newNumbers = task.Result;
        var stringNewNumbers = string.Join(", ",newNumbers);
        Console.WriteLine(stringNewNumbers);
    }
}