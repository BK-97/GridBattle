using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    public GameObject bullet;
    public void CreateBullet(LayerMask enemyLayer, int damage,Vector3 moveDirection)
    {
        var go = PoolingSystem.SpawnObject(bullet, transform.position, Quaternion.identity);
        go.GetComponent<Bullet>().Initialize(enemyLayer, damage, moveDirection);
    }
}
