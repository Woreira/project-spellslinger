using UnityEngine;
using TMPro;

// simple helper that pulls from LocalizationSystem; no inspector sugar or
// Unity Localization package references.

public class LocalizedBehaviour : MonoBehaviour
{
    public string Key;

    public TMP_Text label;

    private void Awake()
    {
        if (label == null || string.IsNullOrEmpty(Key))
        {
            Debug.LogError($"LocalizedBehaviour on [{gameObject.name}] is missing a reference to its label or has an empty Key.");
            return;
        }

        LocalizationSystem.Register(this);
        UpdateLabel();
    }

    public void UpdateLabel()
    {
        label.text = LocalizationSystem.GetLocalizedString(Key);
    }

    private void OnDestroy()
    {
        LocalizationSystem.Unregister(this);
    }
}