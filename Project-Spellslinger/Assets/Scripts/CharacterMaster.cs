using UnityEngine;

public class CharacterMaster : MonoBehaviour
{
    public CharacterBody CharacterBody;

    protected bool IsSetup = false;

    public virtual void Setup(CharacterBody body)
    {
        CharacterBody = body;
    }
}