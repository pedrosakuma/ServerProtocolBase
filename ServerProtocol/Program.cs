using ServerProtocol.Server;
using ServerProtocol.Server.Protocols;
using ServerProtocol.Server.ServerConstrains;

class Program
{
    private static bool isRunning = false;

    static async Task Main(string[] args)
    {
        Console.Title = "Game Server";

        ProtocolManager.InitProtocols();
        Server.Start();

        await Task.Delay(-1);
    }

    private static void MainThread()
    {
        Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
        DateTime _nextLoop = DateTime.Now;

        while (isRunning)
        {
            while (_nextLoop < DateTime.Now)
            {
                ServerLogic.Update();

                _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                if (_nextLoop > DateTime.Now)
                {
                    Thread.Sleep(_nextLoop - DateTime.Now);
                }
            }
        }
    }
}