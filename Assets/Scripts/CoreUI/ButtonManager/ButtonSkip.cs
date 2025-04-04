using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ButtonSkip : MonoBehaviour
{
    public PlayableDirector director;
    public SpawnerWeaponCinematic weaponCinematic;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SkipCutscene();
        }
    }

    void SkipCutscene()
    {
        if (director != null && director.state == PlayState.Playing)
        {
            director.time = director.duration;
            director.Evaluate();
        }

        // Detiene la secuencia de armas si está activa
        if (weaponCinematic != null)
        {
            weaponCinematic.SkipSequence();
        }

        gameObject.SetActive(false);
    }
}
