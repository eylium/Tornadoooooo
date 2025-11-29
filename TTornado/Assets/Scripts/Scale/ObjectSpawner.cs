using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] GameObject _cube;
    [SerializeField] GameObject _sphere;


    private float _timer;
    private float _sphereTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        _sphereTimer += Time.fixedDeltaTime;

        if (_timer >= Random.Range(1, 3))
        {
            Vector3 someRandomPoint = new  Vector3(0, 20, 0) + Random.insideUnitSphere * 10;
           
            Instantiate(_cube, someRandomPoint, Quaternion.identity);
            _timer -= 2;
        }

        if (_sphereTimer >= Random.Range(1, 3))
        {
            Vector3 someRandomPoint = new  Vector3(0, 20, 0) + Random.insideUnitSphere * 10;
           
            Instantiate(_sphere, someRandomPoint, Quaternion.identity);
            _sphereTimer -= 2;
        }
    }
}
