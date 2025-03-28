using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyThrowManager : MonoBehaviour
{
    public Transform handPosition;
    public float throwForce = 15f;
    private List<GameObject> objectQueue = new();
    private bool isHoldingObject = false;
    
    public void PickUpObject(GameObject obj)
    {
        if (isHoldingObject || objectQueue.Contains(obj)) return;
        
        objectQueue.Add(obj);
        isHoldingObject = true;
        StartCoroutine(HandleObjectThrow());
    }
    
    private IEnumerator HandleObjectThrow()
    {
        yield return new WaitForSeconds(1f); 

        if (objectQueue.Count > 0)
        {
            GameObject objToThrow = objectQueue[0];
            objectQueue.RemoveAt(0);
            ThrowObject(objToThrow);
        }

        isHoldingObject = false;
    }
    
    private void ThrowObject(GameObject obj)
    {
        if (obj == null) return;
        
        if (obj.TryGetComponent(out Rigidbody objRb))
        {
            objRb.isKinematic = false;
            objRb.detectCollisions = true;
            objRb.WakeUp();

            Vector3 throwDirection = (FindObjectOfType<EnemyAI>().player.position - obj.transform.position).normalized;
            throwDirection.y = 0.5f;
            objRb.linearVelocity = Vector3.zero;
            objRb.angularVelocity = Vector3.zero;
            objRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            Debug.Log($"Lanzando {obj.name} hacia el jugador");
        }
    }
}