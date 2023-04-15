using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CIE;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("CIE Console App in C#\r");
        Console.WriteLine("---------------------");


        string jsonCIEs;
        using (StreamReader sr = new StreamReader("CDriversDirs.txt"))
        {
            jsonCIEs = sr.ReadToEnd();
        }
        List<CIEClass> savedCIEs = JsonConvert.DeserializeObject<List<CIEClass>>(jsonCIEs);


        void SaveCIE(List<CIEClass> cies)
        {
            string serializedCIEs = JsonConvert.SerializeObject(cies);
            using (StreamWriter sw = new StreamWriter("CDriversDirs.txt"))
            {
                sw.WriteLine(serializedCIEs);
            }
        }


        void ShowCIE(CIEClass cie)
        {
            Console.WriteLine(
                "\n\tNome: " + cie.Nome + "\n" +
                "\tCognome: " + cie.Cognome + "\n" +
                "\tCittà di residenza: " + cie.CittaDiResidenza + "\n" +
                "\tData di nascita: " + cie.DataDiNascita + "\n"
            );
        }


        bool IsInvalidString(string? str)
        {
            if (string.IsNullOrWhiteSpace(str)) return true;
            if (!Regex.IsMatch(str, "[a-zA-Z]")) return true;
            else return false;
        }


        CIEClass? FindCIE(string cognome)
        {
            return savedCIEs.Find(cie => cie.Cognome == cognome);
        }


        void ShowOptions()
        {
            Console.WriteLine("\nWhat do you want to do? Choose an option from the following list:");
            Console.WriteLine("\ta - See all CIEs");
            Console.WriteLine("\tb - Add a new CIE");
            Console.WriteLine("\tc - Search a CIE");
            Console.WriteLine("\td - Update a CIE");
            Console.WriteLine("\te - Delete a CIE");
            Console.WriteLine("\tf - Close CIEs Console App");
            Console.Write("Your option? ");
        }


    start:
        ShowOptions();

        string input = Console.ReadLine();

        if (IsInvalidString(input) || input != "a" && input != "b" && input != "c" && input != "d" && input != "e" && input != "f")
        {
            Console.WriteLine("\nError, please select an existing option.");
            goto start;
        }



        switch (input)
        {
            // LISTING ALL EXISTING CIES
            case "a":
                if (savedCIEs.Count != 0)
                {
                    Console.WriteLine("\nList of existing CIEs:");

                    savedCIEs.ForEach(cie =>
                    {
                        ShowCIE(cie);
                        Console.WriteLine("--------------------------------------------------------------------");
                    });
                    goto start;
                }
                else
                {
                    Console.WriteLine("\nCIE list is empty.");
                    goto start;
                }



            // ADDING A NEW CIE
            case "b":
                Console.WriteLine("\nADDING A NEW CIE\r");
                Console.WriteLine("-----------------");


                Console.WriteLine($"Type date of birth (format: yyyy, mm, dd):");
            checkDateOfBirth:
                Console.WriteLine($"Enter the year (format: yyyy):");
                string? year = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(year) || !int.TryParse(year, out int yyyy))
                {
                    Console.WriteLine("Year of birth can't be empty. Input the year once more.");
                    goto checkDateOfBirth;
                }

                Console.WriteLine($"Enter the month (format: mm):");
                string? month = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(month) || !int.TryParse(month, out int mm))
                {
                    Console.WriteLine("Month of birth can't be empty. Input the month once more.");
                    goto checkDateOfBirth;
                }

                Console.WriteLine($"Enter the day (format: dd):");
                string? day = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(day) || !int.TryParse(day, out int dd))
                {
                    Console.WriteLine("Day of birth can't be empty. Input the day once more.");
                    goto checkDateOfBirth;
                }

                DateTime validDate = new DateTime(1900, 1, 1);
                DateTime dateOfBirth = Convert.ToDateTime(new DateTime(yyyy, mm, dd));
                if (dateOfBirth <= validDate)
                {
                    Console.WriteLine("Date of birth can't be previous to 1/1/1900.");
                    goto checkDateOfBirth;
                }


                Console.WriteLine("Type name:");
            checkName:
                string? name = Console.ReadLine();
                if (IsInvalidString(name))
                {
                    Console.WriteLine("Name can't be empty and can't include numbers. Input the name once more.");
                    goto checkName;
                }


                Console.WriteLine($"Type surname:");
            checkSurname:
                string? surname = Console.ReadLine();
                if (IsInvalidString(surname))
                {
                    Console.WriteLine("Surname can't be empty and can't include numbers. Input the surname once more.");
                    goto checkSurname;
                }


                Console.WriteLine($"Type city of residency:");
            checkCityOfResidency:
                string? city = Console.ReadLine();
                if (IsInvalidString(city))
                {
                    Console.WriteLine("City of residency can't be empty and can't include numbers. Input the city once more.");
                    goto checkCityOfResidency;
                }


                CIEClass newCIE = new CIEClass(name, surname, city, dateOfBirth); 
                savedCIEs.Add(newCIE);

                Console.WriteLine("\nNew CIE successfully added! Check it out:");
                ShowCIE(newCIE);
                SaveCIE(savedCIEs);
                goto start;



            // SEARCHING A PARTICULAR CIE BY SURNAME
            case "c":
                Console.WriteLine("\nSEARCHING A CIE\r");
                Console.WriteLine("---------------");

            startSearch:
                Console.WriteLine("\nType the CIE's surname:");
                string? searchedSurname = Console.ReadLine();

                if (!IsInvalidString(searchedSurname))
                {
                    CIEClass? foundCIE = FindCIE(searchedSurname);
                    if (foundCIE != null)
                    {
                        Console.WriteLine("\nHere's the CIE:");
                        ShowCIE(foundCIE);
                        goto start;
                    }
                    else
                    {
                        Console.WriteLine("\nCIE not found, try with a different surname.");
                        goto startSearch;
                    }
                }
                else
                {
                    Console.WriteLine("\nSurname not valid, type a valid surname.");
                    goto startSearch;
                }



            // EDITING AN EXISTING CIE BY SURNAME
            case "d":
                Console.WriteLine("\nEDITING A NOTE\r");
                Console.WriteLine("--------------");

            startEditing:
                Console.WriteLine("Type the CIE's surname you want to edit:");
                string? editBySurname = Console.ReadLine();


                if (!IsInvalidString(editBySurname))
                {
                    CIEClass? foundCIE = FindCIE(editBySurname);
                    if (foundCIE != null)
                    {
                        Console.WriteLine("\nThis is the CIE your are editing:");
                        ShowCIE(foundCIE);


                        Console.WriteLine("Type a new name:");
                    checkNewName:
                        string? newName = Console.ReadLine();
                        if (IsInvalidString(newName))
                        {
                            Console.WriteLine("Name can't be empty and can't include numbers. Input the name once more.");
                            goto checkNewName;
                        }
                        foundCIE.Nome = newName;


                        Console.WriteLine("Type a new surname:");
                    checkNewSurname:
                        string? newSurname = Console.ReadLine();
                        if (IsInvalidString(newSurname))
                        {
                            Console.WriteLine("Text can't be empty and can't include numbers. Input the title once more.");
                            goto checkNewSurname;
                        }
                        foundCIE.Cognome = newSurname;


                        Console.WriteLine("Type a new surname:");
                    checkNewCity:
                        string? newCity = Console.ReadLine();
                        if (IsInvalidString(newCity))
                        {
                            Console.WriteLine("Text can't be empty and can't include numbers. Input the title once more.");
                            goto checkNewCity;
                        }
                        foundCIE.CittaDiResidenza = newCity;


                        Console.WriteLine($"Type new date of birth (format: dd/mm/yyyy):");
                    checkNewDateOfBirth:
                        Console.WriteLine($"Enter the new year (format: yyyy):");
                        string? newYear = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newYear) || !int.TryParse(newYear, out int newyyyy))
                        {
                            Console.WriteLine("Year of birth can't be empty. Input the year once more.");
                            goto checkNewDateOfBirth;
                        }

                        Console.WriteLine($"Enter the new month (format: mm):");
                        string? newMonth = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newMonth) || !int.TryParse(newMonth, out int newmm))
                        {
                            Console.WriteLine("Month of birth can't be empty. Input the month once more.");
                            goto checkNewDateOfBirth;
                        }

                        Console.WriteLine($"Enter the day (format: dd):");
                        string? newDay = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newDay) || !int.TryParse(newDay, out int newdd))
                        {
                            Console.WriteLine("Day of birth can't be empty. Input the day once more.");
                            goto checkNewDateOfBirth;
                        }

                        DateTime newDataDiNascita = Convert.ToDateTime(new DateTime(newyyyy, newmm, newdd));
                        DateTime newValidDate = new DateTime(1900, 1, 1);
                        if (newDataDiNascita <= newValidDate)
                        {
                            Console.WriteLine("Date of birth can't be previous to 1/1/1900.");
                            goto checkDateOfBirth;
                        }
                        foundCIE.DataDiNascita = newDataDiNascita;


                        Console.WriteLine("\nCIE successfully edited! Check it out:");
                        ShowCIE(foundCIE);
                        SaveCIE(savedCIEs);
                        goto start;
                    }
                    else
                    {
                        Console.WriteLine("\nCIE not found, try with a different surname.\n");
                        goto startEditing;
                    }
                }

                else
                {
                    Console.WriteLine("\nSurname not valid, type a valid surname.\n");
                    goto startEditing;
                }



            // DELETING A NOTE BY SURNAME
            case "e":
                Console.WriteLine("\nDELETING A CIE\r");
                Console.WriteLine("--------------");

            startDeleting:
                Console.WriteLine("Type the CIE's surname you want to delete:");
                string? deleteBySurname = Console.ReadLine();

                if (!IsInvalidString(deleteBySurname))
                {
                    CIEClass? foundCIE = FindCIE(deleteBySurname);
                    if (foundCIE != null)
                    {
                        Console.WriteLine("\nHere is the CIE you want to delete:");
                        ShowCIE(foundCIE);

                    startDeletingConfirmation:
                        Console.WriteLine("\nAre you sure you want to delete this CIE? Yes or no?:");
                        Console.WriteLine("\ty - Yes");
                        Console.WriteLine("\tn - No");
                        Console.Write("Your option? ");

                        switch (Console.ReadLine())
                        {
                            case "y":
                                Console.WriteLine("\nCIE successfully deleted.");
                                savedCIEs.Remove(foundCIE);
                                SaveCIE(savedCIEs);
                                goto start;

                            case "n":
                                goto start;

                            default:
                                Console.WriteLine("\nError, please select an existing option.");
                                goto startDeletingConfirmation;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nCIE not found, try with a different surname.\n");
                        goto startDeleting;
                    }
                }

                else
                {
                    Console.WriteLine("\nSurname not valid, type a valid surname.\n");
                    goto startDeleting;
                }



            // CLOSING THE APP
            case "f":
                Console.WriteLine("\nEXITING THE APP\r");
                Console.WriteLine("---------------");

                Console.Write("Press any key to close the Console CIEs App...");
                break;
        }



        Console.ReadKey();
    }
}

