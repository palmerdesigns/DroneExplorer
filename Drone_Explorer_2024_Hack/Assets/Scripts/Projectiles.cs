using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the inspector
    public Transform bulletSpawnPoint; // Assign the spawn point in the inspector
    public float bulletSpeed = 20f;
    public float fireRate = 0.10f; // Delay between shots

    public float nextFireTime = 0f;

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);
    }
}
