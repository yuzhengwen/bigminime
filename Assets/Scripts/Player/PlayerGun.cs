using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireInterval = .8f;
    private float nextFireTime = 0;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
