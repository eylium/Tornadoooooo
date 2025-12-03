using System;
using UnityEngine;

[SelectionBase]
public class Breakable : MonoBehaviour
{

    [SerializeField] private GameObject _intact;
    [SerializeField] private GameObject _broken;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _intact.SetActive(true);
        _broken.SetActive(false);

        //Break();
    }





    public void Break()
    {
        _intact.SetActive(false);
        _broken.SetActive(true);

        
    }
}
