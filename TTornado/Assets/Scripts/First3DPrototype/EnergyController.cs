using UnityEngine;

public class EnergyController : MonoBehaviour
{
    public float Energy, MaxEnergy;

    [SerializeField]
    private EnergyUIBar energyBar;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        energyBar.SetMaxEnergy(MaxEnergy);
        energyBar.SetEnergy(80);
        Energy = 80;
        ValueManager.IsLoosingHealth = false;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (ValueManager.IsPullingStrongly&&energyBar.Energy>=0)
        {
            energyBar.SetEnergy(energyBar.Energy - 20f * Time.deltaTime);
        }

      

        if (energyBar.Energy <= 0)
        {
            ValueManager.IsLoosingHealth = true;
        }
        else
        {
            ValueManager.IsLoosingHealth = false ;
        }

        if (ValueManager.GainedEnergy)
        {
            energyBar.SetEnergy(energyBar.Energy + 20);
            ValueManager.GainedEnergy = false;
           
        }
    }
}
