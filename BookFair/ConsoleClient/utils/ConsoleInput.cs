namespace ConsoleClient.Utils;

internal static class ConsoleInput
{
    public static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string input = Console.ReadLine()?.Trim()
                ?? throw new EndOfStreamException();

            if (input.Length > 0 && !input.Contains('|'))
            {
                return input;
            }

            Console.WriteLine("Enter a non-empty value without '|'.");
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            if (int.TryParse(ReadRequiredText(prompt), out int value))
            {
                return value;
            }

            Console.WriteLine("Enter a valid integer.");
        }
    }
}