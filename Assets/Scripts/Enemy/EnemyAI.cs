using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float speed = 3f;
    public float attackRange = 2f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;

    [Header("Object Interaction")]
    public float objectDetectionRange = 10f;
    public Transform handPosition;
    public float throwForce = 15f;
    private Rigidbody enemyRb;
    private float lastAttackTime = 0f;
    private GameObject heldObject = null;
    private readonly List<string> validTags = new() { "objectSmall", "objectMedium", "objectBig" };

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.isKinematic = false;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
            if (heldObject == null)
                FindAndPickObject();
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            AttackPlayer();
        }
    }

    void FixedUpdate()
    {
        HandleGravity();
    }

    private void HandleGravity()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f);

        Debug.DrawRay(transform.position, Vector3.down * 1.5f, isGrounded ? Color.green : Color.red);

        if (!isGrounded)
        {
            enemyRb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        enemyRb.linearVelocity = new Vector3(direction.x * speed, enemyRb.linearVelocity.y, direction.z * speed);
    }

    private void AttackPlayer()
    {
        if (player.TryGetComponent(out Rigidbody playerRb))
        {
            Vector3 pushDirection = (player.position - transform.position).normalized;
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            Debug.Log("Golpeando al jugador");
        }
    }

    private void FindAndPickObject()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, objectDetectionRange);

        foreach (Collider obj in objects)
        {
            if (!validTags.Contains(obj.tag)) continue;

            heldObject = obj.gameObject;
            heldObject.transform.position = handPosition.position;
            //heldObject.transform.SetParent(handPosition);

            if (heldObject.TryGetComponent(out Rigidbody objRb))
            {
                objRb.isKinematic = true;
                objRb.detectCollisions = false;
            }

            Debug.Log($"Objeto recogido: {heldObject.name}");

            if (heldObject != null && heldObject.activeInHierarchy)
            {
                Invoke(nameof(ThrowObjectAtPlayer), 1f);
            }
            break;
        }
    }

    private void ThrowObjectAtPlayer()
    {
        if (heldObject == null || !heldObject.activeInHierarchy)
        {
            Debug.LogWarning("No hay ninguno objeto.");
            return;
        }

        if (heldObject.TryGetComponent(out Rigidbody objRb))
        {
            objRb.isKinematic = false;
            objRb.detectCollisions = true;
            objRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            //heldObject.transform.SetParent(null);
            //objRb.velocity = Vector3.zero;
            //objRb.angularVelocity = Vector3.zero;
            objRb.WakeUp();

            Vector3 throwDirection = (player.position - handPosition.position).normalized;
            throwDirection.y = 0.5f;
            objRb.linearVelocity = Vector3.zero;
            objRb.angularVelocity = Vector3.zero;
            objRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            if (heldObject.TryGetComponent(out ThrowableObject throwable))
            {
                Debug.Log($"Lanzado {heldObject.name} con {throwable.damage} de daño.");
            }
            // Debug.Log($"Lanzando {heldObject.name} hacia {player.name}");

            heldObject = null;
        }
    }
}


