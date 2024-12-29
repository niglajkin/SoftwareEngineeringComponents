using System.Net;
using FirstLab;

var domainNames = new[] { "microsoft.com", "google.com" };

Iterator.MyAsyncMap<string, string>(
    domainNames,
    GetHostEntryAsync,
    PrintResults
);

Console.ReadKey();

return;

void PrintResults(List<string> errorMassages, string[] ipAddresses) {
    if (errorMassages.Count != 0) {
        foreach (var massage in errorMassages) {
            Console.WriteLine(massage);
        }
    }

    else {
        Console.WriteLine();
        
        for (var i = 0; i < ipAddresses.Length; i++) {
            var domainName = domainNames[i];
            var ipAddress = ipAddresses[i];

            var stringToPrint = $"{domainName} has such ip addresses:" +
                                $"{Environment.NewLine}{ipAddress}" +
                                Environment.NewLine;
            
            Console.WriteLine(stringToPrint);
        }
        
    }
    
}

void GetHostEntryAsync(string hostName, Action<string?, string?> callback) {
    Dns.BeginGetHostEntry(hostName, asyncResult => {
        try {
            var hostEntry = Dns.EndGetHostEntry(asyncResult);
            var ipAddresses = hostEntry.AddressList;

            var formattedAddresses = string.Join(Environment.NewLine, ipAddresses.Select(
                address => address.ToString()
            ));

            callback(null, formattedAddresses);
        }
        
        catch (Exception ex) {
            callback(ex.Message, null);
        }
        
    }, null);
}