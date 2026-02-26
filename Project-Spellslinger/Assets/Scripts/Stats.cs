using UnityEngine;

[System.Serializable]
public struct StatsDef
{
    public float MovementSpeed;
    public float MovementAccel;
    public float MovementDecel;
    public float AirMovementFactor;
    public Vector3 JumpingVelocity;
}

public class Stats : MonoBehaviour
{
    public StatsDef StatsDef;

}