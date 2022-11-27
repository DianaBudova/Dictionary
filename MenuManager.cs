using EDictionary.DL;

namespace EDictionary.Model;

public class MenuManager
{
    private LanguageDictionary mDict;

    private bool EnterNameOfFile(out string nameOfFile)
    {
        Console.WriteLine("\nEnter the name of file");
        Console.Write("\n>>> ");
        nameOfFile = Console.ReadLine();
        return nameOfFile != "" ? true : false;
    }
    private bool EnterWord(out string key)
    {
        Console.WriteLine("\nEnter the word");
        Console.Write("\n>>> ");
        key = Console.ReadLine();
        return key != "" ? true : false;
    }
    private bool EnterWord(ref List<string> key)
    {
        Console.WriteLine("\nEnter the word(s) until you enter 0");
        while (true)
        {
            Console.Write("\n>>> ");
            string word = Console.ReadLine();
            if (word == "0" || word == "")
                break;
            key.Add(word);
        }
        return key.Count < 1 ? false : true;
    }
    private bool EnterTranslation(out string value)
    {
        Console.WriteLine("\nEnter the translation");
        Console.Write("\n>>> ");
        value = Console.ReadLine();
        return value != "" ? true : false;
    }
    private bool EnterTranslation(ref List<string> value)
    {
        Console.WriteLine("\nEnter the translation(s) until you enter 0");
        while (true)
        {
            Console.Write("\n>>> ");
            string word = Console.ReadLine();
            if (word == "0" || word == "")
                break;
            value.Add(word);
        }
        return value.Count < 1 ? false : true;
    }
    private bool ChooseLanguage(out byte language)
    {
        Console.WriteLine();
        for (byte i = 0; i < Languages.languages.Length; i++)
            Console.WriteLine($"--- Enter ({i + 1}) to create {Languages.languages[i]} dictionary");
        Console.WriteLine();
        do
        {
            Console.Write(">>> ");
            language = byte.Parse(Console.ReadLine());
        } while (language < 1 || language > Languages.languages.Length);
        return true;
    }
    private bool SaveToFile(string nameOfFile, List<string> key)
    {
        for (int i = 0; i < key.Count; i++)
            for (int j = 0; j < mDict.Dict.Count; j++)
            {
                if (mDict.Dict.ElementAt(j).Key == key[i])
                    if (!DictionaryRepository.SaveToFile(Directory.GetCurrentDirectory() + "\\DirectoryRepository" + "\\" + nameOfFile + ".txt",
                        mDict.Dict.ElementAt(j).Key,
                        mDict.Dict.ElementAt(j).Value))
                        return false;
            }
        return true;
    }

    public bool CreateDictionary()
    {
        if (mDict != null)
            return false;
        ChooseLanguage(out byte language);
        mDict = new(language);
        return true;
    }
    public bool AddWord()
    {
        if (mDict == null)
            return false;
        if (!EnterWord(out string key))
            return false;
        List<string> value = new();
        if (!EnterTranslation(ref value))
            return false;
        return mDict.Add(key, value);
    }
    public bool ChangeWord()
    {
        if (mDict == null)
            return false;
        if (!EnterWord(out string oldKey))
            return false;
        if (!EnterWord(out string newKey))
            return false;
        return mDict.ChangeKey(oldKey, newKey);
    }
    public bool ChangeTranslation()
    {
        if (mDict == null)
            return false;
        if (!EnterWord(out string key))
            return false;
        if (!EnterTranslation(out string oldValue))
            return false;
        if (!EnterTranslation(out string newValue))
            return false;
        return mDict.ChangeValue(key, oldValue, newValue);
    }
    public bool RemoveWord()
    {
        if (mDict == null)
            return false;
        if (!EnterWord(out string key))
            return false;
        return mDict.RemoveKey(key);
    }
    public bool RemoveTranslation()
    {
        if (mDict == null)
            return false;
        if (!EnterWord(out string key))
            return false;
        if (!EnterTranslation(out string value))
            return false;
        return mDict.RemoveValue(key, value);
    }
    public bool SearchWord()
    {
        if (mDict == null)
            return false;
        if (!EnterWord(out string key))
            return false;
        return mDict.Show(key);
    }
    public bool SaveToFile()
    {
        if (mDict == null)
            return false;
        List<string> key = new();
        if (!EnterNameOfFile(out string nameOfFile))
            return false;
        if (!EnterWord(ref key))
            return false;
        return SaveToFile(nameOfFile, key);
    }
}