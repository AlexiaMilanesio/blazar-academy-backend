using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System.Xml.Linq;


namespace NotesApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("CRUD Notes Console App in C#\r");
        Console.WriteLine("----------------------------");


        // MEMORY
        // List<NoteClass> notes = new List<NoteClass>();

        string jsonNotes;
        using (StreamReader sr = new StreamReader("CDriversDirs.txt"))
        {
           jsonNotes = sr.ReadToEnd();
        }
        List<NoteClass> savedNotes = JsonConvert.DeserializeObject<List<NoteClass>>(jsonNotes);



        void Save(List<NoteClass> notes)
        {
            string serializedNotes = JsonConvert.SerializeObject(notes);
            using (StreamWriter sw = new StreamWriter("CDriversDirs.txt"))
            {
                sw.WriteLine(serializedNotes);
            }
        }


        void ShowNote(NoteClass note)
        {
            Console.WriteLine(
                "\n\tId: " + note.Id + "\n" +
                "\tTitle: " + note.Title + "\n" +
                "\tCreation date: " + note.CreationDate + "\n" +
                "\tLast modification date: " + note.ModificationDate + "\n" +
                "\tExpiration date: " + note.ExpirationDate + "\n" +
                "\tText: " + note.Text + "\n"
            );
        }
         

        void ShowActionsOptions()
        {
            Console.WriteLine("\nWhat do you want to do? Choose an option from the following list:");
            Console.WriteLine("\ta - See all notes");
            Console.WriteLine("\tb - Add a new note");
            Console.WriteLine("\tc - Search a note");
            Console.WriteLine("\td - Update a note");
            Console.WriteLine("\te - Delete a note");
            Console.WriteLine("\tf - Close Notes Console App");
            Console.Write("Your option? ");
        }


        bool ValidateString(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }


        NoteClass? FindNote(string id)
        {
            return savedNotes.Find(note => note.Id == id);
        }


        // Facoltativo: mostrare nota quando sia la data di scadenza
        void ExpiredNotes()
        {
            List<NoteClass> expiredNotes = savedNotes.FindAll(note => note.ExpirationDate <= DateTime.Now);

            if (expiredNotes.Count != 0)
            {
                if (expiredNotes.Count == 1)
                {
                    Console.WriteLine("\nYou have an expired note:");
                }
                else if (expiredNotes.Count > 1)
                {
                    Console.WriteLine("\nYou have some expired notes:");
                }
                expiredNotes.ForEach(note =>
                {
                    ShowNote(note);
                    Console.WriteLine("--------------------------------------------------------------------");
                });
            }
        }

        ExpiredNotes();



    start:
        ShowActionsOptions();


        string input = Console.ReadLine();

        if (ValidateString(input) || input != "a" && input != "b" && input != "c" && input != "d" && input != "e" && input != "f")
        {
            Console.WriteLine("\nError, please select an existing option");
            goto start;
        }



        switch (input)
        {
            // LISTING ALL EXISTING NOTES
            case "a":
                if (savedNotes.Count != 0)
                {
                    Console.WriteLine("\nHere are all your notes:");

                    savedNotes.ForEach(note =>
                    {
                        ShowNote(note);
                        Console.WriteLine("--------------------------------------------------------------------");
                    });
                    goto start;
                }
                else
                {
                    Console.WriteLine("\nYou don't have any notes");
                    goto start;
                }



            // CREATING & ADDING A NEW NOTE
            case "b": 
                Console.WriteLine("\nCREATING A NEW NOTE\r");
                Console.WriteLine("-------------------");

            startCreating:

                Console.WriteLine("Type a title for your note, and then press enter");
            checkTitle:
                string? title = Console.ReadLine();
                if (ValidateString(title))
                {
                    Console.WriteLine("Title can't be empty! Input the title once more");
                    goto checkTitle;
                }


                Console.WriteLine($"Type the text for your \"{title}\" note, and then press enter");
            checkText:
                string? text = Console.ReadLine();
                if (ValidateString(text))
                {
                    Console.WriteLine("Text can't be empty! Input the text once more");
                    goto checkText;
                }


                Console.WriteLine($"When will \"{title}\" note expire? Type a number, and the press enter");
            checkNumber:
                string? days = Console.ReadLine();
                if (ValidateString(days))
                {
                    Console.WriteLine("Expiration date can't be empty! Input the number once more");
                    goto checkNumber;
                }

                int dte = Convert.ToInt32(days); // dte = days to expiration
                if (dte < 0)
                {
                    Console.WriteLine("Days to expiration can not take negative numbers, type 0 or a positive number");
                    goto checkNumber;
                }

                DateTime expirationDate = Convert.ToDateTime(DateTime.Now.AddDays(dte));


                DateTime creationDate = Convert.ToDateTime(DateTime.Now);
                DateTime modificationDate = Convert.ToDateTime(DateTime.Now);

                Guid uuid = Guid.NewGuid();
                string id = uuid.ToString();


                NoteClass newNote = new NoteClass(id, title, creationDate, modificationDate, expirationDate, text);
                savedNotes.Add(newNote);

                Console.WriteLine("\nYour note was successfully created! Check it out:");
                ShowNote(newNote);
                Save(savedNotes);
                goto start;
               


            // SEARCHING A PARTICULAR NOTE BY ID OR TITLE
            case "c":
                Console.WriteLine("\nSEARCHING A NOTE\r");
                Console.WriteLine("----------------");

            startSearching:
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Search a note by id");
                Console.WriteLine("\tb - Search a note by title");
                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "a":
                    startSearchingById:
                        Console.WriteLine("\nType the note's id, and then press enter");
                        string? searchedId = Console.ReadLine();

                        if (!ValidateString(searchedId))
                        {
                            NoteClass? foundNote = FindNote(searchedId);
                            if (foundNote != null)
                            {
                                Console.WriteLine("\nHere's your note:");
                                ShowNote(foundNote);
                                goto start;
                            }
                            else
                            {
                                Console.WriteLine("\nNote not found, try with a different id");
                                goto startSearchingById;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nId not valid, type a valid id");
                            goto startSearchingById;
                        }


                    case "b":
                    startSearchingByTitle:
                        Console.WriteLine("\nType the note's title, and then press enter");
                        string? searchedTitle = Console.ReadLine();

                        if (!ValidateString(searchedTitle))
                        {
                            NoteClass? foundNote = savedNotes.Find(note => note.Title == searchedTitle);
                            if (foundNote != null)
                            {
                                Console.WriteLine("\nHere's your note:");
                                ShowNote(foundNote);
                                goto start;
                            }
                            else
                            {
                                Console.WriteLine("\nNote not found, please try with a different title");
                                goto startSearchingByTitle;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nTitle does not exist, please type an existing title\n");
                            goto startSearchingByTitle;
                        }


                    default:
                        Console.WriteLine("\nError, please select an existing option\n");
                        goto startSearching;
                }



            // EDITING AN EXISTING NOTE BY ID
            case "d":
                Console.WriteLine("\nEDITING A NOTE\r");
                Console.WriteLine("--------------");

            startEditing:
                Console.WriteLine("Type the note's id you want to edit, and then press enter");
                string? editId = Console.ReadLine();


                if (!ValidateString(editId))
                {
                    NoteClass? foundNote = FindNote(editId);
                    if (foundNote != null)
                    {
                        Console.WriteLine("\nThis is the note your are editing:");
                        ShowNote(foundNote);


                        Console.WriteLine("Type a new title, and then press enter");
                    checkNewTitle:
                        string? newTitle = Console.ReadLine();
                        if (ValidateString(newTitle))
                        {
                            Console.WriteLine("Title can't be empty! Input the title once more");
                            goto checkNewTitle;
                        }
                        foundNote.Title = newTitle;


                        Console.WriteLine("Type a new text, and then press enter");
                    checkNewText:
                        string? newText = Console.ReadLine();
                        if (ValidateString(newText))
                        {
                            Console.WriteLine("Text can't be empty! Input the title once more");
                            goto checkNewText;
                        }
                        foundNote.Text = newText;


                        Console.WriteLine("Type days to expiration from today, and then press enter");
                    checkNewNumber:
                        string? newDays = Console.ReadLine(); 
                        if (ValidateString(newDays))
                        {
                            Console.WriteLine("Expiration date can't be empty! Input the number once more");
                            goto checkNewNumber;
                        }

                        int newDTE = Convert.ToInt32(newDays);
                        if (newDTE < 0)
                        {
                            Console.WriteLine("Days to expiration can not take negative numbers, type 0 or a positive number");
                            goto checkNewNumber;
                        }

                        foundNote.ExpirationDate = Convert.ToDateTime(DateTime.Now.AddDays(newDTE));


                        DateTime newModificationDate = Convert.ToDateTime(DateTime.Now);
                        foundNote.ModificationDate = newModificationDate;


                        Console.WriteLine("\nYour note has been successfully edited! Check it out:");
                        ShowNote(foundNote);
                        Save(savedNotes);
                        goto start;
                    }
                    else
                    {
                        Console.WriteLine("\nNote not found, try with a different id\n");
                        goto startEditing;
                    }
                }

                else
                {
                    Console.WriteLine("\nId not valid, type a valid id\n");
                    goto startEditing;
                }



            // DELETING A NOTE BY ID
            case "e":
                Console.WriteLine("\nDELETING A NOTE\r");
                Console.WriteLine("---------------");

            startDeleting:
                Console.WriteLine("Type the note's id you want to delete");
                string? deleteId = Console.ReadLine();

                if (!ValidateString(deleteId))
                {
                    NoteClass? foundNote = FindNote(deleteId);
                    if (foundNote != null)
                    {
                        Console.WriteLine("\nHere is the note you want to delete:");
                        ShowNote(foundNote);

                    startDeletingConfirmation:
                        Console.WriteLine("\nAre you sure you want to delete this note? Yes or no?:");
                        Console.WriteLine("\ta - Yes");
                        Console.WriteLine("\tb - No");
                        Console.Write("Your option? ");

                        switch (Console.ReadLine())
                        {
                            case "a":
                                Console.WriteLine("\nYour note has been successfully deleted");
                                savedNotes.Remove(foundNote);
                                Save(savedNotes);
                                goto start;

                            case "b":
                                goto start;

                            default:
                                Console.WriteLine("\nError, please select an existing option");
                                goto startDeletingConfirmation;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNote not found, try with a different id\n");
                        goto startDeleting;
                    }
                }

                else
                {
                    Console.WriteLine("\nId not valid, type a valid id\n");
                    goto startDeleting;
                }



            // CLOSING/EXITING THE APP
            case "f":
                Console.WriteLine("\nEXITING THE APP\r");
                Console.WriteLine("---------------");

                Console.Write("Press any key to close the Console Notes App...");
                break;
        }
       

        Console.ReadKey();
    }
}
