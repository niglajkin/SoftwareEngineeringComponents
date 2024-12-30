using System.Net;
using FirstLab;

var domainNames = new[] {
    "microsoft.com", "google.com",
    "youtube.com", "github.com"
};

domainNames.AsyncMap<string, string>(GetHostEntryAsync, PrintResults);

Console.ReadKey();

return;

void PrintResults(List<string> errorMassages, string[] ipAddresses) {
    var print = (IEnumerable<string> collection) => {
        foreach (var element in collection) {
            Console.WriteLine(element);
        }
    };
    
    print(
        errorMassages.Count != 0
            ? errorMassages
            : ipAddresses
    );
}

void GetHostEntryAsync(string hostName, Action<string?, string?> callback) {
    Dns.BeginGetHostEntry(hostName, asyncResult => {
        try {
            var hostEntry = Dns.EndGetHostEntry(asyncResult);
            var ipAddresses = hostEntry.AddressList;

            var formattedResults =
                $"{Environment.NewLine}{hostName} has such ip addresses:{Environment.NewLine}" +
                string.Join(Environment.NewLine, ipAddresses.Select(
                    address => address.ToString()
                ));


            callback(null, formattedResults);
        }

        catch (Exception e) {
            callback($"{Environment.NewLine}Exception was thrown. {e}{Environment.NewLine}", null);
        }
    }, null);
}

//Results:

/*
Result when there is/are an error/s({"(microsoft.com", "gfoodafsasgle.com", "youfaaftfubse.com"}):

Exception was thrown. System.Net.Sockets.SocketException (11001): Unknown host...
Exception was thrown. System.Net.Sockets.SocketException (11001): Unknown host...


Result when everything is  successful:

microsoft.com, which has index 0 was handled
github.com, which has index 3 was handled
google.com, which has index 1 was handled
youtube.com, which has index 2 was handled

microsoft.com has such ip addresses:
20.112.250.133
20.231.239.246
20.76.201.171
20.236.44.162
20.70.246.20

google.com has such ip addresses:
142.251.208.110

youtube.com has such ip addresses:
142.250.201.206

github.com has such ip addresses:
140.82.121.3
*/