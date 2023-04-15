using App;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // Come si dichiara un intero e impostarlo a 7
        int mioIntero1 = 7;
        // int mioIntero2 = "7"; // Rosso --> Errore! Non si può assegnare una stringa a un intero

        // Breakpoints
        Console.WriteLine($"Mio intero: {mioIntero1}");


        // Generics
        // List<T> miaLista1 = new();
        List<int> miaLista1 = new();
        List<string> miaLista2 = new();

        miaLista1.Add(13);
        miaLista2.Add("Pippo");


        // Come utilizzare un Generic?
        // Ho dichiarato, specificato in maniera precisa il mio Generic
        MioGenerico<DateTime> mieDate = new();

        mieDate.Add("Compleanno", new DateTime(2000, 12, 25));

        Console.ReadKey();
    }
}