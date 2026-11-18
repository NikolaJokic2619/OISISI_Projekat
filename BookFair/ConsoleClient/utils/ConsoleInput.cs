using System.Globalization;

namespace ConsoleClient.Utils;

internal static class ConsoleInput
{
    public static string ReadRequiredText(string prompt)
    {
        return ReadText(prompt, allowEmpty: false);
    }

    public static string ReadText(string prompt, bool allowEmpty)
    {
        while (true)
        {
            string input = ReadLine(prompt);

            if (!allowEmpty && input.Length == 0)
            {
                Console.WriteLine("This field is required.");
                continue;
            }

            if (input.Contains('|'))
            {
                Console.WriteLine("The value cannot contain '|'.");
                continue;
            }

            return input;
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            if (int.TryParse(ReadLine(prompt), out int value))
            {
                return value;
            }

            Console.WriteLine("Enter a valid integer.");
        }
    }

    public static int ReadIntInRange(string prompt, int min, int max = int.MaxValue)
    {
        while (true)
        {
            int value = ReadInt(prompt);
            if (value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine(max == int.MaxValue
                ? $"Enter a value of at least {min}."
                : $"Enter a value between {min} and {max}.");
        }
    }

    public static decimal ReadDecimalInRange(string prompt, decimal min, decimal max = decimal.MaxValue)
    {
        CultureInfo culture = CultureInfo.CurrentCulture;
        const NumberStyles style = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

        while (true)
        {
            if (!decimal.TryParse(ReadLine(prompt), style, culture, out decimal value))
            {
                string separator = culture.NumberFormat.NumberDecimalSeparator;
                Console.WriteLine($"Enter a valid number. Use '{separator}' as the decimal separator.");
                continue;
            }

            if (value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine(max == decimal.MaxValue
                ? $"Enter a value of at least {min}."
                : $"Enter a value between {min} and {max}.");
        }
    }

    public static DateOnly ReadDate(string prompt)
    {
        while (true)
        {
            string input = ReadLine(prompt);
            if (DateOnly.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateOnly date))
            {
                return date;
            }

            Console.WriteLine("Enter a valid date in DD/MM/YYYY format.");
        }
    }

    public static T? SelectRecord<T>(
        string label, string emptyMessage, IReadOnlyList<T> records, Func<T, int> getId, Func<T, string> describe)
        where T : class
    {
        if (records.Count == 0)
        {
            Console.WriteLine(emptyMessage);
            return null;
        }

        foreach (T record in records)
        {
            Console.WriteLine($"ID: {getId(record)} | {describe(record)}");
        }

        while (true)
        {
            int id = ReadInt($"{label} ID (-1 to cancel): ");
            if (id == -1)
            {
                return null;
            }

            T? match = records.FirstOrDefault(record => getId(record) == id);
            if (match is not null)
            {
                return match;
            }

            Console.WriteLine("Choose an ID from the list.");
        }
    }

    private static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input is null)
        {
            throw new EndOfStreamException();
        }

        return input.Trim();
    }
}
