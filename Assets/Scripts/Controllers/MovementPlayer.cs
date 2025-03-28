using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator animator; // Agregado para animaciones
    private Vector3 moveDirection;
    public bool isGrounded;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Obtener el Animator adjunto

    }

    void Update()
    {
        HandleInput();

        if (moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
        }

        // Agregado para animaciones Se llaman aqui para que se actualicen en cada frame.
        //Aca  camina 
        if (!Input.GetKey("left shift") && moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
            animator.SetBool("isWalking", true);
        }
        //aca corre
        else if (Input.GetKey("left shift") && moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
            animator.SetBool("isRunning", true);
            moveSpeed = 7f;
        }
        //si no se esta moviendo 
        else if (moveDirection == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            moveSpeed = 5f;
        }


        //Aca Meele  input fire si  solo ataca y esta en idle
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("isMeele");
        }
        else
        {
            animator.SetBool("isMeele", false);
        }

        // Aca  si    fire1 animaciopn  pistol idle
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("isPistolIdle");
        }
        else
        {
            animator.SetBool("isPistolIdle", false);
        }

        // //  aca si  camina  y  ataca  fire1 no funciono funciono lo de arriba
        // if (Input.GetButtonDown("Fire1") && moveDirection != Vector3.zero)
        // {
        //     animator.SetBool("isWalking", true);
        //     animator.SetTrigger("isMeele");
        // }
        // else
        // {
        //     animator.SetBool("isWalking", false);
        //     animator.SetBool("isMeele", false);
        // }
        // //Aca si corre y ataca fire1
        // if (Input.GetButtonDown("Fire1") && Input.GetKey("left shift") && moveDirection != Vector3.zero)
        // {
        //     animator.SetBool("isRunning", true);
        //     animator.SetTrigger("isMeele");
        // }
        // else
        // {
        //     animator.SetBool("isRunning", false);
        //     animator.SetBool("isMeele", false);
        // }

    }
        void FixedUpdate()
        {
            ApplyPhysicsMovement();
        }


    private void HandleInput()
    {
        // Entrada básica de movimiento
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Salto si oprimimos Boton Jump (input manager) y está tocando el suelo.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
        }
    }
    private void ApplyPhysicsMovement()
    {
        // Movimiento con fuerzas físicas por medio de método MovePosition
        playerRigidbody.MovePosition(transform.localPosition + moveDirection * moveSpeed * Time.fixedDeltaTime);

    }
    private void ApplyJump()
    {
        //Usamos método AddForce en Rigidbodycpara aplicar una fuerza vertical con modo de Impulso
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    //Esto nos comunica cuando colisiona con un objeto que tenga Tag "Floor"
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
    //Esto nos comunica cuando deja de colisionar con un objeto que tenga Tag "Floor"
    private void OnCollisionExit(Collision other)
    {

        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
    private void RotateTowardsMovement()
    {
        // Rotación suave hacia la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}

