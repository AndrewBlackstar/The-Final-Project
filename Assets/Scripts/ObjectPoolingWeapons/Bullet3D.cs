using UnityEngine;

public class Bullet3D : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 20f;
    public System.Action destroyed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bala impactó con: {other.gameObject.name}");

        // Aquí puedes agregar lógica extra si necesitas.
        // Por ejemplo, si impacta con algo, puedes desactivarla en lugar de destruirla.

        gameObject.SetActive(false); // Desactiva la bala en lugar de destruirla
    }

    private void OnDisable()
    {
        destroyed?.Invoke();
    }
}
