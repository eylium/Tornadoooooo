using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Grenade : MonoBehaviour
{
    private float _timer;
    
   
  
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 3)
        {
            Explosion(this.transform.position,5,50);
            _timer -= 3;
        }

    }

    private void Explosion(Vector3 center, float radius,float maxforce)
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Cube")
            {
                Rigidbody rigidbody = hitCollider.GetComponent<Rigidbody>();
                Vector3 explosionDirection = hitCollider.transform.position - gameObject.transform.position;
                
                float distance = explosionDirection.magnitude;
                float intensity = 1 - distance/radius;
                float force = maxforce * intensity;
                
                rigidbody.AddForce(explosionDirection*force,ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
