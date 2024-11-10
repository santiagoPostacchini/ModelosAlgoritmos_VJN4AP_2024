using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LocalizationData
{
    public DictionaryEntry[] entries;
}

[Serializable]
public class DictionaryEntry
{
    public string key;
    public string value;
}

public static class LanguageU
{
    public static Dictionary<Languages, Dictionary<string, string>> LoadTranslate(DataLocalization[] data)
    {
        var tempDic = new Dictionary<Languages, Dictionary<string, string>>();

        for (int i = 0; i < data.Length; i++)
        {
            var tempData = new Dictionary<string, string>();

            foreach (var item in data[i].data)
            {
                string wrappedJson = "{\"entries\":" + item.text + "}";

                var jsonData = JsonUtility.FromJson<LocalizationData>(wrappedJson);

                foreach (var entry in jsonData.entries)
                {
                    tempData.Add(entry.key, entry.value);
                }
            }

            tempDic.Add(data[i].language, tempData);
        }
        return tempDic;
    }
}