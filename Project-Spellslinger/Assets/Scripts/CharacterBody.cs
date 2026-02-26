using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    public CharacterMaster Master;
    public Stats Stats;
    public Placeholders Placeholders;
    
    public CharacterController CharacterController;

    public bool IsSetup = false;

    private void Setup()
    {
        Master = GetComponent<CharacterMaster>();
        Stats = GetComponent<Stats>();
        Placeholders = GetComponent<Placeholders>();

        CharacterController = GetComponent<CharacterController>();

        Master.Setup(this);

        IsSetup = true;
    }

    private void Update()
    {
        if (!IsSetup) Setup();
    }
}
