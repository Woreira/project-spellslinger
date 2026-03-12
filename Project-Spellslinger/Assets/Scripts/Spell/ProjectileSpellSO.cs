using UnityEngine;

[CreateAssetMenu(fileName = "New-Projectile-Spell", menuName = "Spells/Projectile Spell (Base)")]
public class ProjectileSpellSO : SpellSO
{
    public SpellStats Stats;
    public SpellInstance Prefab;
}