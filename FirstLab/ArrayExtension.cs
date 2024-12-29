namespace FirstLab;

public static class ArrayExtension {
    
    public static void AsyncMap<TInput, TOutput>(
        this TInput[] inputArray,
        Action<TInput, Action<string?, TOutput?>> asyncAction,
        Action<List<string>, TOutput[]> onCompleteCallback
    ) {
        var resultArray = new TOutput[inputArray.Length];
        var occuredErrorMessages = new List<string>();
        
        var processedElementsCounter = 0;

        for (var i = 0; i < inputArray.Length; i++) {
            var currentElement = inputArray[i];

            asyncAction(
                currentElement,
                OnElementProcessed(i)
            );
        }

        return;

        Action<string?, TOutput?> OnElementProcessed(int index) => (errorMassage, newElement) => {
            var processedElementsCount = Interlocked.Increment(ref processedElementsCounter);

            if (errorMassage is not null) occuredErrorMessages.Add(errorMassage);

            else {
                Console.WriteLine($"{inputArray[index]}, which has index {index} was handled");
                resultArray[index] = newElement!;
            }

            var allElementsProcessed = processedElementsCount == inputArray.Length;
            if (allElementsProcessed) onCompleteCallback(occuredErrorMessages, resultArray);
        };
    }
    
    
} 