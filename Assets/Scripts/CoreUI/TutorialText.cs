using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private Animator textAnimator;

    private void Start()
    {
        textAnimator.Play("Hidden"); // Asegura que inicie oculto
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textAnimator.SetBool("show", true);  // Activa la animación de entrada
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textAnimator.SetBool("show", false);
            textAnimator.SetTrigger("hide");  // Activa la animación de salida
        }
    }
}
