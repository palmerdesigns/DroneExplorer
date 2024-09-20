using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    PatrolState patrolState;
    Projectiles projectiles;
    public float chaseSpeed = 20f;
    public float rotationSpeed = 5f;
    public float raycastDistance = 15f;
    public float shootDelay = 1f; // Delay before shooting
    private Transform player;
    private bool isChasing = false;
    private bool canShoot = true; // Flag to control shooting
    private Rigidbody rb;

    public enum PatrolState
    {
        PATROL,
        CHASE
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        projectiles = GetComponent<Projectiles>();
    }

    private void Update()
    {
        CheckState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            patrolState = PatrolState.CHASE;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            patrolState = PatrolState.PATROL;
        }
    }

    public void CheckState()
    {
        switch (patrolState)
        {
            case PatrolState.PATROL:
                Patroling();
                break;
            case PatrolState.CHASE:
                Chase();
                break;
        }
    }

    public void Patroling()
    {
        Debug.Log("Patroling");
    }

    public void Chase()
    {
        Debug.Log("Chasing");

        // Rotate towards player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards player
        Vector3 moveDirection = direction * chaseSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Perform raycast to check if player is in line of sight
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Player") && canShoot)
            {
                StartCoroutine(ShootWithDelay());
            }
        }
    }

    private IEnumerator ShootWithDelay()
    {
        canShoot = false; // Prevent shooting again immediately
        yield return new WaitForSeconds(shootDelay); // Wait for the specified delay
        ShootPlayer();
        canShoot = true; // Allow shooting again
    }

    public void ShootPlayer()
    {
        projectiles.Shoot();
        Debug.Log("Shooting player");
    }
}
