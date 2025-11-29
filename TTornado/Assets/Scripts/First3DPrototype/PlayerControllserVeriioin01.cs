
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController01 : MonoBehaviour
{





    //public Variable
    [SerializeField]
    private float _cameraMovement;
    [SerializeField]
    private Material _baseMaterial;

    [SerializeField]
    private GameObject _cameraTarget;

    //Private Variables
    private CharacterController controller;


    private Camera _mainCamera;


    //Input actions
    private InputAction _moveAction;
    //private InputAction _jumpAction;
    private InputAction _suckAction;

    //Movement variables
    public float moveSpeed = 10f;
    private float walkingTimer = 0f;
    public Vector3 velocity;
    public bool isGrounded;
    public float gravitySpecial = -25f;



    void Start()
    {

        
        //Gameobjects
        controller = GetComponent<CharacterController>();

        //Inputs
        _moveAction = InputSystem.actions.FindAction("Move");
        //_jumpAction = InputSystem.actions.FindAction("Jump");



    }
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions.FindAction("Move");
        //_jumpAction = InputSystem.actions.FindAction("Jump");



        int playerLayer = LayerMask.NameToLayer("player");
        int suckableLayer = LayerMask.NameToLayer("Suckable");
        int enemyLayer = LayerMask.NameToLayer("enemy");
        int energyLayer = LayerMask.NameToLayer("energy");
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        Physics.IgnoreLayerCollision(playerLayer, suckableLayer, true);
        Physics.IgnoreLayerCollision(playerLayer, energyLayer, true);


        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
   
    }
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
        //Movement
        Vector2 movementInput = _moveAction.ReadValue<Vector2>();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVetor = new Vector3(horizontal, 0f, vertical);
        controller.Move((moveVetor * moveSpeed * Time.deltaTime));

        //Animation
        if (movementInput.magnitude > 0)
        {
            walkingTimer += Time.deltaTime;
        }
        else
        {
            walkingTimer = 0f;
        }

    }



    private void Sucking()
    {
        Color color = Color.magenta;
        if (Input.GetKey(KeyCode.E))
        {
            _baseMaterial.color = Color.cyan;
            ValueManager.IsPullingStrongly = true;
        }
        else
        {
            _baseMaterial.color = color;
            ValueManager.IsPullingStrongly = false;
        }

    }
    void CameraMovement()
    {
        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _cameraTarget.transform.position, Time.fixedDeltaTime * _cameraMovement);
    }


}
