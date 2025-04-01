using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    
    private Vector3 offset = new Vector3(0f, 7f, -9f);
    public float RotationSpeed = 200.0f;
    public float zoomSpeed = 2.0f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float smoothSpeed = 5f;
    public float zoomSmoothSpeed = 10f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float targetZoom;
    private bool isFirstPerson = false;
    public Vector3 firstPersonOffset = new Vector3(0f, 0f, 0f);

    private float hudTimer = 0f;
    public float hudDisplayTime = 2f;

    public Vector3 rotationOffset = new Vector3(0, 10, 0); // Ajuste de rotación adicional

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        targetZoom = offset.magnitude;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
            hudTimer = hudDisplayTime;
            if (player != null)
            {
                player.SetActive(!isFirstPerson);
            }
        }
    }

    void LateUpdate()
    {
        if (isFirstPerson)
        {
            Vector3 headPosition = player.transform.position + firstPersonOffset;
            transform.position = headPosition;

            float mouseX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY; // Se invierte el eje Y para un control más natural
            pitch = Mathf.Clamp(pitch, -100f, 10f);

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            targetZoom -= scroll * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            float smoothZoom = Mathf.Lerp(offset.magnitude, targetZoom, Time.deltaTime * zoomSmoothSpeed);
            
            // Mantener las proporciones del offset al hacer zoom
            offset *= smoothZoom / offset.magnitude;

            Quaternion rotationOffsetQuat = Quaternion.Euler(rotationOffset);
            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0) * rotationOffsetQuat;
            Vector3 rotatedOffset = targetRotation * offset;
            Vector3 desiredPosition = player.transform.position + rotatedOffset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
            transform.LookAt(player.transform.position);
        }

        if (hudTimer > 0)
        {
            hudTimer -= Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (hudTimer > 0)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Bold;

            string modeText = isFirstPerson ? "First Person Mode" : "Third Person Mode";
            GUI.Label(new Rect(10, 10, 300, 30), modeText, style);
        }
    }
}
