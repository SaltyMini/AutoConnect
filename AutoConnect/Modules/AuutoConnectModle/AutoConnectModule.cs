using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Steamworks;

namespace AutoConnect.Modules;

public class AutoConnectModule
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

        await ConnectToServer(target);
    }
    
    
    public bool IsRustRunning()
    {
        try
        {
            // Rust's App ID is 252490
            var rustAppId = 252490;
            
            if (!SteamApps.IsAppInstalled(rustAppId))
                return false;
            
            try
            {
                var processes = System.Diagnostics.Process.GetProcessesByName("RustClient");
                return processes.Length > 0;
            }
            catch
            {
                return false;
            }
            
        } catch (Exception e)
        {
            Console.WriteLine("Error checking if game is running");
            Console.WriteLine(e);
            return false;
        }


    }

    public async Task ConnectToServer(string target)
    {

        Console.WriteLine("Connecting client to target: " + target);
        await Task.Delay(TimeSpan.FromSeconds(1));

        if (IsRustRunning())
        {
            
        }
        else
        {
            //launch rust, then focus, then connect
        }


}
    
}