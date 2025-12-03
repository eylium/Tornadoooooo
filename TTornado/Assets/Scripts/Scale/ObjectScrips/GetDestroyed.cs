using Unity.VisualScripting;
using UnityEngine;

public class GetDestroyed : MonoBehaviour
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
        //Debug.Log(other.bounds.size.magnitude + " " + other.gameObject.name);
        //Debug.Log(gameObject.GetComponent<Collider>().bounds.size.magnitude*0.8f + " " + gameObject.name);

        Debug.Log(other.bounds.size.magnitude + "other");
        Debug.Log(gameObject.GetComponent<Collider>().bounds.size.magnitude * 0.5f);
        if (other.bounds.size.magnitude > gameObject.GetComponent<Collider>().bounds.size.magnitude * 0.5f&& other.gameObject.layer!=8)
        {
            GetComponent<Breakable>().Break();
        }
    }
}
