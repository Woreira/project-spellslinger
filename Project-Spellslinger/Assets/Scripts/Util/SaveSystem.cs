using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    public static Dictionary<string, string> Entries;
    private const string PLAYER_PREFS_ENTRIES_KEY = "Entries";

    [System.Serializable]
    private class DictionaryWrapper
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        public DictionaryWrapper(Dictionary<string, string> dictionary)
        {
            foreach (var pair in dictionary)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            var dict = new Dictionary<string, string>();
            Debug.Assert(keys.Count == values.Count);
            for (int i = 0; i < keys.Count; i++)
            {
                dict[keys[i]] = values[i];
            }
            return dict;
        }
    }

    public static void SaveAllEntries()
    {
        var wrapper = new DictionaryWrapper(Entries);
        var entriesAsJson = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(PLAYER_PREFS_ENTRIES_KEY, entriesAsJson);
        PlayerPrefs.Save();
    }

    public static void SetInt(string key, int value)
    {
        SetString(key, value.ToString());
    }

    public static void SetFloat(string key, float value)
    {
        SetString(key, value.ToString());
    }

    public static void SetBool(string key, bool value)
    {
        SetString(key, value.ToString());
    }

    public static void SetString(string key, string value)
    {
        Entries[key] = value;
    }

    public static void SetObject<T>(string key, T obj)
    {
        string json = JsonUtility.ToJson(obj);
        Entries[key] = json;
    }

    public static void LoadToEntries()
    {
        if (!PlayerPrefs.HasKey(PLAYER_PREFS_ENTRIES_KEY))
        {
            Entries = new Dictionary<string, string>();
            return;
        }

        string json = PlayerPrefs.GetString(PLAYER_PREFS_ENTRIES_KEY);

        if (string.IsNullOrEmpty(json))
        {
            Entries = new Dictionary<string, string>();
            return;
        }

        var wrapper = JsonUtility.FromJson<DictionaryWrapper>(json);

        if (wrapper == null)
        {
            Entries = new Dictionary<string, string>();
            return;
        }

        Entries = wrapper.ToDictionary();
    }

    public static bool HasKey(string key)
    {
        return Entries != null && Entries.ContainsKey(key);
    }

    public static T GetObject<T>(string key, T fallback)
    {
        if (!Entries.TryGetValue(key, out var json)) return fallback;
        return JsonUtility.FromJson<T>(json);
    }
}