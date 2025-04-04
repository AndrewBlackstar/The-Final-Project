using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    private float originalSpeed; // Velocidad original del enemigo
    private float slowedSpeed; // Velocidad reducida durante el efecto
    private float slowDuration; // Duración del efecto
    private bool isSlowed = false;
    private IMovable movable; // Referencia al movimiento del enemigo
    [SerializeField] private ParticleSystem slowVfx;


    void Awake()
    {
        movable = GetComponent<IMovable>();
        if (movable == null)
        {
            Debug.LogError("❌ El enemigo no implementa la interfaz IMovable.");
        }
    }

    public void ApplySlow(float slowPercentage, float duration)
    {
        if (isSlowed) return; // Evitar acumular múltiples efectos

        originalSpeed = movable.Speed;
        slowedSpeed = originalSpeed * (1f - slowPercentage); // Reducir la velocidad según el porcentaje
        slowDuration = duration;

        movable.Speed = slowedSpeed;
        isSlowed = true;

        // Activa las partículas
        if (slowVfx != null)
        {
            slowVfx.Play();
        }

        Debug.Log($"⏳ Enemigo ralentizado. Velocidad reducida a {slowedSpeed} durante {slowDuration} segundos.");

        Invoke(nameof(ResetSpeed), slowDuration);
    }

    private void ResetSpeed()
    {
        movable.Speed = originalSpeed;
        isSlowed = false;

        Debug.Log($"✅ Efecto de ralentización terminado. Velocidad restaurada a {originalSpeed}.");
    }
}
