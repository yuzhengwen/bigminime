using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireInterval = .8f;
    private float nextFireTime = 0;
    public bool canShoot = true;
    private void Update()
    {
        if (Time.time >= nextFireTime && canShoot)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AudioHandler.Instance.PlayAudio("BulletFire");
    }
}
