using System;
using Steamworks;

namespace AutoConnect;

public class Bootstrap
{

    public bool init()
    {
            try
            {
                SteamClient.Init( 480, true );
                return true;
            }
            catch ( Exception e )
            {
                Console.WriteLine("Error init steamworks, existing");
                return false;
            }
    }

    
}