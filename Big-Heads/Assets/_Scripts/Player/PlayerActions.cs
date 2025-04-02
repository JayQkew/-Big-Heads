using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerActions : MonoBehaviour
{
    private InputHandler _inputHandler;
    private StatModifiers _statModifiers;
    private Movement _movement;

    public bool headAttached = true;

    [Space(10)]
    [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    private Head _head;

    [Header("Throw")]
    [SerializeField] private float throwForce;

    [Header("Teleport")]
    [SerializeField] private float teleportTime;
    [SerializeField] private float currTeleportTime;

    [Header("Shooting")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float shootForce;

    [Space(10)]
    [SerializeField] private int maxAmmo;
    [SerializeField] private int currAmmo;

    [Space(10)]
    [SerializeField] private float fireRate;
    [SerializeField] private float currFireRate;

    private void Awake() {
        _inputHandler = GetComponent<InputHandler>();
        _statModifiers = GetComponent<StatModifiers>();
        _movement = GetComponent<Movement>();
        _head = head.GetComponent<Head>();
    }

    private void Start() {
        currAmmo = maxAmmo + _statModifiers.ExtraAmmo;

        if (_statModifiers.AutoFire) {
            _inputHandler.onShoot.AddListener(Shoot);
            _inputHandler.onShootStart.RemoveListener(Shoot);
        }
        else {
            _inputHandler.onShootStart.AddListener(Shoot);
            _inputHandler.onShoot.RemoveListener(Shoot);
        }
    }

    private void Update() {
        GunDirection();
        currTeleportTime += Time.deltaTime;
        currFireRate += Time.deltaTime;
    }

    public void TeleportThrow() {
        if (headAttached) Throw();
        else Teleport();
    }

    private void Teleport() {
        if (currTeleportTime * _statModifiers.TeleportReloadMult >= teleportTime) {
            body.position = head.position;
            head.position = body.position + Vector3.up;
            _movement._rb.linearVelocity = head.GetComponent<Rigidbody2D>().linearVelocity;

            _head.AttachHead(true);
            headAttached = true;
            currTeleportTime = 0;
            currAmmo = maxAmmo + _statModifiers.ExtraAmmo;
        }
    }

    private void Throw() {
        Vector2 force = _inputHandler.aim * throwForce * _statModifiers.ThrowForceMult;
        _head.rb.linearVelocity = Vector2.zero;
        _head.rb.AddForce(force, ForceMode2D.Impulse);

        _head.AttachHead(false);
        headAttached = false;
    }

    public void Shoot() {
        if (currAmmo > 0 && currFireRate >= fireRate) {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetModifiers(_statModifiers.ExtraBounces,
                _statModifiers.BulletSizeMult,
                _statModifiers.BulletBounceMult,
                _statModifiers.BulletMassMult,
                _statModifiers.BulletGravityMult,
                _statModifiers.DamageMult);
            bullet.GetComponent<Bullet>().rb.AddForce(_inputHandler.aim * shootForce * _statModifiers.FireForceMult,
                ForceMode2D.Impulse);

            currAmmo--;
            currFireRate = 0;
        }
    }

    private void GunDirection() {
        float deg = Mathf.Atan2(_inputHandler.aim.y, _inputHandler.aim.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0, 0, deg - 90);
    }
}