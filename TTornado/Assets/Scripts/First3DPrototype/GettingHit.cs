using UnityEngine;
using UnityEngine.SceneManagement;

public class GettingHit : MonoBehaviour
{
    public float Health, MaxHealth;

    //private bool _isDead;

    [SerializeField]
    private HealthBarUIScript healthBar;

    private bool _isDead;

    [SerializeField]
    private Canvas _endScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _endScreen.enabled = false;
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(80);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!_isDead && healthBar.Health <= 0)
        {
            _isDead = true;
            _endScreen.enabled = true;
        }

        if (ValueManager.IsDead)
        {
            _isDead = true;
            _endScreen.enabled = true;
        }


        if (_isDead && Input.GetKeyDown(KeyCode.R))
        {
            healthBar.SetHealth(80);
            _isDead = false;
            ValueManager.IsDead = false;
            SceneManager.LoadScene(0);
        }


        if (ValueManager.IsLoosingHealth)
        {
            healthBar.SetHealth(healthBar.Health - 10f * Time.deltaTime);
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.gameObject.layer == 8)
        {
            SetHealth(-1);
        }


    }



    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
}
