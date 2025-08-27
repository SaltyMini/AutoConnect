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
    
    public static int Main()
    {
        
        //Initialize
        
        instance = new Program();
        bootstrap = new Bootstrap();

        bootstrap.init();
        
        SelectModule();
        
        return 1;
    }

    public static void SelectModule()
    {
        Console.Write("Select module or -m for modules: ");
        string? module = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(module))
        {
            Console.WriteLine("No module specified");
            return;
        }

        if (Enum.TryParse<ModulesList>(module, true, out ModulesList selectedModule))
        {
            Console.WriteLine($"Selected module: {selectedModule}");
            switch (selectedModule)
            {
                case ModulesList.AutoConnect:
                    RunAutoConnect();
                    break;
            }
        }
        else
        {
            Console.WriteLine($"Invalid module: {module}");
            Console.WriteLine("Available modules:");
            foreach (var availableModule in Enum.GetValues<ModulesList>())
            {
                Console.WriteLine($"  - {availableModule}");
            }
            return;
        }

        
    }

    private static void RunAutoConnect()
    {
        Modules.AutoConnect ac = new Modules.AutoConnect();
        ac.StartAutoConnect();
    }
    
    
}


