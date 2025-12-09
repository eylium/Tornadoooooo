using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;


    [SerializeField]
    private Particles[] particleSystem;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartParticlesWP(string name, Vector3 position)
    {

        Particles s = Array.Find(particleSystem, x => x.Name == name);
       

        if (s == null)
        {
            Debug.Log("particles not found");
        }
        else
        {
            s.system.transform.position = position;
            s.system.gameObject.SetActive(true);



            s.system.Play();
        }
    }   
    public void StartParticles(string name)
    {

        Particles s = Array.Find(particleSystem, x => x.Name == name);
       

        if (s == null)
        {
            Debug.Log("particles not found");
        }
        else
        {

            s.system.gameObject.SetActive(true);



            s.system.Play();
        }
    }
    public void StopParticles(string name)
    {
        Particles s = Array.Find(particleSystem, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("particles not found");
        }
        else
        {
            s.system.Stop();
            s.system.gameObject.SetActive(false);

        }
    }
}
