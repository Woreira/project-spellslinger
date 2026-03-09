using UnityEngine;

[CreateAssetMenu(fileName = "New-Projectile-Spell", menuName = "Spells/Projectile Spell (Base)")]
public class ProjectileSpell : SpellSO
{
    public SpellStats Stats;
    public SpellInstance Prefab;
}