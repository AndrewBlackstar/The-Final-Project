using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveForce = 2f;
    public float jumpForce = 200f;
    public float runMultiplier = 4f; // Factor de velocidad al correr

    public Transform cameraTransform;
    private Rigidbody rb;
    private Animator animatorPlayer;

    private bool isGrounded;
    private bool hasJumped;
    private bool isRunning;

    private void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 5f; // Damping para suavizar el movimiento
        rb.angularDamping = 5f; // Damping para suavizar la rotación
        animatorPlayer = GetComponent<Animator>();
        
        hasJumped = false;
        isGrounded = true;
        isRunning = false;
    }

    void FixedUpdate()
{
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");

    Vector3 forward = cameraTransform.forward;
    Vector3 right = cameraTransform.right;
    forward.y = 0;
    right.y = 0;
    forward.Normalize();
    right.Normalize();

    Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

    if (Input.GetKey(KeyCode.R))
    {
        Run(moveDirection);
    }
    else
    {
        Move(moveDirection);
    }
    
    if (moveDirection.magnitude > 0.1f)
    {
        Rotation(moveDirection);
    }               

    // Salto normal cuando NO está corriendo
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isRunning)
    {            
        NormalJump();
    }

    // Salto de carrera cuando está corriendo
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isRunning)
    {
        RunJump();
    }
}


    // Caminar
    private void Move(Vector3 direction)
    {
        isRunning = false;
        rb.linearVelocity = new Vector3(direction.x * moveForce, rb.linearVelocity.y, direction.z * moveForce);

        if (animatorPlayer != null)
        {
            animatorPlayer.SetBool("isRunning", false); // <-- Desactiva la animación de correr
            animatorPlayer.SetBool("isWalking", direction.magnitude > 0.1f); // <-- Activa la animación de caminar
        }
    }

    // Rotar el personaje
    private void Rotation(Vector3 direction)
    {       
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);        
    }
    
    // Correr
    private void Run(Vector3 direction)
    {
        isRunning = true;
        rb.linearVelocity = new Vector3(direction.x * moveForce * runMultiplier, rb.linearVelocity.y, direction.z * moveForce * runMultiplier);

        if (animatorPlayer != null)
        {
            animatorPlayer.SetBool("isRunning", true);
        }

        
    }

    // Salto normal
private void NormalJump()
{
    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    isGrounded = false;
    hasJumped = true;

    if (animatorPlayer != null)
    {
        animatorPlayer.SetBool("isJumping", true);
    }
}

// Salto cuando corre (puede ser más largo o más alto si quieres)
private void RunJump()
{
    rb.AddForce(Vector3.up * (jumpForce * 1.2f), ForceMode.Impulse); // Aumenta la fuerza del salto si está corriendo
    isGrounded = false;
    hasJumped = true;

    if (animatorPlayer != null)
    {
        animatorPlayer.SetBool("isJumping", true);
    }
}

    // Verifica si el jugador está en el suelo   
    
    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        hasJumped = false;

        if (animatorPlayer != null)
        {
            animatorPlayer.SetBool("isJumping", false);
        }
    }
}

private void OnCollisionStay(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
    }
}
}
