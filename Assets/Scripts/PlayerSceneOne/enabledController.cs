using UnityEngine;

public class enabledController : MonoBehaviour
{
    MovementPlayer movement;

    private void Start()
    {
        movement = GetComponent<MovementPlayer>();
    }

    public void enabledMovement()
    {
        movement.enabled = true;
    }

    public void disabledMovement()
    {
        movement.enabled = false;
    }
}
