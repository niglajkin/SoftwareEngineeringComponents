namespace SecondLabAsyncAwait;

public static class ArrayExtension {
    public static async Task<TOutput[]> AsyncMap<TInput, TOutput>(
        this TInput[] inputArray,
        Func<TInput, Task<TOutput>> getAsyncTask
    ) {
        var tasks = inputArray.Select(getAsyncTask).ToArray();
        var outputArray = await Task.WhenAll(tasks);

        return outputArray;
    }
    
}