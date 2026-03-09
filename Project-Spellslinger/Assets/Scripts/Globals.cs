using UnityEngine;

public static class Globals
{
    public const float GRAVITY = -10f;

    public static SpellCollectionSO SpellCollection;

    public static void Setup()
    {
        SpellCollection = Resources.Load<SpellCollectionSO>("Collections/Spell-Collection");
    }
}