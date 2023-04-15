// Scrivere una classe che gestisce i dati di una persona --> per memorizzare la informazione utilizzamo la proprietà
// istanziarla e serializzarla

using First_App;
using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        // ISTANZIARE

        // Prima di tutto, per utilizzare una classe devo prima istanziare l'oggetto
        // Devo dichiarare una variabili del tipo della classe creata:
            // Prima del uguale --> Dichiarazione di variabile: tipo e nome
            // Dopo del uguale --> la parola chiave new crea una nuova istanza della classe
        Class1 class1 = new Class1();
        // Prende riferimento della class1 (perché sta nel heap)


        // Per accedere alle proprietà della classe o popolare la classe si usa dot notation
        class1.Nome = "Alexía";
        class1.Cognome = "Milanesio";
        class1.DataDiNascita = new DateTime(1995, 10, 24);


        // SERIALIZZARE

        // Questo metodo è più veloce che Jsonserializer.SerializeObject(class1)
        string serializedClass1 = JsonConvert.SerializeObject(class1);


        // Write each directory name to a file.
        // C'è una dichiarazione parte sinistra e istanzia una classe. Questo ha un suo corpo.
        // Variabile dichiarata dopo using viene distrutta la variabile sw alla riga 41
        using (StreamWriter sw = new StreamWriter("CDriveDirs.txt")) // path che vuolete scrivere
        {
            sw.WriteLine(serializedClass1);
            sw.Flush();
        }




        // INVERSO - DESERIALIZZARE
        string ReadClass;

        using (StreamReader sr = new StreamReader("CDriveDirs.txt"))
        {
            ReadClass = sr.ReadToEnd();
        }

        Class1 DeserializedClass1 = JsonConvert.DeserializeObject<Class1>(ReadClass);


    }
}



