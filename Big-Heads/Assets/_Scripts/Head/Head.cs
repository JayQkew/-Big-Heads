using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Head : MonoBehaviour, IDamageable
{
    public int health;
    private int maxHealth;

    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Debug.Log("Player Died");
        }
    }
}
