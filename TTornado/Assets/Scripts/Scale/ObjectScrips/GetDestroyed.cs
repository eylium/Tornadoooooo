using System;
using Unity.VisualScripting;
using UnityEngine;

public class GetDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject IgnoreCollision;

    public bool _hasExploded = false;
    [SerializeField]
    private GameObject ExplosionCenter;
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

        //Debug.Log(other.bounds.size.magnitude + other.name);
        //Debug.Log(gameObject.GetComponent<Collider>().bounds.size.magnitude * 0.4f + this.name);



        if (other.bounds.size.magnitude > gameObject.GetComponent<Collider>().bounds.size.magnitude * 0.4f && other.gameObject.layer != 8)
        {
            if (other.gameObject != IgnoreCollision)
            {



                GetComponent<Breakable>().Break();

                if (ExplosionCenter != null)
                {
                    Explosion(ExplosionCenter.transform.position, 100, 20);

                }


                gameObject.SetActive(false);
            }
        }
    }
    private void Explosion(Vector3 center, float radius, float maxforce)
    {
        ValueManager.HasExploded = true;

       
        AudioManager.Instance.PlaySFX("Explosion");
        ParticleManager.Instance.StartParticlesWP("Explosion", transform.position);


        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.attachedRigidbody != null)
            {


                if (hitCollider.GetComponent<Breakable>())
                {
                    hitCollider.GetComponent<Breakable>().Break();
                }

                Rigidbody rigidbody = hitCollider.GetComponent<Rigidbody>();
                Vector3 explosionDirection = hitCollider.transform.position - gameObject.transform.position;

                float distance = explosionDirection.magnitude;
                float intensity = 1 - distance / radius;
                float force = maxforce * intensity;

                rigidbody.AddForce(explosionDirection * force, ForceMode.Impulse);




            }
            
        }
        //ParticleManager.Instance.StopParticles("Explosion");
      
    }


}
