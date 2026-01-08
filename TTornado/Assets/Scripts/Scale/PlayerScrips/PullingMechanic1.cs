
using System.Collections.Generic;
using UnityEngine;



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


    private float _rotationSpeed;



    [SerializeField]
    private float _maxSpeed;

    private float _distanceObjectToCenter;

    private ItemBlockBehaviour _itemBlockBehaviour;

    private float _vibrateTimer;


    private GameObject _object;
  
    private List<GameObject> _list;

    private void Start()
    {
        _list = new List<GameObject>();
    }
    private void FixedUpdate()
    {
        SizeUpPlayer();

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


                    if (_vibrateTimer < 0.5f)
                    {
                        gameObject.GetComponent<ItemBlockBehaviour>().Jitter(gameObject, _target, _maxSpeed, _vibrateTimer);
                    }
                    if (_vibrateTimer > 0.5f)
                    {

                        gameObject.GetComponent<ItemBlockBehaviour>().SetParent(gameObject, _target);
                    }

                    Vector3 direction = gameObject.transform.position - _target.transform.position;

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


                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                    Vector3 direction = gameObject.transform.position - _target.transform.position;


                    if (gameObject.GetComponent<ItemBlockBehaviour>() != null) gameObject.GetComponent<ItemBlockBehaviour>().ThrowObject();

                }
                _list.Clear();
            }

            if (!ValueManager.IsPullingStrongly)
            {
                _vibrateTimer = 0;
            }
        }
    }

    private void SizeUpPlayer()
    {
        ValueManager.SizeCounter = ((float)ValueManager.DestructionCounter / ValueManager.MaxDestruction * 10);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && ValueManager.IsPullingStrongly)
        {

            if (other != null &&
      ValueManager.IsPullingStrongly &&
      !_list.Contains(other.gameObject))
            {
                CheckIfStrongEnough(other);
            }
        }
    }

    public void CheckIfStrongEnough(Collider other)
    {
        if (ValueManager.SizeCounter >= 8)
        {
            AddObjectUnderSize(60, other);
            _rotationSpeed = 8;
           
        }
        else if (ValueManager.SizeCounter >= 6)
        {
            AddObjectUnderSize(40, other);
            _rotationSpeed = 7;
         
        }
        else if (ValueManager.SizeCounter >= 2)
        {
            AddObjectUnderSize(20, other);
            _rotationSpeed = 6;
         
        }
        else if (ValueManager.SizeCounter >= 0)
        {
            AddObjectUnderSize(10, other);
            _rotationSpeed = 4;
           
        }
    }

    private void AddObjectUnderSize(float size, Collider other)
    {

        if (other.bounds.size.magnitude <= size)
        {
            if (other.GetComponent<GetDestroyed>() != null)
            {
                other.GetComponent<GetDestroyed>().SetOffExplosionAndDestruction();

            }

            if (other.gameObject.layer == 3)
            {
                _list.Add(other.gameObject);
            }

            //if (other.GetComponent<ItemBlockBehaviour>()._hasBeenPickedUp == false)
            //{
            //    float addition = other.bounds.size.magnitude;
            //    ValueManager.SizeCounter += addition;


            //}
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
