using Unity.Cinemachine;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public CinemachineCamera thirdPersonCamera;
    public CinemachineCamera firstPersonCamera;
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float verticalLookLimit = 80f;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool isFirstPerson = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
            SwitchCamera();
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (!isFirstPerson)
        {
            playerBody.Rotate(Vector3.up * mouseX);
            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -verticalLookLimit, verticalLookLimit);
            thirdPersonCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        }
        else
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
            firstPersonCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void SwitchCamera()
    {
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
    }
}