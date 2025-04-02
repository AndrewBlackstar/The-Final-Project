using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    [SerializeField] private Rigidbody playerRigidbody;
    public Vector3 moveDirection; //se puso publica para llamar 
    public bool isGrounded;

    [Header("Armas")]
    public Transform weaponsParent; // AsignaR el objeto padre "Weapons" en el Inspector
    [SerializeField] private GameObject[] weapons; // Array para almacenar las armas
    private int lastWeaponID = -1; // Para evitar cambios innecesarios

    [SerializeField] Camera playerCamera;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        // animator = GetComponent<Animator>();

        // Inicializar sistema de armas (mejorado)
        InitializeWeapons();
    }

    void InitializeWeapons()
    {
        if (weaponsParent == null)
        {
            Debug.LogError("❌ Error: No se asignó 'weaponsParent' en el Inspector.");
            return;
        }

        weapons = new GameObject[weaponsParent.childCount];
        for (int i = 0; i < weaponsParent.childCount; i++)
        {
            weapons[i] = weaponsParent.GetChild(i).gameObject;
            weapons[i].SetActive(false);
            Debug.Log($"✅ Arma registrada: {weapons[i].name} (Posición: {i + 1})");
        }
    }

    void Update()
    {
        HandleInput();

        // Actualizar arma activa (versión optimizada)
        UpdateActiveWeapon();

        if (moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
        }

    }

    // ---- **Añadidos para mejorar el sistema de armas** ---- 
    void UpdateActiveWeapon()
    {
        int newWeaponID = WeaponWheelController.weaponID;

        // Solo cambiar si el ID es diferente y válido
        if (newWeaponID != lastWeaponID && newWeaponID >= 0 && newWeaponID <= weapons.Length)
        {
            SwitchWeapon(newWeaponID);
        }
    }



    void SwitchWeapon(int weaponID)
    {
        if (weaponID == 0) return; // No hacer nada si weaponID es 0

        // Desactivar todas las armas antes de activar la nueva
        foreach (var weapon in weapons)
        {
            if (weapon != null) weapon.SetActive(false);
        }

        int arrayIndex = weaponID - 1;
        if (arrayIndex < weapons.Length && weapons[arrayIndex] != null)
        {
            weapons[arrayIndex].SetActive(true);
            Debug.Log($"🔫 Arma activada: {weapons[arrayIndex].name} (ID: {weaponID})");
        }

        lastWeaponID = weaponID;
    }

    // ---------------------------------------------------------


    void FixedUpdate()
    {
        ApplyPhysicsMovement();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = playerCamera.transform.right;
        right.y = 0f;
        right.Normalize();


        moveDirection = forward * vertical + right * horizontal;


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
        }
    }

    private void ApplyPhysicsMovement()
    {
        playerRigidbody.MovePosition(transform.localPosition + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void ApplyJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
