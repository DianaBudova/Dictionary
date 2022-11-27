using EDictionary.DL;

namespace EDictionary.Model;

public class LanguageDictionary
{
    public Dictionary<string, List<string>>? Dict { get; }
    private readonly string language;
    private readonly string pathToFile;

    public LanguageDictionary(byte language)
    {
        this.language = Languages.languages[language - 1];
        pathToFile = Directory.GetCurrentDirectory() + "\\DirectoryRepository" + "\\" + $"{this.language}Dictionary" + ".txt";
        Dict = DictionaryRepository.ReadFromFileTo(pathToFile);
    }
    public bool Add(string key, List<string> value)
    {
        if (Dict.ContainsKey(key) || value.Count == 0)
            return false;
        Dict.Add(key, value);
        return DictionaryRepository.SaveToFile(pathToFile, key, value);
    }
    public bool RemoveKey(string key)
    {
        if (!Dict.ContainsKey(key))
            return false;
        if (!DictionaryRepository.RemoveFromFile(pathToFile, key))
            return false;
        return Dict.Remove(key);
    }
    public bool RemoveValue(string key, string value)
    {
        if (!Dict.ContainsKey(key))
            return false;
        if (!Dict[key].Contains(value))
            return false;
        if (Dict[key].Count <= 1)
            return false;
        if (!DictionaryRepository.RemoveFromFile(pathToFile, key))
            return false;
        if (!Dict[key].Remove(value))
            return false;
        return DictionaryRepository.SaveToFile(pathToFile, key, Dict[key]);
    }
    public bool ChangeKey(string oldKey, string newKey)
    {
        if (!Dict.ContainsKey(oldKey))
            return false;
        if (!DictionaryRepository.RemoveFromFile(pathToFile, oldKey))
            return false;
        List<string> oldValue = Dict[oldKey];
        Dict.Remove(oldKey);
        Dict.Add(newKey, oldValue);
        return DictionaryRepository.SaveToFile(pathToFile, newKey, Dict[newKey]);
    }
    public bool ChangeValue(string key, string oldValue, string newValue)
    {
        if (!Dict.ContainsKey(key))
            return false;
        if (!Dict[key].Contains(oldValue))
            return false;
        if (!DictionaryRepository.RemoveFromFile(pathToFile, key))
            return false;
        Dict[key].Remove(oldValue);
        Dict[key].Add(newValue);
        return DictionaryRepository.SaveToFile(pathToFile, key, Dict[key]);
    }
    public bool Show(string key)
    {
        if (!Dict.ContainsKey(key))
            return false;
        Console.WriteLine($"\n+----------------{language}-----------------+\n" +
            "|\n" +
            $"|  {DictionaryRepository.ToString(key, Dict[key])}\n" +
            "|\n" +
            "+----------------------------------------+");
        return true;
    }
}