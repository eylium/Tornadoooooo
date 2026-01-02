using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class DestructionCounter : MonoBehaviour
{

    /*public int maxDestruction = 50;*/ // set in inspector

    private Slider slider;

    [SerializeField]
    private Image Image;

    void Start()
    {

        slider = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Clamp01((float)ValueManager.DestructionCounter / ValueManager.MaxDestruction);
        if (slider.value == 1)
        {
            ValueManager.GameHasEnded = true;
        }
        //Image.color = Color.Lerp(Color.green, Color.red, slider.value / maxDestruction);
        //if (ValueManager.DestructionCounter > maxDestruction)
        //{
        //}
    }
}
