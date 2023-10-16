using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    private LayerMask _hitLayer;
    private int _damage;
    private bool canMove;

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, moveSpeed * Time.deltaTime, _hitLayer))
            {
                DealDamage(hit.collider.gameObject);
                PoolingSystem.SpawnObject(PoolingSystem.Instance.GetObjectFromName("MagicHit"), transform.position,Quaternion.identity);
                PoolingSystem.ReturnObjectToPool(gameObject);
            }
        }
    }

    public void Initialize(LayerMask hitLayer, int damage)
    {
        _hitLayer = hitLayer;
        _damage = damage;
        canMove = true;
    }

    private void DealDamage(GameObject target)
    {
        IDamageable damageableTarget = target.GetComponent<IDamageable>();
        if (damageableTarget != null)
        {
            damageableTarget.TakeDamage(_damage);
        }
        else
            Debug.LogWarning("The object hit is not damageable!");

    }
}
