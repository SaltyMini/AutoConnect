using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AutoConnect.Modules;

public class AutoConnect
{

    public async void StartAutoConnect()
    {
        Console.WriteLine("Ping target: ");
        string? target = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(target))
        {
            Console.WriteLine("No target specified");
            return;
        }

        Ping pinger = new Ping();
        
        try
        {
            _ = Dns.GetHostEntry(target);

            using var ping = new Ping();
            var reply = ping.Send(target);
            Console.WriteLine(reply.Status);
        }
        catch (SocketException)
        {
            Console.WriteLine("Unknown host (DNS failed).");
        }
        catch (PingException ex) when (ex.InnerException is SocketException)
        {
            Console.WriteLine("Unknown host (DNS failed).");
        }


        while (pinger.Send(target).Status == IPStatus.TimedOut)
        {
            Console.WriteLine("Ping failed");
        }

        Console.WriteLine("Ping succeeded");
        
        await instance.ConnectToServer(target);
    }
    
    
    public bool IsGameRunning()
    {

        

        return false;
    }

    public async Task ConnectToServer(string target)
    {
        
        Console.WriteLine("Connecting client to target: " + target);
        await Task.Delay(TimeSpan.FromSeconds(1));
        
        //TODO: Connect to server logic

        
    }
    
}