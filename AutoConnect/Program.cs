using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AutoConnect;

class Program
{
    private string rustFilePath = "";

    private static Program instance;
    private static Bootstrap bootstrap;
    
    public static async Task Main()
    {
        
        instance = new Program();
        bootstrap = new Bootstrap();

        bootstrap.init();
        
        

    }

    
    
}


