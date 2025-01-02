namespace SecondLab;

public static class ArrayExtension {
    
    public static Task<TOutput[]> AsyncMap<TInput, TOutput>(
        this TInput[] inputArray,
        Func<TInput, Task<TOutput>> getAsyncTask
    ) {
        var newTasks = inputArray.Select(getAsyncTask).ToArray();

        return Task.WhenAll(newTasks);
    }
    
}