using System.Collections.Generic;
using UnityEngine;

public class MorphGun : MonoBehaviour
{
    public MorphBullet shrink, grow;
    public float fireInterval = 1.5f;
    private float nextFireTime = 0;
    [SerializeField] private Transform firePoint;
    public bool canShoot = true;
    public void Shoot(MorphType type)
    {
        if (Time.time >= nextFireTime && canShoot)
        {
            Quaternion rotate = Quaternion.LookRotation(Vector3.forward, GetVectorToMouse(transform.position));
            Instantiate(type == MorphType.Shrink ? shrink : grow, firePoint.position, rotate);
            nextFireTime = Time.time + fireInterval;
            AudioHandler.Instance.PlayAudio("MorphGunFire");
        }
    }
    public static Vector2 GetVectorToMouse(Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, GetVectorToMouse(transform.position));
    }
}
public static class MorphValues
{
    public static Dictionary<MorphState, int> DamageChart = new()
    {
        { MorphState.Small, 1 },
        { MorphState.Normal, 5 },
        { MorphState.Big, 10 }
    };
}
