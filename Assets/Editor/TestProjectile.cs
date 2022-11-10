using System.Collections;
using System.Collections.Generic;
using Modifications;
using Player;
using UnityEngine;

public class TestProjectile : Projectile.BasicProjectile
{
    [SerializeField] private float knockback = 5;
    [SerializeField] private StatModifier _statModifier = new();
    
    public new void OnTriggerEnter2D(Collider2D col)
    {
        if(col.usedByEffector) return;
        Debug.Log(col);
        if (col.gameObject.TryGetComponent(out PlayerController p)) // TODO: need universal base class for game entities
        {
            p.ApplyModifier(_statModifier);
        }

        if (col.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddForce((p.transform.position - transform.position).normalized * knockback, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }
}
