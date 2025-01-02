namespace ThirdLab;

public static class ArrayExtension {
    public static async Task<TOutput[]> AsyncMap<TInput, TOutput>(
        this TInput[] inputArray,
        Func<TInput, CancellationToken, Task<TOutput>> getAsyncTask,
        CancellationToken token = default
    ) {
        
        var tasks = inputArray.Select(element => getAsyncTask(element, token)).ToArray();
        var outputArray = await Task.WhenAll(tasks);

        return outputArray;
    }
    
}