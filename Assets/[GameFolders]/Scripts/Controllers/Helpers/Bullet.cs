using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    private LayerMask _hitLayer;
    private int _damage;
    private bool canMove;
    private Vector3 moveDirection;
    public GameObject hitParticle;
    private void Update()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, moveDirection, out hit, moveSpeed * Time.deltaTime, _hitLayer))
            {
                DealDamage(hit.collider.gameObject);
                PoolingSystem.SpawnObject(hitParticle, transform.position,Quaternion.identity);
                PoolingSystem.ReturnObjectToPool(gameObject);
            }
        }
    }

    public void Initialize(LayerMask hitLayer, int damage,Vector3 direction)
    {
        _hitLayer = hitLayer;
        _damage = damage;
        moveDirection = direction;
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
