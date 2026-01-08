using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    [SerializeField] private Collider _ignoreCollision;
    [SerializeField] private Collider _secondIgnoreCollision;
    [SerializeField] private Collider _playerDamageCollider;
    [SerializeField] private GameObject _itemScript;

    private bool _canDestroy;

    private bool hasExploded;

    private float _destructionTimer;

    //private void FixedUpdate()
    //{
    //    if (_itemScript.GetComponent<ItemBlockBehaviour>().isPickedUp)
    //    {
    //        _destructionTimer += Time.fixedDeltaTime;

    //        if (_destructionTimer >= 1.5f)
    //        {
    //            SetOffExplosion();
    //            _destructionTimer = 0; 
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {

        if (other != null && other.gameObject.layer != 8 & other.gameObject.layer != 6 & other.gameObject.layer != 7 && other != _ignoreCollision && other != _secondIgnoreCollision)
        {
            SetOffExplosion();
        }
    }

    private void SetOffExplosion()
    {

        //bool hasDamaged = false;
        //if (hasDamaged == false)
        //{
        //    Collider[] otherColliders = Physics.OverlapSphere(transform.position, 10);

        //    foreach (Collider collider in otherColliders)
        //    {
        //        if (collider.gameObject.tag == "Player")
        //        {
        //            Debug.Log("beep");

        //            if (ValueManager.DestructionCounter >= 10)
        //            {
        //                ValueManager.DestructionCounter -= 10;
        //            }
        //        }
        //    }

        //    hasDamaged = true;
        //}

















        if (hasExploded == false)
        {
            AudioManager.Instance.sfxSource.volume = 0.01f;
            AudioManager.Instance.PlaySFX("Explosion");
            ParticleManager.Instance.StartParticlesWP("Explosion", transform.position);
            Debug.Log("explodeeeeeeeeeeee");
            AudioManager.Instance.sfxSource.volume = 0.2f;

            hasExploded = true;
        }


        Vector3 center = transform.position;
        float radius = 20f;
        float maxforce = 10f;



        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            //if (hitCollider.GetComponent<ItemBlockBehaviour>() != null)
            //{
            //    if (hitCollider.GetComponent<ItemBlockBehaviour>().isPickedUp)
            //    {

            //        _canDestroy = false;
            //    }
            //    else
            //    {
            //        if (hitCollider != _ignoreCollision)
            //        {
            //            Debug.Log(hitCollider.GetComponent<ItemBlockBehaviour>().isPickedUp + " " + hitCollider.name);
            //            _canDestroy = true;
            //        }
            //    }
            //}



            if (hitCollider.attachedRigidbody != null)
            {


                if (hitCollider.GetComponent<Breakable>() && hitCollider.name != "intact_nuclearPower" && hitCollider.name != "NuclearTrigger")
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
        Destroy(_ignoreCollision);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
