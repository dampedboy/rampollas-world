using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public float range = 10f;
    public float fireRate = 1f;
    public float rotationSpeed = 5f; // Velocità di rotazione del nemico

    private float nextFireTime = 0f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= range)
        {
            RotateTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                ShootAtPlayer();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void ShootAtPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity = direction * 10f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}