namespace EDictionary.DL;

static public class DictionaryRepository
{
    static DictionaryRepository()
    {
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DirectoryRepository");
    }
    static public bool SaveToFile(string pathToFile, string key, List<string> value)
    {
        File.AppendAllText(pathToFile, ToString(key, value) + "\n");
        return true;
    }
    static public bool RemoveFromFile(string pathToFile, string key)
    {
        if (!File.Exists(pathToFile))
        {
            File.Create(pathToFile).Close();
            return false;
        }
        Dictionary<string, List<string>> words = new();
        words = ReadFromFileTo(pathToFile);
        words.Remove(key);
        File.Delete(pathToFile);
        File.Create(pathToFile).Close();
        foreach (var item in words)
            if (!SaveToFile(pathToFile, item.Key, item.Value))
                return false;
        return true;
    }
    static public Dictionary<string, List<string>> ReadFromFileTo(string pathToFile)
    {
        if (!File.Exists(pathToFile))
            File.Create(pathToFile).Close();
        string[] allWords = File.ReadAllLines(pathToFile);
        Dictionary<string, List<string>> temp = new();
        for (int i = 0; i < allWords.Length; i++)
        {
            string[] currentWord = allWords[i].Split('[', ']');
            string word = "";
            foreach (var item in currentWord)
                word += item;
            currentWord = word.Split(',');
            word = "";
            foreach (var item in currentWord)
                word += item;
            currentWord = word.Split(' ');
            List<string> value = new();
            for (int j = 1; j < currentWord.Length; j++)
                value.Add(currentWord[j]);
            temp.Add(currentWord[0], value);
        }
        return temp;
    }
    static public string ToString(string key, List<string> value)
    {
        string text = $"{key} [";
        for (int i = 0; i < value.Count; i++)
        {
            string str = value[i];
            text += i < value.Count - 1 ? $"{str}, " : $"{str}";
        }
        return text += "]";
    }
}