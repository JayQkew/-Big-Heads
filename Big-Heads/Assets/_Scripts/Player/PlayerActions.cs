using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerActions : MonoBehaviour
{
    private InputHandler _inputHandler;
    private StatModifiers _statModifiers;

    public bool headAttached = true;
    [Space(10)] [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    private new_Head _head;
    [FormerlySerializedAs("throwMult")] [Header("Throw")] [SerializeField] private float throwForce;

    [Header("Shooting")] [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [FormerlySerializedAs("shootMult")] [SerializeField] private float shootForce;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _statModifiers = GetComponent<StatModifiers>();
        _head = head.GetComponent<new_Head>();
    }

    private void Update()
    {
        GunDirection();
    }

    public void TeleportThrow()
    {
        if (headAttached) Throw();
        else Teleport();
    }

    private void Teleport()
    {
        body.position = head.position;
        head.position = body.position + Vector3.up;

        _head.AttachHead(true);
        headAttached = true;
    }

    private void Throw()
    {
        Vector2 force = _inputHandler.aim * throwForce * _statModifiers.ThrowForceMult;
        _head.rb.linearVelocity = Vector2.zero;
        _head.rb.AddForce(force, ForceMode2D.Impulse);

        _head.AttachHead(false);
        headAttached = false;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(_inputHandler.aim * shootForce * _statModifiers.FireForceMult, ForceMode2D.Impulse);
    }

    private void GunDirection()
    {
        float deg = Mathf.Atan2(_inputHandler.aim.y, _inputHandler.aim.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, deg - 90);
    }
}