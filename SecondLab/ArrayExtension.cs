namespace SecondLab;

public static class ArrayExtension {
    public static Task<TOutput[]> AsyncMap<TInput, TOutput>(
        this TInput[] inputArray,
        Func<TInput, Task<TOutput>> getAsyncTask
    ) {
        var arraysLength = inputArray.Length;
        var tasks = new Task<TOutput>[arraysLength];
        
        for (var i = 0; i < arraysLength; i++) {
            var currentElement = inputArray[i];
            tasks[i] = getAsyncTask(currentElement);
        }

        return Task.WhenAll(tasks);
    }
    
}