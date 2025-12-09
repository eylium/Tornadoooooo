
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PullingMechanicOutside : MonoBehaviour
{
    //[SerializeField]
    //private float _forceMultiplyer = 1.0f;

    [SerializeField]
    private GameObject _directionObject;


    private float _explosionTimer;

    [SerializeField]
    private GameObject _target;


    [SerializeField]
    private GameObject _innerTarget;

    private GameObject _player;

    [SerializeField]
    private float _rotationSpeed;



    [SerializeField]
    private float _maxSpeed;

    private float _distanceObjectToCenter;

    private ItemBlockBehaviour _itemBlockBehaviour;

    private float _vibrateTimer;


    private GameObject _object;
    //private GameObject[] _objectArray;

    private List<GameObject> _list;

    private void Start()
    {
        _list = new List<GameObject>();

    }
    private void FixedUpdate()
    {



        if (ValueManager.HasExploded)
        {
            _explosionTimer += Time.deltaTime;




            ValueManager.IsPullingStrongly = false;

            if (_explosionTimer > 2)
            {


                ValueManager.IsPullingStrongly = true;
                ValueManager.HasExploded = false;
                _list.Clear();
                _explosionTimer = 0;
            }
        }

        EnableParticles();
        //transform.position = Vector3.MoveTowards(transform.position,new Vector3(0,0,0), _maxSpeed);
        //
        if (ValueManager.IsPullingStrongly)
        {
            _vibrateTimer += Time.fixedDeltaTime;
            _target.transform.Rotate(new Vector3(0, _rotationSpeed, 0));
            if (_list.Count != 0)
            {
                foreach (GameObject gameObject in _list)
                {

                    gameObject.GetComponent<ItemBlockBehaviour>()._cantBeThrown = false;

                    Vector3 connection = _target.transform.position - gameObject.transform.position;
                    gameObject.transform.right = connection;


                    if (_vibrateTimer < 0.4f)
                    {
                        gameObject.GetComponent<ItemBlockBehaviour>().Jitter(gameObject, _target, _maxSpeed, _vibrateTimer);
                    }
                    if (_vibrateTimer > 0.4f)
                    {

                        //gameObject.GetComponent<ItemBlockBehaviour>().Move(gameObject, _target, _maxSpeed, _vibrateTimer);                        
                        gameObject.GetComponent<ItemBlockBehaviour>().SetParent(gameObject, _target);
                    }

                    Vector3 direction = gameObject.transform.position - _target.transform.position;

                    //if (direction.magnitude <= 3)
                    //{

                    //    gameObject.GetComponent<ItemBlockBehaviour>().StartScale(gameObject);

                    //    //gameObject.transform.localScale *= 0.9f;
                    //}
                    //if (direction.magnitude <= 1)
                    //{

                    //    int suckableLayer = LayerMask.NameToLayer("Suckable");

                    //    Physics.IgnoreLayerCollision(suckableLayer, suckableLayer, true);
                    //    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //}
                }
            }


        }
        else if (_list.Count != 0)
        {
            {
                foreach (GameObject gameObject in _list)
                {

                    gameObject?.transform.SetParent(null, true);
                    int suckableLayer = LayerMask.NameToLayer("Suckable");
                    Physics.IgnoreLayerCollision(suckableLayer, suckableLayer, false);


                    //gameObject.GetComponent<ItemBlockBehaviour>().ReverseScale(gameObject);


                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                    Vector3 direction = gameObject.transform.position - _target.transform.position;

                    //if (direction.magnitude <= 1f)
                    //{
                    //    rb.linearVelocity = Vector3.zero;
                    //    rb.angularVelocity = Vector3.zero;

                    //    rb.AddForce(new Vector3(0, 1 * _forceMultiplyer, 0), ForceMode.Impulse);
                    //}

                    //if (direction.magnitude >= 1f)
                    //{
                    //gameObject.GetComponent<ItemBlockBehaviour>().ReverseScale(gameObject); gameObject.GetComponent<ItemBlockBehaviour>().ReverseScale(gameObject);



                    gameObject.GetComponent<ItemBlockBehaviour>().ThrowObject();

                    //}

                    //gameObject.GetComponent<MeshRenderer>().enabled = true;


                }
                _list.Clear();
            }

            if (!ValueManager.IsPullingStrongly)
            {
                _vibrateTimer = 0;
            }


            //transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), Time.deltaTime * _rotationSpeed);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.gameObject.layer == 3 && ValueManager.IsPullingStrongly)
        {
            _list.Add(other.gameObject);
        }


    }


    private void EnableParticles()
    {

        if (ValueManager.IsPullingStrongly)
        {
            ParticleManager.Instance.StartParticles("Suction");
        }
        else
        {
            ParticleManager.Instance.StopParticles("Suction");
        }
    }



    public static float InExpo(float t) => (float)Mathf.Pow(2, 10 * (t - 1));





}
