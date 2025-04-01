using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private InputHandler inputHandler;

    public bool headAttached = true;
    [Space(10)] [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    private new_Head _head;
    [Header("Throw")] [SerializeField] private float throwMult;

    [Header("Shooting")] [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float shootMult;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
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
        Vector2 force = inputHandler.aim * throwMult;
        _head.rb.linearVelocity = Vector2.zero;
        _head.rb.AddForce(force, ForceMode2D.Impulse);

        _head.AttachHead(false);
        headAttached = false;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(inputHandler.aim * shootMult, ForceMode2D.Impulse);
    }

    private void GunDirection()
    {
        float deg = Mathf.Atan2(inputHandler.aim.y, inputHandler.aim.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, deg - 90);
    }
}