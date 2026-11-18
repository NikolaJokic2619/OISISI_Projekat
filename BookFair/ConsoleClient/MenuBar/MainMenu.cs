namespace ConsoleClient.MenuBar;

internal class MainMenu
{
    private readonly IReadOnlyList<Menu> _menus;

    public MainMenu(IReadOnlyList<Menu> menus)
    {
        _menus = menus;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Book Fair ---");

            for (int i = 0; i < _menus.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menus[i].Title}");
            }

            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string? input = Console.ReadLine()?.Trim();

            if (input is null or "0")
            {
                return;
            }

            if (int.TryParse(input, out int option) &&
                option >= 1 && option <= _menus.Count)
            {
                _menus[option - 1].RunMenu();
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }
}