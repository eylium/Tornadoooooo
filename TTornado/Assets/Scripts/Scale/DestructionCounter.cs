using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestructionCounter : MonoBehaviour
{
    private List<Object> _allDestructible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_allDestructible = new List<Object>();
        //Breakable[] allObjects = FindObjectsOfType<Breakable>();

        //foreach (Breakable go in allObjects)
        //    if (go.isActiveAndEnabled)
        //        print(go + " is an active object");

        //GameObject[] activeObjects = FindObjectsOfType<GameObject>();

        //Debug.Log(activeObjects.Length);
    }

    // Update is called once per frame
    void Update()
    {

        //float objectCount = UnityStats.vboTotal;

        //Debug.Log(objectCount);


        //int numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Destructible").Length;

        //Debug.Log(numberOfTaggedObjects);


        //Debug.Log(ValueManager.GameObjectCounter);
    }
}
