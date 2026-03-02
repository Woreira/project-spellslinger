using UnityEngine;

[System.Serializable]
public struct CharacterStatsDefinition
{
    public float MovementSpeed;
    public float MovementAccel;
    public float MovementDecel;
    public float AirMovementFactor;
    public Vector3 JumpingVelocity;
}

[System.Serializable]
public class CharacterStats
{
    public CharacterStatsDefinition Stats;
}