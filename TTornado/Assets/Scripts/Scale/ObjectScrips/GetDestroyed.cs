using System;
using Unity.VisualScripting;
using UnityEngine;

public class GetDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject IgnoreCollision;

    //[SerializeField]
    //private Collider PlayerDamage;



    public bool _hasExploded = false;
    [SerializeField]
    private GameObject ExplosionCenter;

    private int _objectCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void OnTriggerEnter(Collider other)
    {
        //GameObject pullObject = GameObject.Find("PullHit");
        //int p = pullObject.GetComponent<PullingMechanicOutside>().CheckIfStrongEnough(gameObject.GetComponent<Collider>(), false);


        //if ((ValueManager.SizeCounter >= 3)&& (this.gameObject.GetComponent<Collider>().bounds.size.magnitude>= 60)){
        //    Debug.Log("beep");
        //}


        CheckIfSized(ValueManager.SizeCounter, IgnoreCollision.GetComponent<Collider>().bounds.size.magnitude);
    }

    private void CheckIfSized(float size, float colliderSize)
    {
        if (size >= 8 && colliderSize <= 60)
        {
            SetOffExplosionAndDestruction();
        }
        else if (size >= 6 && colliderSize <= 40)
        {

            SetOffExplosionAndDestruction();
        }
        else if (size >= 2 && colliderSize <= 20)
        {

            SetOffExplosionAndDestruction();
        }
        else if (size >= 0 && colliderSize <= 10)
        {

            SetOffExplosionAndDestruction();
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //Debug.Log(other.bounds.size.magnitude + " " + other.gameObject.name);
    //Debug.Log(gameObject.GetComponent<Collider>().bounds.size.magnitude*0.8f + " " + gameObject.name);

    //Debug.Log(other.bounds.size.magnitude + other.name);
    //Debug.Log(gameObject.GetComponent<Collider>().bounds.size.magnitude * 0.4f + this.name);



    //GetJudegedBySize(other);

    //GetJudgedByAmount(other);

    //    Debug.LogWarning("entered");


    //}


    public void SetOffExplosionAndDestruction()
    {
        //Debug.Log("break");

        GetComponent<Breakable>().Break();
        if (ExplosionCenter != null)
        {

            //Debug.Log(gameObject.name);
            Explosion(ExplosionCenter.transform.position, 100, 20);
            Debug.Log("explode");
            _objectCounter = 0;

        }

        gameObject.SetActive(false);
    }
    private void Explosion(Vector3 center, float radius, float maxforce)
    {
        //bool hasDamaged = false;
        //if (hasDamaged == false)
        //{
        //    Collider[] otherColliders = Physics.OverlapSphere(center, 70);

        //    foreach (Collider collider in otherColliders)
        //    {
        //        if (collider.gameObject.tag == "Player")
        //        {
        //            Debug.Log("beep");

        //            if (ValueManager.DestructionCounter >= 40)
        //            {
        //                ValueManager.DestructionCounter -= 40;
        //            }
        //        }
        //    }

        //    hasDamaged = true;
        //}
        ValueManager.HasExploded = true;

        Debug.Log("explode");
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
                if (rigidbody != null)
                {
                    rigidbody.AddForce(explosionDirection * force, ForceMode.Impulse);
                }




            }

        }
        //ParticleManager.Instance.StopParticles("Explosion");


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (ExplosionCenter != null)
        {
            Gizmos.DrawWireSphere(ExplosionCenter.transform.position, 70);
        }
    }
    //private void GetJudgedByAmount(Collider other)
    //{

    //    //Debug.Log(this.name + " " + this.gameObject.GetComponent<Collider>().bounds.size.magnitude);
    //    //SetOffDestructionBySize(other, 40, 15);


    //    if (this.gameObject.GetComponent<Collider>().bounds.size.magnitude >= 40)
    //    {

    //        SetOffDestructionBySize(other, 40, 50);

    //    }
    //    else if (gameObject.GetComponent<Collider>().bounds.size.magnitude >= 30)
    //    {

    //        SetOffDestructionBySize(other, 30, 10);

    //    }
    //    else if (gameObject.GetComponent<Collider>().bounds.size.magnitude >= 20)
    //    {

    //        SetOffDestructionBySize(other, 20, 3);

    //    }







    //    if (other.gameObject.layer != 8)
    //    {
    //        _objectCounter++;
    //        //Debug.Log(this + " " + _objectCounter);
    //    }
    //}
    //private void SetOffDestructionBySize(Collider other, int boundSize, int objectCounter)
    //{
    //    //int bigBoundSize = boundSize+=20;

    //    //Debug.Log(bigBoundSize);

    //    //Debug.Log(gameObject.GetComponent<Collider>().bounds.size.magnitude);
    //    if (this.gameObject.GetComponent<Collider>().bounds.size.magnitude >= boundSize /*&& (gameObject.GetComponent<Collider>().bounds.size.magnitude <= bigBoundSize*//*)*/)
    //    {

    //        if (other.gameObject != IgnoreCollision)
    //        {

    //            if (_objectCounter >= objectCounter)
    //            {
    //                SetOffExplosionAndDestruction();

    //            }
    //        }
    //    }
    //}

    //private void GetJudegedBySize(Collider other)
    //{
    //    if (other.bounds.size.magnitude > gameObject.GetComponent<Collider>().bounds.size.magnitude * 0.4f && other.gameObject.layer != 8)
    //    {
    //        if (other.gameObject != IgnoreCollision)
    //        {



    //            //GetComponent<Breakable>().Break();

    //            //if (ExplosionCenter != null)
    //            //{
    //            //    Explosion(ExplosionCenter.transform.position, 100, 20);

    //            //}


    //            //gameObject.SetActive(false);

    //            SetOffExplosionAndDestruction();
    //        }
    //    }
    //}



}
