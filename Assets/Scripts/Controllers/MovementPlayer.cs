// using UnityEngine;

// public class MovementPlayer : MonoBehaviour
// {
//     [Header("Configuración")]
//     private float horizontal, vertical;
//     public float moveSpeed = 5f;
//     public float rotationSpeed = 10f;
//     public float jumpForce = 7f;
//     [SerializeField] private Rigidbody playerRigidbody;
//     private Vector3 moveDirection;
//     private Animator animator;
//     public bool isGrounded;

//     void Start()
//     {
//         playerRigidbody = GetComponent<Rigidbody>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         HandleInput();
//         RotateTowardsMovement();
//         UpdateAnimationStates(); // Actualizar animaciones aquí
//     }

//     void FixedUpdate()
//     {
//         ApplyPhysicsMovement();
//     }

//     private void HandleInput()
//     {
//         horizontal = Input.GetAxis("Horizontal");
//         vertical = Input.GetAxis("Vertical");
//         moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             ApplyJump();
//         }
//     }

//     private void ApplyPhysicsMovement()
//     {
//         Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
//         playerRigidbody.MovePosition(newPosition);
//     }

//     private void ApplyJump()
//     {
//         // 🔹 Corregido: Se usa "velocity" en lugar de "linearVelocity"
//         playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
//         playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         isGrounded = false;
//     }

//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = true;
//         }
//     }

//     private void OnCollisionExit(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = false;
//         }
//     }

//     private void RotateTowardsMovement()
//     {
//         if (moveDirection != Vector3.zero)
//         {
//             Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//     }

//     private void UpdateAnimationStates()
//     {
//         bool isMoving = moveDirection.magnitude > 0;

//         // i el jugador se está moviendo y mantiene Shift, está corriendo.
//         bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

//         // Si se está moviendo pero no presiona Shift, está caminando.
//         bool isWalking = isMoving && !isRunning;

//         //  Enviar los estados al Animator
//         animator.SetBool("isWalking", isWalking);
//         animator.SetBool("isRunning", isRunning);
//     }
// }


//Funciona este pero no el atacck

// using UnityEngine;

// public class MovementPlayer : MonoBehaviour
// {
//     [Header("Configuración")]
//     private float horizontal, vertical;
//     public float moveSpeed = 5f;
//     public float rotationSpeed = 10f;
//     public float jumpForce = 7f;
//     [SerializeField] private Rigidbody playerRigidbody;
//     private Vector3 moveDirection;
//     private Animator animator;
//     public bool isGrounded;
//     private bool isAttacking = false; // 🔹 Variable para evitar ataques continuos

//     void Start()
//     {
//         playerRigidbody = GetComponent<Rigidbody>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         HandleInput();
//         RotateTowardsMovement();
//         UpdateAnimationStates();
//         HandleAttack(); // 🔹 Llamar la función de ataque en Update
//     }

//     void FixedUpdate()
//     {
//         ApplyPhysicsMovement();
//     }

//     private void HandleInput()
//     {
//         horizontal = Input.GetAxis("Horizontal");
//         vertical = Input.GetAxis("Vertical");
//         moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             ApplyJump();
//         }
//     }

//     private void ApplyPhysicsMovement()
//     {
//         Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
//         playerRigidbody.MovePosition(newPosition);
//     }

//     private void ApplyJump()
//     {
//         playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
//         playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         isGrounded = false;
//     }

//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = true;
//         }
//     }

//     private void OnCollisionExit(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = false;
//         }
//     }

//     private void RotateTowardsMovement()
//     {
//         if (moveDirection != Vector3.zero)
//         {
//             Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//     }

//     private void UpdateAnimationStates()
//     {
//         bool isMoving = moveDirection.magnitude > 0;
//         bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
//         bool isWalking = isMoving && !isRunning;

//         animator.SetBool("isWalking", isWalking);
//         animator.SetBool("isRunning", isRunning);
//     }

//     private void HandleAttack()
//     {
//         // Solo puede atacar si está caminando o corriendo
//         if ((animator.GetBool("isWalking") || animator.GetBool("isRunning")) && Input.GetButtonDown("Fire1") && !isAttacking)
//         {
//             isAttacking = true;
//             animator.SetTrigger("isAttacking"); //  Activar la animación de ataque
//             Invoke("ResetAttack", 0.5f); // Controlar la duración del ataque
//         }
//     }

//     private void ResetAttack()
//     {
//         isAttacking = false;
//     }
// }

//mas o menso funcional la meeele pero malita
// using UnityEngine;

// public class MovementPlayer : MonoBehaviour
// {
//     [Header("Configuración")]
//     private float horizontal, vertical;
//     public float moveSpeed = 5f;
//     public float rotationSpeed = 10f;
//     public float jumpForce = 7f;
//     [SerializeField] private Rigidbody playerRigidbody;
//     private Vector3 moveDirection;
//     private Animator animator;
//     public bool isGrounded;
//     private bool isAttacking = false; // Para evitar ataques continuos

//     void Start()
//     {
//         playerRigidbody = GetComponent<Rigidbody>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         HandleInput();
//         RotateTowardsMovement();
//         UpdateAnimationStates();
//         HandleAttack(); // Llamar la función de ataque en Update
//     }

//     void FixedUpdate()
//     {
//         ApplyPhysicsMovement();
//     }

//     private void HandleInput()
//     {
//         horizontal = Input.GetAxis("Horizontal");
//         vertical = Input.GetAxis("Vertical");
//         moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             ApplyJump();
//         }
//     }

//     private void ApplyPhysicsMovement()
//     {
//         Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
//         playerRigidbody.MovePosition(newPosition);
//     }

//     private void ApplyJump()
//     {
//         playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
//         playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         isGrounded = false;
//     }

//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = true;
//         }
//     }

//     private void OnCollisionExit(Collision other)
//     {
//         if (other.gameObject.CompareTag("Floor"))
//         {
//             isGrounded = false;
//         }
//     }

//     private void RotateTowardsMovement()
//     {
//         if (moveDirection != Vector3.zero)
//         {
//             Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//     }

//     private void UpdateAnimationStates()
//     {
//         bool isMoving = moveDirection.magnitude > 0;
//         bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
//         bool isWalking = isMoving && !isRunning;
//         bool isIdle = !isMoving; // 🔹 Estado de Idle cuando no se mueve

//         animator.SetBool("isWalking", isWalking);
//         animator.SetBool("isRunning", isRunning);
//         animator.SetBool("isIdle", isIdle);
//     }

//     private void HandleAttack()
//     {
//         if ((animator.GetBool("isIdle") || animator.GetBool("isWalking") || animator.GetBool("isRunning")) 
//             && Input.GetButtonDown("Fire1") && !isAttacking)
//         {
//             isAttacking = true;
//             animator.SetBool("isMeele", true); //  Activa el ataque en el Animator
//             Invoke("ResetAttack", 0.5f); // Controla la duración del ataque
//         }
//     }

//     private void ResetAttack()
//     {
//         isAttacking = false;
//         animator.SetBool("isMeele", false); //  Desactiva el ataque después de un tiempo
//     }
// }

using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Configuración")]
    private float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    [SerializeField] private Rigidbody playerRigidbody;
    private Vector3 moveDirection;
    private Animator animator;
    public bool isGrounded;
    private bool isAttacking = false;
    private string lastState = "Idle";

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        RotateTowardsMovement();
        UpdateAnimationStates();
        HandleAttack();
    }

    void FixedUpdate()
    {
        ApplyPhysicsMovement();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
        }
    }

    private void ApplyPhysicsMovement()
    {
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(newPosition);
    }

    private void ApplyJump()
    {
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    private void RotateTowardsMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimationStates()
    {
        if (isAttacking) return; // Evita cambiar animaciones si está atacando

        bool isMoving = moveDirection.magnitude > 0;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        bool isWalking = isMoving && !isRunning;
        bool isIdle = !isMoving;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdle", isIdle);
    }

    private void HandleAttack()
    {
        if (!isAttacking)
        {
            if (animator.GetBool("isIdle")) lastState = "Idle";
            if (animator.GetBool("isWalking")) lastState = "Walking";
            if (animator.GetBool("isRunning")) lastState = "Running";
        }

        if ((animator.GetBool("isIdle") || animator.GetBool("isWalking") || animator.GetBool("isRunning")) 
            && Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isMeele", true);

            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);

            Invoke("ResetAttack", 0.5f);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isMeele", false);

        if (lastState == "Idle") animator.SetBool("isIdle", true);
        if (lastState == "Walking") animator.SetBool("isWalking", true);
        if (lastState == "Running") animator.SetBool("isRunning", true);
    }
}
