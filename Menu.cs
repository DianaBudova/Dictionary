namespace EDictionary.Model;

public class Menu
{
    private static MenuManager mM;

    public Menu()
    {
        mM = new();
        Main();
    }
    static private void BarMenu()
    {
        Console.WriteLine("\n+------------------MENU------------------+\n" +
            "|                                        |\n" +
            "|  (1) Create/Open the dictionary        |\n" +
            "|  (2) Add the word                      |\n" +
            "|  (3) Change                            |\n" +
            "|  (4) Remove                            |\n" +
            "|  (5) Search the translation            |\n" +
            "|  (6) Save to file                      |\n" +
            "|  (0) Exit                              |\n" +
            "|                                        |\n" +
            "+----------------------------------------+");
    }
    static private void BarContinue()
    {
        Console.WriteLine("\n+----------------------------------------+\n" +
            "|                                        |\n" +
            "|  Enter (1) to continue                 |\n" +
            "|  Enter (0) to exit                     |\n" +
            "|                                        |\n" +
            "+----------------------------------------+");
    }
    static private bool Continues()
    {
        BarContinue();
        return Convert.ToBoolean(MakeDecision(0, 1));
    }
    static private byte MakeDecision(uint smallNum, uint largeNum)
    {
        byte choice;
        Console.WriteLine();
        do
        {
            Console.Write(">>> ");
            choice = byte.Parse(Console.ReadLine());
        } while (choice < smallNum || choice > largeNum);
        return choice;
    }

    public void Main()
    {
        while (true)
        {
            BarMenu();
            switch (MakeDecision(0, 6))
            {
                case 1:
                    Create();
                    break;
                case 2:
                    Add();
                    break;
                case 3:
                    Change();
                    break;
                case 4:
                    Remove();
                    break;
                case 5:
                    Search();
                    break;
                case 6:
                    Save();
                    break;
                case 0:
                    return;
            }
        }
    }
    public void Create()
    {
        if (mM.CreateDictionary())
            Console.WriteLine("\n+----------------------------------------+\n" +
                "|                                        |\n" +
                "|  The dictionary created successfully   |\n" +
                "|                                        |\n" +
                "+----------------------------------------+");
        else
            Console.WriteLine("\nERROR:\n" +
                "--- Perhaps the dictionary is already existing");
    }
    public void Add()
    {
        do
        {
            if (mM.AddWord())
                Console.WriteLine("\n+----------------------------------------+\n" +
                    "|                                        |\n" +
                    "|  The word added successfully           |\n" +
                    "|                                        |\n" +
                    "+----------------------------------------+");
            else
                Console.WriteLine("\nERROR:\n" +
                    "--- Perhaps the dictionary isn't existing\n" +
                    "--- Perhaps the word is already existing\n" +
                    "--- Perhaps the translation(s) not entered\n" +
                    "--- Perhaps the word not saved to repository");
        } while (Continues());
    }
    public void Change()
    {
        do
        {
            Console.WriteLine("\n--- Enter (1) to change the word\n" +
                "--- Enter (2) to change the translation");
            switch (MakeDecision(1, 2))
            {
                case 1:
                    if (mM.ChangeWord())
                        Console.WriteLine("\n+----------------------------------------+\n" +
                            "|                                        |\n" +
                            "|  The word changed successfully         |\n" +
                            "|                                        |\n" +
                            "+----------------------------------------+");
                    else
                        Console.WriteLine("\nERROR:\n" +
                            "--- Perhaps the dictionary isn't existing\n" +
                            "--- Perhaps the word isn't existing");
                    break;
                case 2:
                    if (mM.ChangeTranslation())
                        Console.WriteLine("\n+----------------------------------------+\n" +
                            "|                                        |\n" +
                            "|  The translation changed successfully  |\n" +
                            "|                                        |\n" +
                            "+----------------------------------------+");
                    else
                        Console.WriteLine("\nERROR:\n" +
                            "--- Perhaps the dictionary not created\n" +
                            "--- Perhaps the word isn't existing\n" +
                            "--- Perhaps the translation isn't existing");
                    break;
            }
        } while (Continues());
    }
    public void Remove()
    {
        do
        {
            Console.WriteLine("\n--- Enter (1) to remove the word\n" +
                "--- Enter (2) to remove the translation");
            switch (MakeDecision(1, 2))
            {
                case 1:
                    if (mM.RemoveWord())
                        Console.WriteLine("\n+----------------------------------------+\n" +
                            "|                                        |\n" +
                            "|  The word removed successfully         |\n" +
                            "|                                        |\n" +
                            "+----------------------------------------+");
                    else
                        Console.WriteLine("\nERROR:\n" +
                            "--- Perhaps the dictionary isn't existing\n" +
                            "--- Perhaps the word isn't existing\n" +
                            "--- Perhaps the word not deleted from repository/dictionary");
                    break;
                case 2:
                    if (mM.RemoveTranslation())
                        Console.WriteLine("\n+----------------------------------------+\n" +
                            "|                                        |\n" +
                            "|  The translation removed successfully  |\n" +
                            "|                                        |\n" +
                            "+----------------------------------------+");
                    else
                        Console.WriteLine("\nERROR:\n" +
                            "--- Perhaps the dictionary not created\n" +
                            "--- Perhaps the word isn't existing\n" +
                            "--- Perhaps the translation isn't existing\n" +
                            "--- Perhaps the dictionary has the only one translation\n" +
                            "--- Perhaps the translation not deleted from repository/dictionary");
                    break;
            }
        } while (Continues());
    }
    public void Search()
    {
        do
        {
            if (mM.SearchWord())
                continue;
            else
                Console.WriteLine("\nERROR:\n" +
                    "--- Perhaps the dictionary isn't existing\n" +
                    "--- Perhaps the word isn't existing");
        } while (Continues());
    }
    public void Save()
    {
        do
        {
            if (mM.SaveToFile())
                Console.WriteLine("\n+----------------------------------------+\n" +
                    "|                                        |\n" +
                    "|  The word(s) saved successfully        |\n" +
                    "|                                        |\n" +
                    "+----------------------------------------+");
            else
                Console.WriteLine("\nERROR:\n" +
                    "--- Perhaps the dictionary isn't existing\n" +
                    "--- Perhaps you entered an incorrect name of file\n" +
                    "--- Perhaps you didn't enter word(s)\n" +
                    "--- Perhaps the word(s) not saved to file");
        } while (Continues());
    }
}