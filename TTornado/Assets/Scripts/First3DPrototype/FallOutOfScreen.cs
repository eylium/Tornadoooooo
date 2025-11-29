using UnityEngine;

public class FallOutOfScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ValueManager.IsDead = true;
            Debug.Log("fall out of screen");
        }
    }
}
