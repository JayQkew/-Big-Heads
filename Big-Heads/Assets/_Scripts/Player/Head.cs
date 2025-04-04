using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D),
    typeof(CircleCollider2D),
    typeof(SpringJoint2D))]
[SelectionBase]
public class Head : MonoBehaviour, IDamageable
{
    private StatHandler _statHandler;
    private PlayerHealth _playerHealth;

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

    private void Awake() {
        _statHandler = GetComponentInParent<StatHandler>();
        _playerHealth = GetComponentInParent<PlayerHealth>();

        rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
        _joint = GetComponent<SpringJoint2D>();

        bounceMat = new PhysicsMaterial2D
        {
            name = "bounceMatPersonal"
        };
    }

    private void Start() {
        bounceMat.bounciness = _statHandler.GetFloatStat(Stat.HeadBounce);
        _col.sharedMaterial = bounceMat;

        rb.mass = _statHandler.GetFloatStat(Stat.HeadMass);
        rb.gravityScale = _statHandler.GetFloatStat(Stat.HeadGravity);

        _col.radius = _statHandler.GetFloatStat(Stat.HeadSize)/2;

        transform.GetChild(0).localScale = Vector3.one * _statHandler.GetFloatStat(Stat.HeadSize);
    }

    public void AttachHead(bool isAttached) {
        _joint.enabled = isAttached;
        if (isAttached) _joint.distance = 0;
    }

    public void Damage(float dmg) {
        _playerHealth.TakeDamage(dmg);
    }
}