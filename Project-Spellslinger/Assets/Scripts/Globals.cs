using UnityEngine;

public static class Globals
{
    public const float GRAVITY = -10f;

    public static SpellCollectionSO SpellCollection;
    public static LocalizationConfigSO LocalizationConfig;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Setup()
    {
        //todo setup custom log handler

        SpellCollection = Resources.Load<SpellCollectionSO>("Collections/Spell-Collection");
        LocalizationConfig = Resources.Load<LocalizationConfigSO>("Localization/Localization-Config");


        LocalizationSystem.Setup("en");
    }
}