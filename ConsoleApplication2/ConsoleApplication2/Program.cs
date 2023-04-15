using ConsoleApplication2;
using Newtonsoft.Json;


internal class Program
{
    private static void Main(string[] args)
    {
        ClassCIE classCIE1 = new ClassCIE();

        classCIE1.FirstName = "Alexía";
        classCIE1.LastName = "Milanesio";
        classCIE1.CityOfResidence = "Varese";
        classCIE1.DateOfBirth = new DateTime(1890, 10, 24);

        Console.WriteLine(classCIE1);

        string serializedClassCIE = JsonConvert.SerializeObject(classCIE1);

        using (StreamWriter sw = new StreamWriter("CDriversDirs.txt"))
        {
            sw.WriteLine(serializedClassCIE);
        }
    }
}
