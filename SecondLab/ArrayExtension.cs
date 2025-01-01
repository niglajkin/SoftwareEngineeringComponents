namespace SecondLab;

public static class ArrayExtension {
    
    public static Task<(TOutput[], List<Exception>)> AsyncMap<TInput, TOutput>(
        this TInput[] inputArray,
        Func<TInput, Task<TOutput>> getAsyncTask
    ) {
        var arraysLength = inputArray.Length;

        var outputArray = new TOutput[arraysLength];
        var occuredExceptions = new List<Exception>();
        var tasks = new List<Task>();

        for (var i = 0; i < arraysLength; i++) {
            var capturedIndex = i;
            var currentElement = inputArray[capturedIndex];

            var task = getAsyncTask(currentElement).ContinueWith(resultTask => {
                if (resultTask.IsFaulted) {
                    var exceptions = resultTask.Exception.InnerExceptions;
                    occuredExceptions.AddRange(exceptions);
                    return;
                }

                outputArray[capturedIndex] = resultTask.Result;
            });

            tasks.Add(task);
        }
        
        return Task.WhenAll(tasks).ContinueWith(_ => (outputArray, occuredExceptions));
    }
    
}