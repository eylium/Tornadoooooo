using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIScript : MonoBehaviour
{


    public float Health, MaxHealth, Widht, Height;

    [SerializeField]
    private RectTransform healthBar;
    private void Start()
    {
        Health = MaxHealth;     
    }

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        Health = health;
        float newWidth = (Health / MaxHealth) * Widht;


        healthBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
