using System;
using AutoConnect.Modules.SprayControlModule;

namespace AutoConnect;

internal class Program
{
    private static Bootstrap _bootstrap;
    
    [STAThread]
    public static int Main()
    {
        _bootstrap = new Bootstrap();

        _bootstrap.Init();
        
        SelectModule();
        
        return 1;
    }

    private static void SelectModule()
    {
        Console.Write("Select module or -m for modules: ");
        var module = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(module))
        {
            Console.WriteLine("No module specified");
            return;
        }

        if (Enum.TryParse(module, true, out ModulesList selectedModule))
        {
            Console.WriteLine($"Selected module: {selectedModule}");
            switch (selectedModule)
            {
                case ModulesList.AutoConnect:
                    ModuleRunMessage(selectedModule);
                    RunAutoConnect();
                    break;
                case ModulesList.SprayControl:
                    ModuleRunMessage(selectedModule);   
                    RunOverlayStart();
                    break;
            }
        }
        else
        {
            Console.WriteLine("Available modules:");
            foreach (var availableModule in Enum.GetValues<ModulesList>())
            {
                Console.WriteLine($"  - {availableModule}");
            }
            SelectModule();
        }

        
    }

    private static void ModuleRunMessage(ModulesList module)
    {
        Console.WriteLine($"Running module: {module}");
    }

    private static void RunAutoConnect()
    {
        Modules.AutoConnectModule ac = new Modules.AutoConnectModule();
        ac.StartAutoConnect();
    }

    private static void RunOverlayStart()
    {
        
        Console.WriteLine("Starting overlay");
        
        SprayOverlay overlay = new SprayOverlay();
        overlay.SpawnWindow();
        
        System.Windows.Threading.Dispatcher.Run();
    }
}


