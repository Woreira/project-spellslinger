using UnityEngine;

[System.Serializable]
public struct SpellStatsDefinition
{
    public float Damage;
    public float Speed;
}

[System.Serializable]
public class SpellStats
{
    public SpellStatsDefinition Stats;
}