
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Pulling : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField]
    private GameObject _directionObject;

    private bool isScaling;
    private bool hasScaled;

    private int _counter;
    //private bool isScaling;
    //private bool hasScaled;


    [SerializeField]
    private GameObject _target;


    [SerializeField]
    private GameObject _innerTarget;


    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _shrinkingSpeed = 0.05f;



    [SerializeField]
    private float _maxSpeed;

    private float _distanceObjectToCenter;



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
        GetBigger();
        GetSmaller();

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
                    Vector3 connection = _target.transform.position - gameObject.transform.position;
                    gameObject.transform.right = connection;


                    if (_vibrateTimer < 0.4f)
                    {
                        gameObject.GetComponent<ItemBlockkBig>().Jitter(gameObject, _target, _maxSpeed, _vibrateTimer);
                    }
                    if (_vibrateTimer > 0.4f)
                    {

                        gameObject.GetComponent<ItemBlockkBig>().Move(gameObject, _target, _maxSpeed, _vibrateTimer);
                    }

                    Vector3 direction = gameObject.transform.position - _target.transform.position;

                    if (direction.magnitude <= 9)
                    {

                        gameObject.GetComponent<ItemBlockkBig>().StartScale(gameObject);

                    }
                    if (direction.magnitude <= 1)
                    {

                        int suckableLayer = LayerMask.NameToLayer("Suckable");

                        Physics.IgnoreLayerCollision(suckableLayer, suckableLayer, true);
                        gameObject.GetComponent<MeshRenderer>().enabled = false;

                      
                    }
                    if (direction.magnitude <=2)
                    {
                        _counter = gameObject.GetComponent<ItemBlockkBig>().AddCounter(_counter);

                    }




                }
            }


        }
        else if (_list.Count != 0)
        {
            foreach (GameObject gameObject in _list)
            {

                //gameObject?.transform.SetParent(null, true);



                //gameObject.GetComponent<ItemBlockBehaviour>().ReverseScale(gameObject);


                Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                Vector3 direction = gameObject.transform.position - _target.transform.position;

                if (direction.magnitude <= 1f)
                {

                    //rb.linearVelocity = Vector3.zero;
                    //rb.angularVelocity = Vector3.zero;

                    //rb.AddForce(new Vector3(0, 1 * _forceMultiplyer, 0), ForceMode.Impulse);
                    //gameObject.GetComponent<ItemBlockBehaviour>().ReverseScale(gameObject);
                }

                if (direction.magnitude >= 1f)
                {
                    //int suckableLayer = LayerMask.NameToLayer("Suckable");
                    //Physics.IgnoreLayerCollision(suckableLayer, suckableLayer, false);
                    gameObject.GetComponent<ItemBlockkBig>().ThrowObject();
                }

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
    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.gameObject.layer == 3 && ValueManager.IsPullingStrongly)
        {
            _list.Add(other.gameObject);
        }


    }


    private void EnableParticles()
    {
        ParticleSystem s = (ParticleSystem)FindAnyObjectByType(typeof(ParticleSystem));
        if (ValueManager.IsPullingStrongly)
        {
            s.Play();
        }
        else
        {
            s.Stop();
        }
    }




    private void GetSmaller()
    {
       
        Vector3 scale = _player.transform.localScale;
        _player.transform.localScale = new Vector3(scale.x -= _shrinkingSpeed * Time.fixedDeltaTime, scale.y -= _shrinkingSpeed * Time.fixedDeltaTime, scale.z -= _shrinkingSpeed * Time.fixedDeltaTime);
    }
    private void GetBigger()
    {

        if (_counter - 1 <= 0) return;


        Vector3 scale = _player.transform.localScale;
        Vector3 newScale = _player.transform.localScale *= 1.15f;

        //_player.transform.localScale = new Vector3(scale.x += _shrinkingSpeed * Time.fixedDeltaTime, scale.y += _shrinkingSpeed * Time.fixedDeltaTime, scale.z += _shrinkingSpeed * Time.fixedDeltaTime);

        if (!hasScaled && !isScaling)
        {
            StartCoroutine(ScaleOnceBack(scale,newScale));
        }

        _counter--;

    }

    private IEnumerator ScaleOnceBack(Vector3 start, Vector3 newS)
    {

       
        isScaling = true;
        hasScaled = true;

        Vector3 startScale = start;
        Vector3 newScale = newS;

        float duration = 3f; // how long the scaling should take
        float elapsed = 0f;



        while (elapsed < duration)
        {
            //float t = elapsed / duration; // normalized time [0, 1]

            float t = elapsed / duration;
          
            _player.transform.localScale = Vector3.Lerp(startScale, newScale, t);


            //go.transform.localScale = Vector3.Lerp(startScale, newScale, t);
            elapsed += Time.deltaTime;
            yield return null; // wait for next frame
        }

        // Ensure it ends exactly on the target scale
        _player.transform.localScale = newScale;

        isScaling = false;

    }


}
