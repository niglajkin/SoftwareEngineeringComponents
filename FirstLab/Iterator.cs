namespace FirstLab;

public static class Iterator {
    public static void MyAsyncMap<TEntry, TResult>(
        TEntry[] entryArray,
        Action<TEntry, Action<string?, TResult?>> asyncAction,
        Action<List<string>, TResult[]> onCompleteCallback
    ) {
        var resultArray = new TResult[entryArray.Length];
        var occuredErrors = new List<string>();
        var processedElementsCounter = 0;
        
        for (var i = 0; i < entryArray.Length; i++) {
            var currentElement = entryArray[i];
            
            asyncAction(
                currentElement,
                OnElementProcessed(i)
            );
            
        }

        return;
        
        Action<string?, TResult?> OnElementProcessed(int index) => (errorMassage, newElement) => {
            var processedElementsCount = Interlocked.Increment(ref processedElementsCounter);

            if (errorMassage is not null) occuredErrors.Add(errorMassage);
            
            else {
                Console.WriteLine($"{entryArray[index]}, which has index {index} was handled");
                resultArray[index] = newElement!;
            }

            var allElementsProcessed = processedElementsCount == entryArray.Length;
            if (allElementsProcessed) onCompleteCallback(occuredErrors, resultArray);
            
        };
        
    }
    
} 