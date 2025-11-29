using UnityEngine;

public class CameaJittering : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ValueManager.IsPullingStrongly)
        {
            float range = Random.Range(-5f*Time.fixedDeltaTime, 5*Time.fixedDeltaTime);

            Vector3 camPos = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(camPos.x, camPos.y+=range, camPos.z);
        }
    }
}
