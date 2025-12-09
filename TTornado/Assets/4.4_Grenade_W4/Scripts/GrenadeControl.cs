using UnityEngine;

public class GrenadeControl : MonoBehaviour
{
    [SerializeField]
   private GameObject _prefab;
   
    public GameObject _grenade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randPos = new Vector3(Random.insideUnitSphere.x * 10, 10, Random.insideUnitSphere.z * 10);
            Instantiate(_prefab, _prefab.transform.position, Quaternion.identity);
            _prefab.transform.position = randPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject launchGrenade = Instantiate(_grenade, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            Rigidbody rb = launchGrenade.GetComponent<Rigidbody>();

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 shootDirection = (worldMousePos - Camera.main.transform.position).normalized;

            rb.AddForce(shootDirection * 20, ForceMode.Impulse);
        }
    }
}

