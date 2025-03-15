using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

public class Head : MonoBehaviour, IDamageable
{
    public bool headAttached = true;
    [Header("Health")]
    public int health;
    [SerializeField] private int maxHealth;
    
    [Header("Head")]
    [Space(10)]
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    [Space(10)] [SerializeField] private float radiusChangeSpeed;

    [Header("Thow")]
    [SerializeField] private float thowForceMult;
    
    private SoftBody softBody;
    private Aim aim;

    private void Awake()
    {
        softBody = gameObject.GetComponentInChildren<SoftBody>();
        aim = GetComponentInParent<Aim>();
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

    public void Throw(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && headAttached)
        {
            softBody.AddForce(aim.dir * thowForceMult, ForceMode2D.Impulse);
            headAttached = false;
        }
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