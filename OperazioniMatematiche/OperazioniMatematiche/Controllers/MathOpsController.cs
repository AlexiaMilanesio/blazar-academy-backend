using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace OperazioniMatematiche.Controllers;

[ApiController]
[Route("[controller]")]
public class MathOpsController : ControllerBase
{
    // Somma
    [HttpGet("Sum")]
    public string Sum()
    {
        try
        {
            string? number1 = Request.Query["number1"];
            string? number2 = Request.Query["number2"];

            float n1, n2;

            if (string.IsNullOrWhiteSpace(number1) && string.IsNullOrWhiteSpace(number2))
                throw new Exception("Expected 2 parameters but none were received. You cannot send an empty request.");
            else if (string.IsNullOrWhiteSpace(number1))
                throw new Exception("First parameter cannot be null or empty.");
            else if (string.IsNullOrWhiteSpace(number2))
                throw new Exception("Second parameter cannot be null or empty.");
            else if (!float.TryParse(number1, out n1) && !float.TryParse(number2, out n2))
                throw new Exception("Both parameters have to be numbers.");
            else if (!float.TryParse(number1, out n1))
                throw new Exception("First parameter has to be a number.");
            else if (!float.TryParse(number2, out n2))
                throw new Exception("Second parameter has to be a number.");
            else
            {
                float result = n1 + n2;
                return $"The result of {n1} plus {n2} is: {result}.";
            }
        }
        catch (Exception e)
        {
            return $"An exception was thrown: {e.Message}";
        }
    }



    // Sottrazione
    [HttpGet("Subtraction")]
    public string Subtraction()
    {
        try
        {
            string? number1 = Request.Query["number1"];
            string? number2 = Request.Query["number2"];

            float n1, n2;

            if (string.IsNullOrWhiteSpace(number1) && string.IsNullOrWhiteSpace(number2))
                throw new Exception("Expected 2 parameters but none were received. You cannot send an empty request.");
            else if (string.IsNullOrWhiteSpace(number1))
                throw new Exception("First parameter cannot be null or empty.");
            else if (string.IsNullOrWhiteSpace(number2))
                throw new Exception("Second parameter cannot be null or empty.");
            if (!float.TryParse(number1, out n1) && !float.TryParse(number2, out n2))
                throw new Exception("Both parameters have to be numbers.");
            else if (!float.TryParse(number1, out n1))
                throw new Exception("First parameter has to be a number.");
            else if (!float.TryParse(number2, out n2))
                throw new Exception("Second parameter has to be a number.");
            else
            {
                float result = n1 - n2;
                return $"The result of {n1} minus {n2} is: {result}.";
            }
        }
        catch (Exception e)
        {
            return $"An exception was thrown: {e.Message}";
        }
    }



    // Moltiplicazione
    [HttpGet("Multiplication/{number1}/{number2}")]
    public string Multiplication(string number1, string number2)
    {
        try
        {
            float n1, n2;

            if (!float.TryParse(number1, out n1) && !float.TryParse(number2, out n2))
                throw new Exception("Both parameters have to be numbers.");
            else if (!float.TryParse(number1, out n1))
                throw new Exception("First parameter has to be a number.");
            else if (!float.TryParse(number2, out n2))
                throw new Exception("Second parameter has to be a number.");
            else
            {
                float result = n1 * n2;
                return $"The result of {n1} times {n2} is: {result}.";
            }
        }
        catch (Exception e)
        {
            return $"An exception was thrown: {e.Message}";
        }
    }



    // Divisione
    [HttpGet("Division/{number1}/{number2}")]
    public string Division(string number1, string number2)
    {
        try
        {
            float n1, n2;

            if (!float.TryParse(number1, out n1) && !float.TryParse(number2, out n2))
                throw new Exception("Both parameters have to be numbers.");
            else if (!float.TryParse(number1, out n1))
                throw new Exception("First parameter has to be a number.");
            else if (!float.TryParse(number2, out n2))
                throw new Exception("Second parameter has to be a number.");
            else if (n2 == 0)
                throw new Exception("Attempted to divide by zero. Enter a non-zero divisor.");
            else
            {
                double exactResult = (double)n1 / (double)n2;
                double integerResult = Math.Floor(n1 / n2);
                double reminder = Math.Ceiling(n1 % n2);

                return $"The exact result of {n1} divided {n2} is: {exactResult}. " +
                    $"The integer result is: {integerResult} and the reminder is: {reminder}.";
            }
        }
        catch (Exception e)
        {
            return $"An exception was thrown: {e.Message}";
        }
    }



    // Elevamento a potenza
    [HttpGet("Exponentiation")]
    public async Task<string> Exponentiation()
    {
        try
        {
            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body))
                throw new Exception("Expected 2 parameters but none were received. You cannot send an empty request.");

            PowBodyData? incomingData = JsonConvert.DeserializeObject<PowBodyData>(body);
            string? number1 = incomingData?.Number1;
            string? number2 = incomingData?.Number2;

            float n1, n2;

            if (!float.TryParse(number1, out n1) && !float.TryParse(number2, out n2))
                throw new Exception("Both parameters have to be numbers.");
            else if (!float.TryParse(number1, out n1))
                throw new Exception("First parameter has to be a number.");
            else if (!float.TryParse(number2, out n2))
                throw new Exception("Second parameter has to be a number.");
            //else if (n1 == Math.Floor(n1) && n1 < 0 && n2 != Math.Floor(n2) || n1 != Math.Floor(n1) && n1 < 0 && n2 != Math.Floor(n2))
            //    throw new Exception("Result is NaN.");
            else
            {
                double result = Math.Pow(n1, n2);

                if (double.IsNaN(result))
                    throw new Exception("With a negative base number and a decimal exponent: Result is NaN.");

                return $"The result of {n1} raised to the power of {n2} is: {result}.";
            }
        }
        catch (Exception e)
        {
            return $"An exception was thrown: {e.Message}";
        }
    }



    // Radice
    [HttpGet("SquareRoot")]
    public async Task<string> SquareRoot()
    {
        try
        {
            string body = await new StreamReader(Request.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body))
                return "Expected 1 parameter but none were received. You cannot send an empty request.";

            SqrtBodyData? incomingData = JsonConvert.DeserializeObject<SqrtBodyData>(body);
            string? number = incomingData?.Number;

            float num;

            if (!float.TryParse(number, out num))
                throw new Exception("The expected parameter has to be a number.");
            else if (num < 0)
                throw new Exception("The only number that has a single square root is zero. There are no negative square number roots.");
            else
            {
                double result = Math.Sqrt(num);
                return $"The square root of {num} is: {result}.";
            }
        }
        catch (Exception e)
        {
            return $"An exception was thrown: {e.Message}";
        }
    }

}
