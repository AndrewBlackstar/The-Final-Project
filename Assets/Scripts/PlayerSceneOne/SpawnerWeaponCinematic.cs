using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnerWeaponCinematic : MonoBehaviour
{
    public GameObject[] WeaponPrefab;
    public Transform spawnpont;
    public ParticleSystem spawnVfx;

    private Coroutine currentSequence;
    private GameObject currentWeapon;
    private bool cancelRequested = false;
    private bool hasBeenSkipped = false;

    public void ShowWeaponSequence()
    {
        if (hasBeenSkipped) return; // Evita que se vuelva a ejecutar tras el skip

        cancelRequested = false;

        if (currentSequence != null)
            StopCoroutine(currentSequence);

        currentSequence = StartCoroutine(SpawnSequence(2f));
    }

    IEnumerator SpawnSequence(float time)
    {
        foreach (var weapon in WeaponPrefab)
        {
            if (cancelRequested || hasBeenSkipped) yield break;

            if (currentWeapon != null)
                Destroy(currentWeapon);

            currentWeapon = Instantiate(weapon, spawnpont.position, Quaternion.identity);

            if (spawnVfx != null)
            {
                ParticleSystem vfx = Instantiate(spawnVfx, spawnpont.position, Quaternion.identity);
                vfx.Play();
                Destroy(vfx.gameObject, vfx.main.duration + vfx.main.startLifetime.constantMax);
            }

            yield return new WaitForSeconds(time);
        }

        HideWeapon();
    }

    public void SkipSequence()
    {
        cancelRequested = true;
        hasBeenSkipped = true;

        if (currentSequence != null)
        {
            StopCoroutine(currentSequence);
            currentSequence = null;
        }

        HideWeapon();
    }

    public void HideWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }
    }
}
