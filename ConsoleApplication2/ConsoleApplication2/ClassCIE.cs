using System;
using System.Text.RegularExpressions;


namespace ConsoleApplication2
{
	public class ClassCIE
	{
        private string? firstName;
        private string? lastName;
        private string? cityOfResidence;
        private DateTime dateOfBirth;
         

        public string FirstName
        {
            get { return firstName; }
            set
            {
                try
                {
                    bool valid = Regex.IsMatch(value, "[a-zA-Z]");

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Il nome non può essere null o empty.");
                    }
                    else if (!valid)
                    {
                        throw new Exception("Il nome inserito non deve contenere cifre e/o numeri"); 
                    }
                    else
                    {
                        firstName = value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public string LastName
        {
            get { return lastName; }
            set
            {
                try
                {
                    bool valid = Regex.IsMatch(value, "[a-zA-Z]");

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Il cognome non può essere null o empty.");
                    }
                    else if (!valid)
                    {
                        throw new Exception("Il cognome inserito non deve contenere cifre e/o numeri");
                    }
                    else
                    {
                        lastName = value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public string CityOfResidence
        {
            get { return cityOfResidence; }
            set
            {
                try
                {
                    bool valid = Regex.IsMatch(value, "[a-zA-Z]");

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("Il cognome non può essere null o empty.");
                    }
                    else if (!valid)
                    {
                        throw new Exception("La città di residenza non deve contenere cifre e/o numeri"); 
                    }
                    else
                    {
                        cityOfResidence = value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                try
                {
                    DateTime FirstAcceptableDate = new DateTime(1900, 1, 1);
                    bool validDate = value >= FirstAcceptableDate;

                    if (!validDate)
                    {
                        throw new Exception("La data di nascita deve essere posteriore al 01/01/1900");
                    }
                    else
                    {
                        dateOfBirth = value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}

