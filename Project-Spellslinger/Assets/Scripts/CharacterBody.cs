using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    public CharacterMaster Master;

    public CharacterStats Stats;
    public Placeholders Placeholders;

    public CharacterController CharacterController;

    public bool IsSetup = false;

    private void Setup()
    {
        Master = GetComponent<CharacterMaster>();

        CharacterController = GetComponent<CharacterController>();

        Master.Setup(this);

        IsSetup = true;
    }

    private void Update()
    {
        if (!IsSetup) Setup();
    }
}
