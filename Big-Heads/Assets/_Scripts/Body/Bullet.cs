using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private CircleCollider2D _col;
    public float damage = 1;
    public int maxBounces;
    private int _currBounces = 0;
    public float bulletSize;
    public float bounciness;
    public float mass;
    public float gravity;

    private void Awake()
    {
        _col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetModifiers(int bounceMod, float sizeMod, float massMod, float gravityMod, float damageMod)
    {
        maxBounces = bounceMod;
        
        bulletSize = sizeMod;
        _col.radius = bulletSize/2;
        transform.GetChild(0).localScale = Vector3.one * bulletSize;
        
        rb.mass = massMod;
        
        rb.gravityScale = gravityMod;
        
        damage = damageMod;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<IDamageable>() != null)
        {
            collision.transform.GetComponent<IDamageable>().Damage(damage);
        }

        _currBounces++;
        if (_currBounces >= maxBounces) Destroy(gameObject);
    }
}
