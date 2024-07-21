using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            RotateFirePointTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void RotateFirePointTowardsPlayer()
    {
        Vector3 direction = (player.position - firePoint.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        firePoint.rotation = Quaternion.Slerp(firePoint.rotation, lookRotation, Time.deltaTime * 5f);

        // Add 90 degrees rotation around the Y axis to the enemy's rotation
        Quaternion additionalRotation = Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * additionalRotation, Time.deltaTime * 5f);
    }

    void FireProjectile()
    {
        Vector3 direction = (player.position - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = direction * 20f; // Adjust the speed as needed
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
