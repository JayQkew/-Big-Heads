using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

public class Head : MonoBehaviour, IDamageable
{
    public int health;
    [SerializeField] private int maxHealth;
    [Space(10)]
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    [Space(10)] [SerializeField] private float radiusChangeSpeed;
    private float oldSize;

    private SoftBody softBody;

    private void Awake()
    {
        softBody = gameObject.GetComponentInChildren<SoftBody>();
    }

    private void Start()
    {
        minSize = softBody.radius;
    }
    
    public void Expand(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) softBody.radius = maxSize;
        else if (ctx.canceled) softBody.radius = minSize;
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