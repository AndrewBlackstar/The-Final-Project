using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 2f;
    public float jumpForce = 200f;
    public float runMultiplier = 4f;

    public Transform cameraTransform;
    private Rigidbody rb;
    private Animator animatorPlayer;

    private bool isGrounded;
    private bool hasJumped;
    private bool isRunning;

    public float stepHeight = 0.35f;
    public float stepSmooth = 0.1f;

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
        rb.linearDamping = 5f;
        rb.angularDamping = 5f;
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

        if (Input.GetKey(KeyCode.LeftShift))
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isRunning)
        {
            NormalJump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isRunning)
        {
            RunJump();
        }

        StepClimb(); // <<-- Aquí se llama la función de escalones
    }

    private void Move(Vector3 direction)
    {
        isRunning = false;
        rb.linearVelocity = new Vector3(direction.x * moveForce, rb.linearVelocity.y, direction.z * moveForce);

        if (animatorPlayer != null)
        {
            animatorPlayer.SetBool("isRunning", false);
            animatorPlayer.SetBool("isWalking", direction.magnitude > 0.1f);
        }
    }

    private void Rotation(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    private void Run(Vector3 direction)
    {
        isRunning = true;
        rb.linearVelocity = new Vector3(direction.x * moveForce * runMultiplier, rb.linearVelocity.y, direction.z * moveForce * runMultiplier);

        if (animatorPlayer != null)
        {
            animatorPlayer.SetBool("isRunning", true);
        }
    }

    private void NormalJump()
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

    Vector3 jumpDirection = moveDirection * moveForce + Vector3.up * jumpForce;
    rb.AddForce(jumpDirection, ForceMode.Impulse);

    isGrounded = false;
    hasJumped = true;

    if (animatorPlayer != null)
    {
        animatorPlayer.SetBool("isJumping", true);
    }
}


    private void RunJump()
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

    Vector3 jumpDirection = moveDirection * moveForce * runMultiplier + Vector3.up * (jumpForce * 1.2f);
    rb.AddForce(jumpDirection, ForceMode.Impulse);

    isGrounded = false;
    hasJumped = true;

    if (animatorPlayer != null)
    {
        animatorPlayer.SetBool("isJumping", true);
    }
}


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

    // <<-- Escalones
    private void StepClimb()
    {
        Vector3[] directions = {
            transform.forward,
            (transform.forward + transform.right).normalized,
            (transform.forward - transform.right).normalized
        };

        foreach (var dir in directions)
        {
            RaycastHit hitLower;
            if (Physics.Raycast(transform.position + Vector3.up * 0.1f, dir, out hitLower, 0.5f))
            {
                RaycastHit hitUpper;
                if (!Physics.Raycast(transform.position + Vector3.up * stepHeight, dir, out hitUpper, 0.5f))
                {
                    rb.position += new Vector3(0f, stepSmooth, 0f);
                    break;
                }
            }
        }
    }
}
