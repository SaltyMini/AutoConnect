using System;
using Steamworks;

namespace AutoConnect;

public class Bootstrap : IDisposable
{
    
    private bool _isInitialized = false;
    private bool _disposed = false;

    public bool Init()
    {
        try
        {
            // 480 is temp app ID for dev
            SteamClient.Init(480, true);
            
            // Verify initialization worked
            if (!SteamClient.IsValid)
            {
                Console.WriteLine("Failed to initialize Steam client");
                return false;
            }

            _isInitialized = true;
            Console.WriteLine("Steam initialized successfully");
            
            // Optional: Get user info
            var steamId = SteamClient.SteamId;
            var userName = SteamClient.Name;
            Console.WriteLine($"Logged in as: {userName} (ID: {steamId})");
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error initializing Steamworks:");
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public void RunCallbacks()
    {
        if (_isInitialized)
        {
            SteamClient.RunCallbacks();
        }
    }

    public void Shutdown()
    {
        if (_isInitialized && !_disposed)
        {
            try
            {
                SteamClient.Shutdown();
                _isInitialized = false;
                Console.WriteLine("Steam shutdown complete");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during Steam shutdown: {e.Message}");
            }
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Shutdown();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    ~Bootstrap()
    {
        Dispose();
    }
}