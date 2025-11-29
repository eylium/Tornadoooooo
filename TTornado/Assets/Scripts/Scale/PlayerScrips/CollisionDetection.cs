using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CollisionDetection : MonoBehaviour
{


    [SerializeField]
    private GameObject _directionObject;


    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<Rigidbody>() != null)
        {


            //if (other.gameObject != null)
            //{
            //    //other.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);
            //    Debug.Log(other);
            //    float distance = (_directionObject.transform.position - other.transform.position).magnitude * (1/_forceMultiplyer);

            //    //Vector3 directionVector = (transform.parent.transform.position - other.transform.position);
            //    Vector3 directionVector = (transform.parent.transform.position - other.transform.position);

            //    Debug.Log(directionVector);

            //    other.GetComponent<Rigidbody>().AddForce((directionVector.normalized * (1 / distance)), ForceMode.VelocityChange);

            //}   


            if (other.gameObject != null && ValueManager.IsPullingStrongly)
            {
                DoSuking(140, other);
                if (other.gameObject.layer == 10)
                {
                    Destroy(other.gameObject);
                    ValueManager.GainedEnergy = true;
                }
            }
            else if (other.gameObject != null)
            {
                DoSuking(20, other);
            }



        }


    }

    private void DoSuking(float forceMultiplyer, Collider other)
    {
        //other.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);
        //Debug.Log(other);
        float distance = (_directionObject.transform.position - other.transform.position).magnitude * (1 / forceMultiplyer);

        //Vector3 directionVector = (transform.parent.transform.position - other.transform.position);
        Vector3 directionVector = (transform.parent.transform.position - other.transform.position);

        //Debug.Log(directionVector.normalized * (1 / distance));

        other.GetComponent<Rigidbody>().AddForce((directionVector.normalized * (1 / distance)), ForceMode.Acceleration);


    }



}
