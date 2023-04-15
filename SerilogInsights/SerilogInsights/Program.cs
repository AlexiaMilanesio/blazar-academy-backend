using Serilog;

namespace SerilogInsights;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Serilog!");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        //Log.Logger.Information("This is an information message");
        //Log.Logger.Error("This is an error message");

        try
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    Log.Logger.Warning("This is a warning message");
                }
                else if (i == 1)
                {
                    Log.Logger.Fatal("This is a fatal message");
                }
                else if (i == 4)
                {
                    throw new Exception("This is a demo exception");
                }
                else
                {
                    Log.Logger.Information("The value of i is {iVariable}", i);
                }
            }
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Caught this exception");
        }

    }
}
    
