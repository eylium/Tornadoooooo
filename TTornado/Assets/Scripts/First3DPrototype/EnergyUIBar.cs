using UnityEngine;

public class EnergyUIBar : MonoBehaviour
{

    public float Energy, MaxEnergy, Widht, Height;

    [SerializeField]
    private RectTransform energyBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Energy = MaxEnergy;
    }

    public void SetMaxEnergy(float maxEnergy)
    {
        MaxEnergy = maxEnergy;
    }
    public void SetEnergy(float energy)
    {
        Energy = energy;
        float newWidth = (Energy / MaxEnergy) * Widht;


        energyBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
