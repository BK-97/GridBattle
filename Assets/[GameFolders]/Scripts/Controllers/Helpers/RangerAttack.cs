using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    public GameObject bullet;
    public void CreateBullet(LayerMask enemyLayer, int damage,Vector3 moveDirection)
    {
        Quaternion instantiateRot = Quaternion.identity;
        Vector3 spawnPos = transform.position - Vector3.up * 0.2f;
        var go = PoolingSystem.SpawnObject(bullet, spawnPos, instantiateRot);
        go.GetComponent<Bullet>().Initialize(enemyLayer, damage, moveDirection);
    }
}
