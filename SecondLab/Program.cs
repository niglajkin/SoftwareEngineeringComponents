using System.Net;
using SecondLab;

Console.WriteLine("App is running");

var domainNames = new[] { "googssfle.com", "mifsfcrosoft.com", "jwfjf", "jfa;j" };

domainNames.AsyncMap(Dns.GetHostAddressesAsync).ContinueWith(PrintResults);

Console.ReadKey();

return;

void PrintResults(Task<(IPAddress[][], List<Exception>)> task) {
    var (ipAddresses, exceptions) = task.Result;
    
    if(exceptions.Count != 0) exceptions.ForEach(Console.WriteLine);
    else {
        foreach (var ip in ipAddresses) {
            Console.WriteLine($"IP:{ip.First()}");
        }
    }

}