using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public float DestroyTimer = 3f;
    public Vector3 RandomizeIntensity  = new Vector3(1f,2,3);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, DestroyTimer);

        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            (Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y)), 
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));


    }   
}
