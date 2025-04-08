using UnityEngine;
using System.Collections.Generic;

public class FireballPool : MonoBehaviour
{
    public static FireballPool instance;

    public GameObject fireballPrefab;
    public int poolSize = 10;

    private Queue<GameObject> fireballPool = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab);
            fireball.SetActive(false);
            fireballPool.Enqueue(fireball);
        }
    }

    public GameObject GetFireball(Vector3 position, Quaternion rotation)
    {
        GameObject fireball;

        if (fireballPool.Count > 0)
        {
            fireball = fireballPool.Dequeue();
            Debug.Log("✅ Fireball obtenida del pool.");
        }
        else
        {
            fireball = Instantiate(fireballPrefab);
            Debug.Log("⚠️ Pool vacío. Instanciando nueva fireball.");
        }

        fireball.transform.position = position;
        fireball.transform.rotation = rotation;
        fireball.SetActive(true);

        return fireball;
    }

    public void ReturnFireball(GameObject fireball)
    {
        fireball.SetActive(false);
        fireballPool.Enqueue(fireball);
    }
}
