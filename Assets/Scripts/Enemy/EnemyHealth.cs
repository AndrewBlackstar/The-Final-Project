using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Transform healthBarUI;
    public Vector3 offset = new Vector3(0, 2f, 0); // Posición sobre la cabeza

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (healthBarUI != null)
        {
            healthBarUI.position = transform.position + offset;
            healthBarUI.LookAt(mainCamera.transform);
        }
    }
}

