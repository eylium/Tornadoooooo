using UnityEngine;
using UnityEngine.AI;

public class EnemyWalking : MonoBehaviour
{

    NavMeshAgent agent;
    GameObject _player;

    [SerializeField]
    LayerMask _groundLayer, _playerLayer;

    

    Vector3 _destinationPoint;

    bool _walkPointSet;

    [SerializeField]
    float _walkRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        if (!_walkPointSet)
        {
            SearchForDestination();
        }
        if (_walkPointSet) { agent.SetDestination(_destinationPoint); }

        if (Vector3.Distance(transform.position, _destinationPoint) < 10) _walkPointSet = false;


    }
    void SearchForDestination()
    {
        float z = Random.Range(-_walkRange, _walkRange);
        float x = Random.Range(-_walkRange, _walkRange);

        _destinationPoint = new Vector3(transform.position.x + x, transform.position.y,transform.position.z + z);

        if (Physics.Raycast(_destinationPoint, Vector3.down, _groundLayer))
        {
            _walkPointSet = true;   
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(_destinationPoint, new Vector3(3, 3, 3));
    }
}
