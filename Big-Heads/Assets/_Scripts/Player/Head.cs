using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D),
    typeof(CircleCollider2D),
    typeof(SpringJoint2D))]
[SelectionBase]
public class Head : MonoBehaviour , IDamageable
{
    private StatModifiers _statModifiers;
    private PlayerStats _playerStats;
    
    public Rigidbody2D rb;
    private CircleCollider2D _col;
    private SpringJoint2D _joint;
    
    [Header("Bounce")]
    [SerializeField] private PhysicsMaterial2D bounceMat;
    [SerializeField] private float bounce;

    [Header("Physics")] 
    [SerializeField] private float headSize;
    [SerializeField] private float headMass;
    [SerializeField] private float headGravity;

    private void Awake()
    {
        _statModifiers = GetComponentInParent<StatModifiers>();
        _playerStats = GetComponentInParent<PlayerStats>();
        
        rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
        _joint = GetComponent<SpringJoint2D>();
        
        bounceMat = new PhysicsMaterial2D
        {
            name = "bounceMatPersonal"
        };
    }

    private void Start()
    {
        bounceMat.bounciness = bounce * _statModifiers.HeadBounceMult;
        _col.sharedMaterial = bounceMat;
        
        rb.mass = headMass * _statModifiers.HeadMassMult;
        rb.gravityScale = headGravity * _statModifiers.HeadGravityMult;
        
        _col.radius = headSize * _statModifiers.HeadSizeMult/2;
        transform.GetChild(0).localScale = Vector3.one * headSize * _statModifiers.HeadSizeMult;
    }

    public void AttachHead(bool isAttached)
    {
        _joint.enabled = isAttached;
        if (isAttached) _joint.distance = 0;
    }

    public void Damage(float dmg) {
        _playerStats.TakeDamage(dmg);
    }
}
