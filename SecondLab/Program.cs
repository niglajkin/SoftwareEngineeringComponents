using System.Net;
using SecondLab;

Console.WriteLine("App is running");

var domainNames = new[] { "google.com", "github.com", "youtube.com" };

domainNames.AsyncMap(Dns.GetHostAddressesAsync).ContinueWith(task => {
    var ipAddresses = task.Result;

    for (var i = 0; i < ipAddresses.Length; i++) {
        var domainName = domainNames[i];
        var ipAddress = ipAddresses[i];
        
        Console.WriteLine($"{domainName}, IP:{ipAddress.First()}");
    }
});

Console.ReadKey();