﻿using System.Collections.Generic;
using UnityEngine;

public class MorphGun : MonoBehaviour
{
    public MorphBullet bulletPrefab;
    public float fireInterval = 1.5f;
    private float nextFireTime = 0;

    public void Shoot(MorphType type)
    {
        if (Time.time >= nextFireTime)
        {
            Quaternion rotate = Quaternion.LookRotation(Vector3.forward, GetVectorToMouse(transform.position));
            Instantiate(bulletPrefab, transform.position, rotate);
            bulletPrefab.type = type;
            nextFireTime = Time.time + fireInterval;
        }
    }
    public static Vector2 GetVectorToMouse(Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
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
