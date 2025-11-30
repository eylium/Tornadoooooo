using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{


    //public Variable
    [SerializeField]
    private float _cameraMovement;


    //[SerializeField]
    //private Material _baseMaterial;

    [SerializeField]
    private GameObject _cameraTarget;


    //Private Variables
    private CharacterController controller;


    private Camera _mainCamera;


    //Movement variables
    public float moveSpeed = 10f;
    public Vector3 velocity;
    public bool isGrounded;
    public float gravitySpecial = -25f;



    void Start()
    {


        //Gameobjects
        controller = GetComponent<CharacterController>();



    }
    private void Awake()
    {
        controller = GetComponent<CharacterController>();



        int playerLayer = LayerMask.NameToLayer("player");
        int suckableLayer = LayerMask.NameToLayer("Suckable");

        Physics.IgnoreLayerCollision(playerLayer, suckableLayer, true);


        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {


        //Movement
        isGrounded = GetComponent<CharacterController>().isGrounded;
        Walking();
        CameraMovement();

        Sucking();

        // Apply gravity
        velocity.y += gravitySpecial * Time.deltaTime;
        if (isGrounded)
        {
            velocity.x = 0f;
            velocity.z = 0f;
            //velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }


    private void Walking()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vector3 moveVetor = new Vector3(horizontal, 0f, vertical);  

        //Vector3 moveVetor = ValueManager.WorldMousePosition;
        //Vector3 pos = Vector3.Lerp(transform.position, moveVetor, Time.deltaTime);

        Vector3 pos = (ValueManager.WorldMousePosition-transform.position).normalized;
        controller.Move((new Vector3(pos.x,0,pos.z) * moveSpeed * Time.deltaTime));




    }



    private void Sucking()
    {
        Color color = Color.magenta;
        if (Input.GetMouseButton(0))
        {
            //_baseMaterial.color = Color.cyan;
            ValueManager.IsPullingStrongly = true;
        }
        else
        {
            //_baseMaterial.color = color;
            ValueManager.IsPullingStrongly = false;
        }

    }
    void CameraMovement()
    {
        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _cameraTarget.transform.position, Time.fixedDeltaTime * _cameraMovement);
    }
}
