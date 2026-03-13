using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalizationSystem
{
    private static Dictionary<string, string> _localizedStringsByKey;
    public static List<LocalizedBehaviour> RegisteredBehaviours = new List<LocalizedBehaviour>();

    public static void Setup(string csv, string columnId)
    {
        _localizedStringsByKey = new Dictionary<string, string>();
        if (string.IsNullOrEmpty(csv) || string.IsNullOrEmpty(columnId)) return;

        var reader = new StringReader(csv);
        var header = reader.ReadLine();
        if (header == null) return;
        var headers = header.Split(',');
        var columnIndex = -1;
        for (var i = 1; i < headers.Length; i++)    //note(woreira): skip first column since it's reserved for keys
        {
            if (headers[i].Trim() == columnId)
            {
                columnIndex = i;
                break;
            }
        }

        if (columnIndex < 0)
        {
            Debug.LogError($"LocalizationSystem: column [{columnId}] not found in localization CSV.");
            return;
        }

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(',');
            if (parts.Length <= columnIndex) continue;
            var key = parts[0].Trim();
            var value = parts[columnIndex].Trim();
            if (!string.IsNullOrEmpty(key))
            {
                _localizedStringsByKey[key] = value;
            }
        }

        for (var i = 0; i < RegisteredBehaviours.Count; i++)
        {
            RegisteredBehaviours[i].UpdateLabel();
        }
    }

    public static void Setup(TextAsset asset, string columnId)
    {
        Setup(asset.text, columnId);
    }

    public static string GetLocalizedString(string key)
    {
        var value = key;
        _localizedStringsByKey.TryGetValue(key, out value);
        return value;
    }

    public static void Register(LocalizedBehaviour behaviour)
    {
        RegisteredBehaviours.Add(behaviour);
    }

    public static void Unregister(LocalizedBehaviour behaviour)
    {
        RegisteredBehaviours.Remove(behaviour);
    }
}

